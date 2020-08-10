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
    /// Interaction logic for AddTeam.xaml
    /// </summary>
    public partial class AddTeam : Window
    {
        CRUDManager _crudManager = new CRUDManager();

        public AddTeam()
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

            //int playerid = int.Parse(TxtBoxPlayerId.Text);
            _crudManager.PostTeam(id, name, stadium, location);
            MessageBox.Show("Your Team has been updated succesfully.");
            this.Close();
        }
    }
}
