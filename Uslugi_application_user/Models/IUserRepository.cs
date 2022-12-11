using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Uslugi_application_user.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credentital);
        bool IsUserExist(UserModel userModel);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetByUsername(string username);
        IEnumerable <UserModel> GetByAll();
        bool chekEmail(string mail);
        void resetPasswd(string pass, string mail);
        void sendMail(string mail, string pass, MailMessage mailMess);
        bool chekUsername(string username);
        void DeleteAllReservationData(DateTime dataNow);
        bool chekPassAndRepPass(SecureString pass, SecureString passRep);
    }
}
