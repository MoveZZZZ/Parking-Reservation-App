using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Uslugi_application_user.Views
{
    /// <summary>
    /// Логика взаимодействия для ReservationView.xaml
    /// </summary>
    public partial class AddReservationView : UserControl
    {
        public DateTime sRes;
        public DateTime eRes;
        public AddReservationView()
        {
            InitializeComponent();
            dataTimePickStart.DisplayDateStart = DateTime.Now;
            dataTimePickEnd.DisplayDateStart = DateTime.Now;
            comboBoxParkingNumber.IsEnabled = false;
        }

        private void dataTimePickStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            dataTimePickEnd.DisplayDateStart = dataTimePickStart.SelectedDate;
            var dataNow = DateTime.Now;
            int day = dataNow.Day;
            //errListaRez.Visibility = Visibility.Visible;
            if (dataTimePickStart.SelectedDate.ToString() == DateTime.Now.Date.ToString())
            {
                comboBoxTimeStart.Items.Clear();
                for (int hours = dataNow.Hour + 1; hours < 24; hours++)
                {
                    if (hours < 10)
                        comboBoxTimeStart.Items.Add("0" + Convert.ToString(hours) + ":00");
                    else
                        comboBoxTimeStart.Items.Add(Convert.ToString(hours) + ":00");
                }
            }
            else
            {
                comboBoxTimeStart.Items.Clear();
                for (int i = 0; i < 24; i++)
                {
                    if (i < 10)
                        comboBoxTimeStart.Items.Add("0" + Convert.ToString(i) + ":00");
                    else
                        comboBoxTimeStart.Items.Add(Convert.ToString(i) + ":00");
                }
            }

        }

        private void dataTimePickEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataNow = DateTime.Now;
            int day = dataNow.Day;
          //  errListaRez.Visibility = Visibility.Visible;
            if (dataTimePickEnd.SelectedDate.ToString() == DateTime.Now.Date.ToString())
            {
                comboBoxTimeEnd.Items.Clear();
                for (int h = Convert.ToInt32(dataNow.Hour) + 2; h < 24; h++)
                {
                    if (h < 10)
                        comboBoxTimeEnd.Items.Add("0" + Convert.ToString(h) + ":00");
                    else
                        comboBoxTimeEnd.Items.Add(Convert.ToString(h) + ":00");
                }
            }
            else
            {
                comboBoxTimeEnd.Items.Clear();
                for (int i = 0; i < 24; i++)
                {
                    if (i < 10)
                        comboBoxTimeEnd.Items.Add("0" + Convert.ToString(i) + ":00");
                    else
                        comboBoxTimeEnd.Items.Add(Convert.ToString(i) + ":00");
                }
            }
        }
        public void addListParking()
        {
           // errListaRez.Visibility = Visibility.Collapsed;
            comboBoxParkingNumber.Items.Clear();
            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `idpark`, `startres`, `endres` FROM userpark", db.getConnection());
            string t = dataTimePickStart.SelectedDate.ToString();
            string t2 = dataTimePickEnd.SelectedDate.ToString();

            t=t.Remove(10);
            t2=t2.Remove(10);

            string choiseDateSt = (t + " " + comboBoxTimeStart.Text + ":00");
            string choiseDateEnds = (t2 + " " + comboBoxTimeEnd.Text + ":00");

            sRes = Convert.ToDateTime(choiseDateSt);
            eRes = Convert.ToDateTime(choiseDateEnds);


            List<int> indexReser = new List<int>();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            db.closeConnection();
            foreach (DataRow row in table.Rows)
            {
                DateTime ds = Convert.ToDateTime(row[1].ToString());
                DateTime de = Convert.ToDateTime(row[2].ToString());

                if (sRes > de)
                {
                    continue;
                }
                else if (eRes < de && ds <= eRes)
                {
                    indexReser.Add(Convert.ToInt32(row[0].ToString()));
                }
                else
                {
                    indexReser.Add(Convert.ToInt32(row[0].ToString()));
                }
            }
            int a = 1;
            int c = indexReser.Count();
            if (c != 0)
            {
                indexReser.Sort();
                for (int n = 0; a < 171 && n != c; a++)
                {

                    if (a == indexReser[n])
                    {
                        n++;
                    }
                    else
                    {
                        comboBoxParkingNumber.Items.Add(a);
                    }
                }
            }
            while (a < 171)
            {
                comboBoxParkingNumber.Items.Add(a);
                a++;
            }
            comboBoxParkingNumber.IsEnabled = true;
        }



        private void addReserv_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxParkingNumber.SelectedItem != null && carNum.Text.Length <= 10 && carNum.Text.Length > 3 && sRes < eRes &&!chekNumberPark())
            {
                DB db = new DB();
                MySqlCommand com = new MySqlCommand("INSERT INTO userpark (`iduser`, `idpark`, `startres`, `endres`, `tablenumber`) VALUES(@id, @idp, @sres, @endres, @tn)", db.getConnection());
                com.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                com.Parameters.Add("@idp", MySqlDbType.Int32).Value = Convert.ToInt32(comboBoxParkingNumber.Text);
                com.Parameters.Add("@sres", MySqlDbType.DateTime).Value = sRes;
                com.Parameters.Add("@endres", MySqlDbType.DateTime).Value = eRes;
                com.Parameters.Add("@tn", MySqlDbType.VarChar).Value = carNum.Text.ToUpper();
                db.openConnection();
                if (com.ExecuteNonQuery() == 1)
                {
                    errText.Foreground = Brushes.Green;
/*                    comboBoxTimeStart.SelectedItem = null;
                    comboBoxTimeEnd.SelectedItem = null;*/
                    errText.Text = "Udało się!";
                }
                else
                {
                    MessageBox.Show("Mejsce nie zostało zarezerwowane, sprobuj ponownie!");
                }
                db.closeConnection();

                addListParking();
            }
            else if(comboBoxParkingNumber.SelectedItem == null)
            {
                errText.Foreground = Brushes.Red;
                errText.Text = "* mejsce nie zostalo wybrane";
            }
            else if(sRes>eRes)
            {
                errText.Foreground = Brushes.Red;
                errText.Text = "* nieprowidlowa data";
            }
            else if(carNum.Text.Length > 10 || carNum.Text.Length<=3)
            {
                errText.Foreground = Brushes.Red;
                errText.Text = "*  dlugosc numeru pojazdu powinna być <10 i >3!";
            }
            else
            {
                errText.Foreground = Brushes.Red;
                errText.Text = "* mejsce juz zarezerwowane innym uzytkownikiem";
            }
            comboBoxParkingNumber.IsEnabled = false;
        }
        private void comboBoxTimeEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (comboBoxTimeEnd.SelectedIndex >= 0 && comboBoxTimeStart.IsDropDownOpen == false && comboBoxTimeStart.SelectedIndex >= 0 && comboBoxTimeEnd.IsDropDownOpen == false)
                addListParking();
        }
        private bool chekNumberPark()
        {
            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `startres`, `endres` FROM userpark WHERE `idpark` = @ip", db.getConnection());
            command.Parameters.Add("@ip", MySqlDbType.Int32).Value = Convert.ToInt32(comboBoxParkingNumber.Text.ToString());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            db.closeConnection();

            foreach (DataRow row in table.Rows)
            {
                DateTime ds = Convert.ToDateTime(row[0].ToString());
                DateTime de = Convert.ToDateTime(row[1].ToString());

                if (sRes > de)
                {
                    continue;
                }
                else if (eRes < de && ds <= eRes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }

}
