using SnakServer.Service;
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
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SnakServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerService networkClient;

        public MainWindow()
        {
            InitializeComponent();
            networkClient = new ServerService();
            networkClient.StartService();
        }

        private void createInstanceButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // close db and wcf connections
            networkClient.StopService();
        }
    }
}
