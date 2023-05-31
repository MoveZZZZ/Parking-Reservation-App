using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Uslugi_application_user.Models;

namespace Uslugi_application_user.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                string a = BCrypt.Net.BCrypt.HashPassword(SecureStringToString(userModel.Password));
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO users(`login`, `pass`, `name`, `surname`, `mail`) VALUES(@login, @password, @name, @surname, @m)";
                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = userModel.Username;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value =a;
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userModel.Name;
                command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userModel.LastName;
                command.Parameters.Add("@m", MySqlDbType.VarChar).Value = userModel.Email;
                command.ExecuteNonQuery();
            }
        }
        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public bool chekPassAndRepPass(SecureString pass, SecureString passRep)
        {
            string p = SecureStringToString(pass);
            string p2 = SecureStringToString(passRep);
            if (p == p2)
                return true;
            else
                return false;

        }

        public bool AuthenticateUser(NetworkCredential credentital)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                string passUserDB="";
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM `users` WHERE `login` = @username";
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = credentital.UserName;
                MySqlDataReader data = command.ExecuteReader();
                if(data.HasRows)
                {
                    while (data.Read())
                    {
                        passUserDB = Convert.ToString(data.GetValue(2));
                    }
                    
                }
               try
                {
                    if (BCrypt.Net.BCrypt.Verify(Convert.ToString(credentital.Password), passUserDB))
                    {
                        validUser = true;
                    }
                    else
                    {
                        validUser = false;
                    }
                }
                catch
                {
                    validUser = false;
                }

            }
            return validUser;
        }
        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
        public UserModel GetByUsername(string username)
        {
            UserModel umod = new UserModel();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM `users` WHERE `login` = @log";
                command.Parameters.Add("@log", MySqlDbType.VarChar).Value = username;
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                umod.Id = row[0].ToString();
                umod.Username= row[1].ToString();
                umod.Password = new NetworkCredential("", row[2].ToString()).SecurePassword;
                umod.Name = row[3].ToString();
                umod.LastName = row[4].ToString();
                umod.Email = row[5].ToString();
            }
            return umod;
        }
        public bool IsUserExist(UserModel userModel)
        {
            bool existUser;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM `users` WHERE `login` = @username";
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = userModel.Username;
                //command.ExecuteNonQuery();
                existUser = command.ExecuteScalar() == null ? false : true;
            }
            return existUser;
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public bool chekEmail(string mail)
        {
            bool chekMail;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT `mail` FROM `users` WHERE `mail` = @m";
                command.Parameters.Add("@m", MySqlDbType.VarChar).Value = mail;
                chekMail = command.ExecuteScalar() == null ? false : true;
            }
                return chekMail;
        }
        public void resetPasswd(string pass, string mail)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                string a = BCrypt.Net.BCrypt.HashPassword(pass);
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE users SET pass=@npass WHERE mail = @m";
                command.Parameters.Add("@m", MySqlDbType.VarChar).Value = mail;
                command.Parameters.Add("@npass", MySqlDbType.VarChar).Value = a;
                command.ExecuteNonQuery();
            }
        }
        public void sendMail(string mail, string pass, MailMessage mailMess)
        {
            using (mailMess)
            {
                mailMess.From = new MailAddress("srmp.app@gmail.com");
                mailMess.To.Add(mail);
                mailMess.Subject = "RESET PASSWORD";
                mailMess.Body = "<h1>Your new password is: " + pass + "</h1>";
                mailMess.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("srmp.app@gmail.com", "gdqmnzokxrktxbok");
                    smtp.EnableSsl = true;
                    smtp.Send(mailMess);
                }
            }
        }
        public bool chekUsername(string name)
        {
            bool isUsed;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT `login` FROM `users` WHERE login=@clogin";
                command.Parameters.Add("@clogin", MySqlDbType.VarChar).Value = name;
                isUsed = command.ExecuteScalar() == null ? false : true;
            }
            return isUsed;
        }
        public void DeleteAllReservationData(DateTime dataNow)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM `userpark` WHERE `endres` <= @datanow";
                DateTime d = Convert.ToDateTime(dataNow.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.Add("@datanow", MySqlDbType.DateTime).Value = dataNow;
                command.ExecuteNonQuery();
            }
        }
    }
}
