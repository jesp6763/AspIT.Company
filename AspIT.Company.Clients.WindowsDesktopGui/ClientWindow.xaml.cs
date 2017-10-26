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
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using AspIT.Company.Clients.Communications;

namespace AspIT.Company.Clients.WindowsDesktop.Gui
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginHandler.Logout();
            Application.Current.MainWindow.Show();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SendUserBtn_Click(object sender, RoutedEventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            NetworkStream stream = ConnectionHandler.CurrentConnection.GetStream();
            formatter.Serialize(stream, LoginHandler.CurrentUser);
        }
    }
}
