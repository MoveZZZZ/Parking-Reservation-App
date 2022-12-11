using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Uslugi_application_user.Models;
using Uslugi_application_user.Repositories;

namespace Uslugi_application_user.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserModel _currentAccount;
        private ViewModelBase _currenChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;
        public string StringPass
        {
            get
            {
                return new NetworkCredential("", CurrentUserAccount.Password).Password;
            }

        }
        public UserModel CurrentUserAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currenChildView;
            }
            set
            {
                _currenChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }

        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }


        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowReservationViewCommand { get; }
        public ICommand ShowListReservationViewCommand { get; }
        public ICommand ShowSettingsViewCommnad { get; }
        public ICommand ShowFAQViewCommand { get; }


        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserModel();
            userRepository.DeleteAllReservationData(DateTime.Now);
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowReservationViewCommand = new ViewModelCommand(ExecuteShowReservationCommand);
            ShowListReservationViewCommand = new ViewModelCommand(ExecuteShowListReservayionCommand);
            ShowSettingsViewCommnad = new ViewModelCommand(ExecuteShowSettingsCommand);
            ShowFAQViewCommand = new ViewModelCommand(ExecuteShowFAQCommand);
            ExecuteShowHomeViewCommand(null);
            LoadCurrentUserData();
        }

        private void ExecuteShowFAQCommand(object obj)
        {
            CurrentChildView = new FAQViewModel();
            Caption = "FAQ";
            Icon = IconChar.Question;
        }

        private void ExecuteShowSettingsCommand(object obj)
        {
            CurrentChildView = new SettingsViewModel();
            Caption = "Ustawienia";
            Icon = IconChar.Gears;
        }

        private void ExecuteShowListReservayionCommand(object obj)
        {
            CurrentChildView = new ShowReservationListModel();
            Caption = "Lista rezerwacji";
            Icon = IconChar.List;
        }

        private void ExecuteShowReservationCommand(object obj)
        {
            CurrentChildView = new ReservationViewModel();
            Caption = "Dodaj rezerwację";
            Icon = IconChar.Car;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Menu główne";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user !=null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.Id = user.Id;
                CurrentUserAccount.Email = user.Email;
                CurrentUserAccount.Name = user.Name;
                CurrentUserAccount.LastName = user.LastName;
                CurrentUserAccount.Password = user.Password;
                CurrentUserAccount.DisplayName = $"Witam {user.Name} {user.LastName}" +"!";
               
            }
            else
            {
                CurrentUserAccount.DisplayName = "User not logged in";
            }
        }
    }
}
