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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AspIT.Company.Common.Entities;
using AspIT.Company.Clients.Communications.Enums;
using AspIT.Company.Clients.Communications;

namespace AspIT.Company.Clients.WindowsDesktop.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            // Attempt to login user
            LoginResult result = LoginHandler.AttemptLogin(new User(usernameTb.Text, passwordBox.Password));

            switch(result)
            {
                case LoginResult.Success:
                    ClientWindow clientWin = new ClientWindow();
                    clientWin.Show();
                    // Hide login window
                    Hide();
                    break;
                case LoginResult.UserDoesNotExist:
                    statusLbl.Content = "User does not exist!";
                    break;
                case LoginResult.WrongUsernameOrPassword:
                    statusLbl.Content = "Wrong username or password!";
                    break;
                case LoginResult.ServerRefusedClient:
                    statusLbl.Content = "Client connection refused by server!";
                    break;
                case LoginResult.UserAlreadyLoggedIn:
                    statusLbl.Content = "User is already logged in. Please try again later";
                    break;
            }
        }
    }
}
