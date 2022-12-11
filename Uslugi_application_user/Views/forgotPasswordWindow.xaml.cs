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

namespace Uslugi_application_user.Views
{
    /// <summary>
    /// Логика взаимодействия для forgotPasswordWindow.xaml
    /// </summary>
    public partial class forgotPasswordWindow : Window
    {
        public forgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void TextBlockBackLog_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var lgW = new loginWindow();
            this.Close();
            lgW.Show();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new loginWindow();
            loginView.Show();
            this.Close();
            /* loginView.IsVisibleChanged += (s, ev) =>
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
