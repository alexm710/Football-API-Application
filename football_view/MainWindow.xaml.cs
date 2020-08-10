using football_from_scratch_client;
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

namespace football_view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CRUDManager _clientController = new CRUDManager();
        //private UpdatePlayer _updatePlayerX = new UpdatePlayer();
        public MainWindow()
        {
            InitializeComponent();
            _clientController.GetAllPlayers();
            _clientController.GetAllTeams();
            PopulatePlayers();
            PopulateTeams();

        }

        private void ButtonAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            AddPlayer addPlayer = new AddPlayer();
            addPlayer.Show();
            _clientController.GetAllPlayers();
            PopulatePlayers();
        }

        private void PopulatePlayers()
        {
            ListBoxFootballPlayers.ItemsSource = _clientController.players;
        }

        private void ButtonDeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxFootballPlayers.SelectedItem != null)
            {
                Player player = ListBoxFootballPlayers.SelectedItem as Player;
                _clientController.DeletePlayerAsync(player.PlayerId);
                MessageBox.Show("Your player has been deleted succesfully.");
                _clientController.GetAllPlayers();
                PopulatePlayers();
            }
        }

        private void ButtonUpdatePlayer_Click(object sender, RoutedEventArgs e)
        {
            UpdatePlayer updatePlayer = new UpdatePlayer();
            updatePlayer.ShowDialog();
            if (ListBoxFootballPlayers.SelectedItem != null)
            {
                Player player = ListBoxFootballPlayers.SelectedItem as Player;
                _clientController.GetAllPlayers();
                PopulatePlayers();
            }
        }

        // Teams CRUD

        private void PopulateTeams()
        {
            ListBoxFootballTeams.ItemsSource = _clientController.teams;
        }

        private void ButtonDeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxFootballTeams.SelectedItem != null)
            {
                Team team = ListBoxFootballTeams.SelectedItem as Team;
                _clientController.DeleteTeamAsync(team.TeamId);
                MessageBox.Show("Your team has been deleted succesfully.");
                _clientController.GetAllTeams();
                PopulateTeams();
            }
        }

        private void ButtonUpdateTeam_Click(object sender, RoutedEventArgs e)
        {
            UpdateTeam updateTeam = new UpdateTeam();
            updateTeam.ShowDialog();
            if (ListBoxFootballPlayers.SelectedItem != null)
            {
                Team team = ListBoxFootballPlayers.SelectedItem as Team;
                _clientController.GetAllTeams();
                PopulateTeams();
            }

        }

        private void ButtonAddTeam_Click(object sender, RoutedEventArgs e)
        {
            AddTeam addTeam = new AddTeam();
            addTeam.Show();
            _clientController.GetAllTeams();
            PopulateTeams();
        }
    }
}