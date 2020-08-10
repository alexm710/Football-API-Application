using football_from_scratch_client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace football_view
{
    /// <summary>
    /// Interaction logic for UpdateTeam.xaml
    /// </summary>
    public partial class UpdateTeam : Window
    {
        CRUDManager _crudManager = new CRUDManager();

        public UpdateTeam()
        {
            InitializeComponent();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(TxtBoxId.Text);
            _ = TxtBoxId.Text;
            string name = TxtBoxName.Text;
            string stadium = TxtBoxStadium.Text;
            string location = TxtBoxLocation.Text;

            int playerid = int.Parse(TxtBoxPlayerId.Text);
            _crudManager.UpdateTeamX(id, name, stadium, location, playerid);
            MessageBox.Show("Your Player has been updated succesfully.");
            this.Close();
        }
    }
}
