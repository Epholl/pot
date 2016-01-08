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
using SnakServer.Db;

namespace SnakServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            ((ServerService) SL.Get(typeof(ServerService))).StartService();
            SL.Register(this);
        }

        private void createInstanceButton_Click(object sender, RoutedEventArgs e)
        {
            //runningInstancesLabel.Content = databaseClient.GetDbContext().Players.Select(p => p).ToList().First().name;
            var databaseClient = (DatabaseService) SL.Get(typeof(DatabaseService));
            var result = databaseClient.GetAllLevelsWithHighScores();
            runningInstancesLabel.Content = result.Count + ", " + result.First().PlayerName + ": " + result.First().Score + ", " + result.First().LevelName;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ServerService) SL.Get(typeof(ServerService))).StopService();
        }
    }
}
