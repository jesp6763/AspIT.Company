﻿using System;
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
                    Hide();
                    MessageBox.Show("Login successful!");
                    break;
                case LoginResult.UserDoesNotExist:
                    MessageBox.Show("User does not exist!");
                    break;
                case LoginResult.WrongUsernameOrPassword:
                    MessageBox.Show("Wrong username or password!");
                    break;
                case LoginResult.ServerRefusedClient:
                    MessageBox.Show("Client connection refused by server!");
                    break;
            }
        }
    }
}
