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
    /// Interaction logic for AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Window
    {
        CRUDManager _crudManager = new CRUDManager();
        public AddPlayer()
        {
            InitializeComponent();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(TxtBoxId.Text);
            _ = TxtBoxId.Text;

            int age = int.Parse(TxtBoxAge.Text);
            _ = TxtBoxId.Text;


            string name = TxtBoxName.Text;
            string position = TxtBoxPosition.Text;
            //string age = TxtBoxAge.Text;
            string team = TxtBoxTeamName.Text;
            _crudManager.PostPlayer(id, name, position, age, team);
            MessageBox.Show("Your Player has been updated succesfully.");
            this.Close();
        }
    }
}
