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
using System.Windows.Forms;
using System.IO;
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
            System.Windows.Application.Current.MainWindow.Show();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SendUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if(imagePath.Text != string.Empty)
            {
                // Get image file bytes
                byte[] imageBytes = File.ReadAllBytes(imagePath.Text);

                // Send bytes to server
                ConnectionHandler.CurrentConnection.Client.Send(imageBytes);
                System.Windows.MessageBox.Show("Image has been sent");
            }
        }

        private void ImageBrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            imagePath.Text = openFileDialog.FileName;
        }
    }
}
