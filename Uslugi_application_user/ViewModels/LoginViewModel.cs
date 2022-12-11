using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Uslugi_application_user.Models;
using Uslugi_application_user.Repositories;

namespace Uslugi_application_user.ViewModels
{
   public class LoginViewModel: ViewModelBase
    {
        //logIN
        private string _username;
        private SecureString _password;
        private SecureString _passwordRep;
        private string _errorMessage;
        private string _errorMessage2;
        private bool _isViewVisible = true;
        private string _name;
        private string _lastname;
        private string _mail;
        private string _errorMessageReg;
        private string _errorMessageReset;
        private string _errorMessageReset2;
        private bool _isViewRegVisible = false;
        private bool _isViewRecoverVisible = false;




        //LOGIN
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString PasswordRep
        {
            get
            {
                return _passwordRep;
            }
            set
            {
                _passwordRep = value;
                OnPropertyChanged(nameof(PasswordRep));
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }        
        public string ErrorMessage2
        {
            get
            {
                return _errorMessage2;
            }
            set
            {
                _errorMessage2 = value;
                OnPropertyChanged(nameof(ErrorMessage2));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
                OnPropertyChanged(nameof(Mail));
            }
        }
        public string ErrorMessageRegistration
        {
            get
            {
                return _errorMessageReg;
            }
            set
            {
                _errorMessageReg = value;
                OnPropertyChanged(nameof(ErrorMessageRegistration));
            }
        }
        public bool IsViewRegistrationVisible
        {
            get
            {
                return _isViewRegVisible;
            }
            set
            {
                _isViewRegVisible = value;
                OnPropertyChanged(nameof(IsViewRegistrationVisible));
            }
        }
        public bool IsViewRecoverVisible
        {
            get
            {
                return _isViewRecoverVisible;
            }
            set
            {
                OnPropertyChanged(nameof(IsViewRecoverVisible));
            }
        }
        public string ErrorMessageReset
        {
            get
            {
                return _errorMessageReset;
            }
            set
            {
                _errorMessageReset = value;
                OnPropertyChanged(nameof(ErrorMessageReset));
            }
        }       
        public string ErrorMessageReset2
        {
            get
            {
                return _errorMessageReset2;
            }
            set
            {
                _errorMessageReset = value;
                OnPropertyChanged(nameof(ErrorMessageReset2));
            }
        }
        //Commands 
        public ICommand LoginComand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand CreateAccoutCommand { get; }


        private IUserRepository userRepository;
        //Constructor

        public LoginViewModel()
        {
/*            ErrorMessageRegistration = "Niepoprawne dane!";
            ErrorMessageReset = "Niepoprawna poczta!";
            ErrorMessage = "Niepoprawne dane!";*/
            userRepository = new UserRepository();
            LoginComand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            CreateAccoutCommand = new ViewModelCommand(ExecuteCreateAccountCommand, CanExecuteCreateAccountCommand);
            RecoverPasswordCommand = new ViewModelCommand(ExecuteResetPasswdCommand, CanExecuteResetPassword);
            
        }

        private bool CanExecuteResetPassword(object obj)
        {
            bool validMail;
            if(Mail==null)
            {
                validMail = false;
                ErrorMessageReset = "Poczta nie może być pusta!";
            }
            else if (!userRepository.chekEmail(Mail))
            {
                validMail = false;
                ErrorMessageReset = "Taka poczta nie jest zarejstrowana!";
            }
            else
            {
                validMail = true;
                ErrorMessageReset = "";
            }
            return validMail;
        }
        private void ExecuteResetPasswdCommand(object obj)
        {
            if(userRepository.chekEmail(Mail))
            {
                MailMessage mailM = new MailMessage();
                const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()-+_/.,;:";
                StringBuilder sb = new StringBuilder();
                Random rand = new Random();
                for(int i=0; i<=10; i++)
                {
                    int ind = rand.Next(chars.Length);
                    sb.Append(chars[ind]);
                }
                userRepository.sendMail(Mail, sb.ToString(), mailM);
                userRepository.resetPasswd(sb.ToString(), Mail);
                IsViewRecoverVisible = false;
            }
            else
            {
                ErrorMessageReset = "* Invalid email!";
            }
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;

            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Login nie może być pusty!!";
                validData = false;
            }
            else if(Password == null )
            {
                ErrorMessage = "Hasło nie może być puste!";
                validData = false;
            }
            else 
            {
                ErrorMessage = "";
                validData = true;
            }
            return validData;
        }
        private void ExecuteLoginCommand(object obj)
        {
            if (userRepository.AuthenticateUser(new NetworkCredential(Username, Password)))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage2 = "Zły login lub hasło!!";
            }
        }
        private bool CanExecuteCreateAccountCommand(object obj)
        {
            bool validCreate;
            string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(Name) || Name.Length < 3)
            {
                ErrorMessageRegistration = "Niepoprawne imię! Powinno być > 3 znaków!";
                validCreate = false;
            }
            else if (string.IsNullOrEmpty(LastName) || LastName.Length < 3)
            {
                ErrorMessageRegistration = "Niepoprawne nazwisko! Powinno być > 3 znaków!";
                validCreate = false;
            }
            else if (Mail == null || !r.Match(Mail).Success)
            {
                ErrorMessageRegistration = "Niepoprawna poczta!";
                validCreate = false;
            }

            else if (Mail != null&& userRepository.chekEmail(Mail))
            {
                ErrorMessageRegistration = "Taka poczta już jest zarejstrowana!";
                validCreate = false;
            }
            else if (string.IsNullOrEmpty(Username) || Username.Length < 3)
            {
            
                ErrorMessageRegistration = "Niepoprawny login! Login powinny być > 3 znaków";
                validCreate = false;
                
            }
            else if (userRepository.chekUsername(Username))
            {
                ErrorMessageRegistration = "Taki użytkownik już jest zarejstrowany!";
                validCreate = false;
            }
            else if(Password == null || Password.Length < 6 || PasswordRep == null || PasswordRep.Length < 6)
            {
                ErrorMessageRegistration = "Niepoprawne oba hasła! Hasła powinny być > 6 znaków";
                validCreate = false;
            }
            else
            {
                if (!userRepository.chekPassAndRepPass(Password, PasswordRep))
                {
                    ErrorMessageRegistration = "Niepoprawne drugie hasło!";
                    validCreate = false;
                }
                else
                {
                    validCreate = true;
                    ErrorMessageRegistration = "";
                } 
            }
            return validCreate;
        }
        private void ExecuteCreateAccountCommand(object obj)
        {
            UserModel userModel = new UserModel();
            userModel.Username = Username;
            userModel.Password = Password;
            userModel.Name = Name;
            userModel.LastName = LastName;
            userModel.Email = Mail;
            var isValidRegistration = userRepository.IsUserExist(userModel);
            if (!isValidRegistration)
            {
                userRepository.Add(userModel);
                IsViewRegistrationVisible = false;
            }
            else
            {
                ErrorMessageRegistration = "* Bad data!";
            }
        }
    }
}
