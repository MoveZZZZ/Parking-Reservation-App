using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ShowReservationListView.xaml
    /// </summary>
    public partial class ShowReservationListView : UserControl
    {

        public class chekBoxData
        {
            public int val;
            public string txt;
        }

        List<chekBoxData> chekedValue = new List<chekBoxData>();
        public ShowReservationListView()
        {

            InitializeComponent();
           
        }


        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;

            chekedValue.Add(new chekBoxData { txt = chkZone.Content.ToString(), val = Convert.ToInt32(chkZone.Tag) });

            /*ZoneText.Text = "Selected Zone Name= " + chkZone.Content.ToString();
            ZoneValue.Text = "Selected Zone Value= " + chkZone.Tag.ToString();*/
        }

        private void CheckBoxZone_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;
            chekedValue.RemoveAll(a => a.val == (Convert.ToInt32(chkZone.Tag.ToString())));
        }
        private void removeReserv_Click(object sender, RoutedEventArgs e)
        {
            listBoxZone.Items.Refresh();
            if(chekedValue.Count()>0)
            {
                DB db = new DB();
                db.openConnection();
                string tempParkID = null;
                string tempStTime = null;
                for (int i=0; i<chekedValue.Count(); i++)
                {
                    string t = listBoxZone.Items[0].ToString();
                    string temp = chekedValue[i].txt;
                    tempParkID = temp.Remove(10);
                    tempParkID = tempParkID.Remove(0,5);


                    tempStTime = temp.Remove(0, 22);
                    tempStTime = tempStTime.Remove(19);


                    MySqlCommand com = new MySqlCommand("DELETE FROM userpark WHERE `idpark`=@ip AND `startres`=@sr ", db.getConnection());
                    com.Parameters.Add("@ip", MySqlDbType.Int32).Value = Convert.ToInt32(tempParkID);
                    com.Parameters.Add("@sr", MySqlDbType.DateTime).Value = Convert.ToDateTime(tempStTime);
                    com.ExecuteNonQuery();
                    listBoxZone.Items.Refresh();
                
                }
                chekedValue.Clear();
                db.closeConnection();
                errTextShow.Foreground = Brushes.Green;

                errTextShow.Text = "* dane zostaly usunięte, zrestartuj stronę!";
            }
            else
            {
                errTextShow.Foreground = Brushes.Red;
                errTextShow.Text = "* dane nie są wybrane, gdy dopiero usuwałeś dane, zrestartuj stronę!";
            }

        }

    }
}
