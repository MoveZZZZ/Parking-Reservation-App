using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Uslugi_application_user.ViewModels;
using Uslugi_application_user.Views;


namespace Uslugi_application_user
{

    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
           // var logVM = new LoginViewModel();
            var loginView = new loginWindow();
            loginView.Show();
/*            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new mWindow();
                    mainView.Show();
                    loginView.Close();
                }
            };*/
        }
    }
}
