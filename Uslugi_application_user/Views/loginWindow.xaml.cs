using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Uslugi_application_user.ViewModels;

namespace Uslugi_application_user.Views
{
    /// <summary>
    /// Логика взаимодействия для loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        public loginWindow()
        {
            InitializeComponent();
        }

        private void TextBlockRegister_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var rWin = new RegistrationWindow();
            this.Close();
            rWin.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TextBlockForgotPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var forWin = new forgotPasswordWindow();
            this.Close();
            forWin.Show();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            this.IsVisibleChanged += (s, ev) =>
            {
            var mainView = new mWindow();
            if (this.IsVisible == false && this.IsLoaded)
            {

                mainView.Show();
                this.Close();
            }
            };

        }
    }
}
