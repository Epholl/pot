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
using SnakServer.Db.Util;

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
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            dataGrid.ItemsSource = db.GetAllLevelsWithScores();
        }

        private void ClearHighscores_Click(object sender, RoutedEventArgs e)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            db.ClearHighScores();
            dataGrid.ItemsSource = null;
        }

        private void ShowAllScoresButton_Click(object sender, RoutedEventArgs e)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            dataGrid.ItemsSource = db.GetAllLevelsWithScores();
        }

        private void ShowLevelHighscoresButton_Click(object sender, RoutedEventArgs e)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            dataGrid.ItemsSource = db.GetAllLevelsWithHighscores();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ServerService)SL.Get(typeof(ServerService))).StopService();
        }
    }
}
