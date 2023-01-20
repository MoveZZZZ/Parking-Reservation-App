using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
using Uslugi_application_user.Repositories;

namespace Uslugi_application_user.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private UserRepository urep;
        public SettingsView()
           
        {

            InitializeComponent();
            urep = new UserRepository();
        }

        private void changePasswd_Click(object sender, RoutedEventArgs e)
        {

            changePass.Visibility = Visibility;
            changeLogin.Visibility = Visibility;
            changeDataOs.Visibility = Visibility;
            changeMail.Visibility = Visibility;



            txtBlockName.Visibility = Visibility.Collapsed;
            txtBlockLastName.Visibility = Visibility.Collapsed;
            txtBlockUsername.Visibility = Visibility.Collapsed;
            txtBlockMail.Visibility = Visibility.Collapsed;
            txtBlockPass.Visibility = Visibility.Collapsed;
            txtBlockNewPass.Visibility = Visibility.Collapsed;


            txtNameUser.Visibility = Visibility.Collapsed;
            txtLastNameUser.Visibility = Visibility.Collapsed;
            txtLoginUser.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Collapsed;
            txtEmailUser.Visibility = Visibility.Collapsed;
            txtNewPass.Visibility = Visibility.Collapsed;

            appChangeLog.Visibility = Visibility.Collapsed;
            appChangeDataOs.Visibility = Visibility.Collapsed;
            appChangeMail.Visibility = Visibility.Collapsed;
            appChangePass.Visibility = Visibility.Collapsed;
            appDelReserv.Visibility = Visibility.Collapsed;
            appDelAcc.Visibility = Visibility.Collapsed;

            errSettings.Text = " ";
            txtNameUser.Text = " ";
            txtLastNameUser.Text = " ";
            txtLoginUser.Text = " ";
            txtEmailUser.Text = " ";
            txtPassword.Password = null;
            txtNewPass.Password = null;
        }
        private void appDelReserv_Click(object sender, RoutedEventArgs e)
        {
            string passUser = new NetworkCredential("", txtPassword.Password).Password;
            if (!BCrypt.Net.BCrypt.Verify(passUser, passUserEnd.Text.ToString()))
            {
                errSettings.Text = "* niepoprawne hasło!";
            }
            else
            {
                DB db = new DB();
                MySqlCommand com = new MySqlCommand("DELETE FROM `userpark` WHERE `iduser` = @uid", db.getConnection());
                com.Parameters.Add("@uid", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                db.openConnection();
                if (com.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Rezerwacje zostały usunięte. Zrestartuj aplikacje!");

                    errSettings.Text = "";
                    foreach (Window w in App.Current.Windows)
                        w.Close();
                }
                else
                {
                    MessageBox.Show("Rezerwacje nie zostały usunięte, sprobuj ponownie!");
                }
                db.closeConnection();
                
            }
        }
        private void removeAllReservation_Click(object sender, RoutedEventArgs e)
        {
            errSettings.Text = " ";
            txtNameUser.Text = " ";
            txtLastNameUser.Text = " ";
            txtLoginUser.Text = " ";
            txtEmailUser.Text = " ";
            txtPassword.Password = null;
            txtNewPass.Password = null; 

            changePass.Visibility = Visibility.Collapsed;
            changeLogin.Visibility = Visibility.Collapsed;
            changeDataOs.Visibility = Visibility.Collapsed;
            changeMail.Visibility = Visibility.Collapsed;
            txtBlockName.Visibility = Visibility.Collapsed;
            txtBlockLastName.Visibility = Visibility.Collapsed;
            txtBlockUsername.Visibility = Visibility.Collapsed;
            txtBlockMail.Visibility = Visibility.Collapsed;
            txtBlockPass.Visibility = Visibility.Collapsed;
            txtBlockNewPass.Visibility = Visibility.Collapsed;


            txtNameUser.Visibility = Visibility.Collapsed;
            txtLastNameUser.Visibility = Visibility.Collapsed;
            txtLoginUser.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Collapsed;
            txtEmailUser.Visibility = Visibility.Collapsed;
            txtNewPass.Visibility = Visibility.Collapsed;

            appChangeDataOs.Visibility = Visibility.Collapsed;
            appChangeLog.Visibility = Visibility.Collapsed;
            appChangeMail.Visibility = Visibility.Collapsed;
            appChangePass.Visibility = Visibility.Collapsed;
            appChangeLog.Visibility = Visibility.Collapsed;
            appChangeDataOs.Visibility = Visibility.Collapsed;
            appChangeMail.Visibility = Visibility.Collapsed;
            appChangePass.Visibility = Visibility.Collapsed;
            appDelReserv.Visibility = Visibility.Collapsed;
            appDelAcc.Visibility = Visibility.Collapsed;


            txtPassword.Visibility = Visibility;
            txtBlockPass.Visibility = Visibility;
            appDelReserv.Visibility = Visibility;


        }
        private void appDelAcc_Click(object sender, RoutedEventArgs e)
        {
            string passUser = new NetworkCredential("", txtPassword.Password).Password;
            if (!BCrypt.Net.BCrypt.Verify(passUser, passUserEnd.Text.ToString()))
            {
                errSettings.Text = "* niepoprawne hasło!";
            }
            else
            {
                DB db = new DB();
                MySqlCommand com = new MySqlCommand("DELETE FROM `users` WHERE `userID` = @uid", db.getConnection());
                com.Parameters.Add("@uid", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                db.openConnection();
                if (com.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Konto zostało usunięte. Zrestartuj aplikacje!");
                    MySqlCommand command = new MySqlCommand("DELETE FROM `userpark` WHERE `iduser` = @uid", db.getConnection());
                    command.Parameters.Add("@uid", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                    command.ExecuteNonQuery();
                    db.closeConnection();
                    errSettings.Text = "";
                    foreach (Window w in App.Current.Windows)
                        w.Close();
                }
                else
                {
                    MessageBox.Show(" Konto nie zostało usunięte, sprobuj ponownie!");
                }
            }
        }
        private void removeAccount_Click(object sender, RoutedEventArgs e)
        {
            errSettings.Text = " ";
            txtNameUser.Text = " ";
            txtLastNameUser.Text = " ";
            txtLoginUser.Text = " ";
            txtEmailUser.Text = " ";
            txtPassword.Password = null;
            txtNewPass.Password = null;

            changePass.Visibility = Visibility.Collapsed;
            changeLogin.Visibility = Visibility.Collapsed;
            changeDataOs.Visibility = Visibility.Collapsed;
            changeMail.Visibility = Visibility.Collapsed;
            txtBlockName.Visibility = Visibility.Collapsed;
            txtBlockLastName.Visibility = Visibility.Collapsed;
            txtBlockUsername.Visibility = Visibility.Collapsed;
            txtBlockMail.Visibility = Visibility.Collapsed;
            txtBlockPass.Visibility = Visibility.Collapsed;
            txtBlockNewPass.Visibility = Visibility.Collapsed;


            txtNameUser.Visibility = Visibility.Collapsed;
            txtLastNameUser.Visibility = Visibility.Collapsed;
            txtLoginUser.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Collapsed;
            txtEmailUser.Visibility = Visibility.Collapsed;
            txtNewPass.Visibility = Visibility.Collapsed;

            appChangeDataOs.Visibility = Visibility.Collapsed;
            appChangeLog.Visibility = Visibility.Collapsed;
            appChangeMail.Visibility = Visibility.Collapsed;
            appChangePass.Visibility = Visibility.Collapsed;
            appChangeLog.Visibility = Visibility.Collapsed;
            appChangeDataOs.Visibility = Visibility.Collapsed;
            appChangeMail.Visibility = Visibility.Collapsed;
            appChangePass.Visibility = Visibility.Collapsed;
            appDelReserv.Visibility = Visibility.Collapsed;


            txtPassword.Visibility = Visibility;
            txtBlockPass.Visibility = Visibility;
            appDelAcc.Visibility = Visibility;

        }
        private void appChangePass_Click(object sender, RoutedEventArgs e)
        {

            string passNew = new NetworkCredential("", txtNewPass.Password).Password;
            string passUser = new NetworkCredential("", txtPassword.Password).Password;
            string abc = passUserEnd.Text.ToString();
            if (!BCrypt.Net.BCrypt.Verify(passUser, passUserEnd.Text.ToString()))
            {
                errSettings.Text = "* niepoprawne tymczasowe hasło!";
            }
            else if (passNew == null || passNew.Length < 6)
            {
                errSettings.Text = "* niepoprawne nowe hasło!";
            }
            else
            {
                string a = BCrypt.Net.BCrypt.HashPassword(passNew);
                DB db = new DB();
                MySqlCommand com = new MySqlCommand("UPDATE `users` SET `pass` = @npas WHERE `userID` = @uid", db.getConnection());
                com.Parameters.Add("@uid", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                com.Parameters.Add("@npas", MySqlDbType.VarChar).Value = a;
                db.openConnection();
                if (com.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Hsalo zostale zmienione. Zrestartuj aplikacje!");

                    errSettings.Text = "";
                    foreach (Window w in App.Current.Windows)
                        w.Close();
                }
                else
                {
                    MessageBox.Show("Hasło nie zostałe zmienione, sprobuj ponownie!");
                }
            }
        }
        private void changePass_Click(object sender, RoutedEventArgs e)
        {
            errSettings.Text = " ";
            txtNameUser.Text = " ";
            txtLastNameUser.Text = " ";
            txtLoginUser.Text = " ";
            txtEmailUser.Text = " ";
            txtPassword.Password = null;
            txtNewPass.Password = null;

            changePass.Visibility = Visibility.Collapsed;
            changeLogin.Visibility = Visibility.Collapsed;
            changeDataOs.Visibility = Visibility.Collapsed;
            changeMail.Visibility = Visibility.Collapsed;


            txtPassword.Visibility = Visibility;
            txtNewPass.Visibility = Visibility;
            txtBlockPass.Visibility = Visibility;
            txtBlockNewPass.Visibility = Visibility;

            appChangePass.Visibility = Visibility;
        }
        private void appChangeLog_Click(object sender, RoutedEventArgs e)
        {
            string passUser = new NetworkCredential("", txtPassword.Password).Password;
            if (txtLoginUser.Text.ToString() == UserLoginUser.Text.ToString())
            {
                errSettings.Text = "* nie możesz zmienić login!";
            }

            else if(txtLoginUser.Text.Length<3 || txtLoginUser.Text.Length>20)
            {
                errSettings.Text = "* login musi być >3 i <20 znaków!";
            }
            else if(urep.chekUsername(txtLoginUser.Text.ToString()))
            {
                errSettings.Text = "* tzki użytkownik już istneje!";
            }
            else if(!BCrypt.Net.BCrypt.Verify(passUser, passUserEnd.Text.ToString()))
            {
                errSettings.Text = "* niepoprawne hasło!";
            }
            else
            {
                DB db = new DB();
                MySqlCommand com = new MySqlCommand("UPDATE `users` SET `login` = @npas WHERE `userID` = @uid", db.getConnection());
                com.Parameters.Add("@uid", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                com.Parameters.Add("@npas", MySqlDbType.VarChar).Value = txtLoginUser.Text.ToString();
                db.openConnection();
                if (com.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Login zostal zmieniony. Zrestartuj aplikacje!");

                    errSettings.Text = "";
                    foreach (Window w in App.Current.Windows)
                        w.Close();
                }
                else
                {
                    MessageBox.Show("Login nie został zmieniony, sprobuj ponownie!");
                }
            }
        }
        private void changeLogin_Click(object sender, RoutedEventArgs e)
        {
            errSettings.Text = " ";
            txtNameUser.Text = " ";
            txtLastNameUser.Text = " ";
            txtLoginUser.Text = " ";
            txtEmailUser.Text = " ";
            txtPassword.Password = null;
            txtNewPass.Password = null;

            changePass.Visibility = Visibility.Collapsed;
            changeLogin.Visibility = Visibility.Collapsed;
            changeDataOs.Visibility = Visibility.Collapsed;
            changeMail.Visibility = Visibility.Collapsed;



            txtLoginUser.Visibility = Visibility;
            txtBlockUsername.Visibility = Visibility;
            txtPassword.Visibility = Visibility;
            txtBlockPass.Visibility = Visibility;

            appChangeLog.Visibility = Visibility;



        }
        private void appChangeDataOs_Click(object sender, RoutedEventArgs e)
        {
            string passUser = new NetworkCredential("", txtPassword.Password).Password;
            if (txtNameUser.Text.Length<3 && txtNameUser.Text.Length>10)
            {
                errSettings.Text = "* imie musi być >3 i <10 znaków!";
            }
            else if(txtLastNameUser.Text.Length<3 && txtLastNameUser.Text.Length>10)
            {
                errSettings.Text = "* nazwisko musi być >3 i <10 znaków!";
            }
            else if (!BCrypt.Net.BCrypt.Verify(passUser, passUserEnd.Text.ToString()))
            {
                errSettings.Text = "* niepoprawne hasło!";
            }
            else
            {
                DB db = new DB();
                MySqlCommand com = new MySqlCommand("UPDATE `users` SET `name` = @nname, `surname` = @nsur WHERE `userID` = @uid", db.getConnection());
                com.Parameters.Add("@uid", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                com.Parameters.Add("@nname", MySqlDbType.VarChar).Value = txtNameUser.Text.ToString();
                com.Parameters.Add("@nsur", MySqlDbType.VarChar).Value = txtLastNameUser.Text.ToString();
                db.openConnection();
                if (com.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Dane zostały zmienione. Zrestartuj aplikacje!");

                    errSettings.Text = "";
                    foreach (Window w in App.Current.Windows)
                        w.Close();
                }
                else
                {
                    MessageBox.Show("Dane nie zostały zmienione, sprobuj ponownie!");
                }

            }
        }
        private void changeDataOs_Click(object sender, RoutedEventArgs e)
        {
            errSettings.Text = " ";
            txtNameUser.Text = " ";
            txtLastNameUser.Text = " ";
            txtLoginUser.Text = " ";
            txtEmailUser.Text = " ";
            txtPassword.Password = null;
            txtNewPass.Password = null;

            changePass.Visibility = Visibility.Collapsed;
            changeLogin.Visibility = Visibility.Collapsed;
            changeDataOs.Visibility = Visibility.Collapsed;
            changeMail.Visibility = Visibility.Collapsed;


            txtNameUser.Visibility = Visibility;
           
            txtBlockName.Visibility = Visibility;
            txtLastNameUser.Visibility = Visibility;
            txtBlockLastName.Visibility = Visibility;
            txtPassword.Visibility = Visibility;
            txtBlockPass.Visibility = Visibility;
            appChangeDataOs.Visibility = Visibility;
        }
        private void appChangeMail_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            string passUser = new NetworkCredential("", txtPassword.Password).Password;
            if (!r.Match(txtEmailUser.Text.ToString()).Success)
            {
                errSettings.Text = "* zły mail!";
            }
            else if(urep.chekEmail(txtEmailUser.Text.ToString()))
            {
                errSettings.Text = "* taki mail juz jest zarejstrowany!";
            }
            else if (!BCrypt.Net.BCrypt.Verify(passUser, passUserEnd.Text.ToString()))
            {
                errSettings.Text = "* niepoprawne hasło!";
            }
            else
            {
                DB db = new DB();
                MySqlCommand com = new MySqlCommand("UPDATE `users` SET `mail` = @nmail WHERE `userID` = @uid", db.getConnection());
                com.Parameters.Add("@uid", MySqlDbType.Int32).Value = Convert.ToInt32(idUser.Text.ToString());
                com.Parameters.Add("@nmail", MySqlDbType.VarChar).Value = txtEmailUser.Text.ToString();
                db.openConnection();
                if (com.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Dane zostały zmienione. Zrestartuj aplikacje!");

                    errSettings.Text = "";
                    foreach (Window w in App.Current.Windows)
                        w.Close();
                }
                else
                {
                    MessageBox.Show("Dane nie zostały zmienione, sprobuj ponownie!");
                }
            }


        }
        private void changeMail_Click(object sender, RoutedEventArgs e)
        {
            errSettings.Text = " ";
            txtNameUser.Text = " ";
            txtLastNameUser.Text = " ";
            txtLoginUser.Text = " ";
            txtEmailUser.Text = " ";
            txtPassword.Password = null;
            txtNewPass.Password = null;

            changePass.Visibility = Visibility.Collapsed;
            changeLogin.Visibility = Visibility.Collapsed;
            changeDataOs.Visibility = Visibility.Collapsed;
            changeMail.Visibility = Visibility.Collapsed;



            txtBlockMail.Visibility = Visibility;
            txtEmailUser.Visibility = Visibility;
            appChangeMail.Visibility = Visibility;
            txtBlockPass.Visibility = Visibility;
            txtPassword.Visibility = Visibility;
        }




    }
}
