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
    /// The single window of the server app, allowing basic DB info and highscore reset
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// The constructor initializes gui components and ensures service instances are initialized and present in the SL system
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ((ServerService) SL.Get(typeof(ServerService))).StartService();
            SL.Register(this);
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            dataGrid.ItemsSource = db.GetAllLevelsWithScores();
        }

        /// <summary>
        /// The button callback to remove all highscores from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearHighscores_Click(object sender, RoutedEventArgs e)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            db.ClearHighScores();
            dataGrid.ItemsSource = null;
        }

        /// <summary>
        /// This button causes the app window to show all scores. A new highscore added needs manual refresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAllScoresButton_Click(object sender, RoutedEventArgs e)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            dataGrid.ItemsSource = db.GetAllLevelsWithScores();
        }

        /// <summary>
        /// This button causes the app to show all highscores for every level. A new highscore added needs manual refresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowLevelHighscoresButton_Click(object sender, RoutedEventArgs e)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            dataGrid.ItemsSource = db.GetAllLevelsWithHighscores();
        }

        /// <summary>
        /// Upon window close the server service closes to prevent DB connection leaks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ServerService)SL.Get(typeof(ServerService))).StopService();
        }
    }
}
