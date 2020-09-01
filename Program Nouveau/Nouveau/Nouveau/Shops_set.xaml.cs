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
using MySql.Data.MySqlClient;
using System.Windows.Controls;

namespace Nouveau
{
    /// <summary>
    /// Logika interakcji dla klasy Shops_set.xaml
    /// </summary>
    public partial class Shops_set : Window
    {
        public Shops_set()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SetComboBox();
        }
        private void SetComboBox()
        {
            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                string query = "SELECT * FROM Sklepy;";
                ConnectSQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);

                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString("Nazwa");
                    if(sname != "-")
                    {
                        comboBox.Items.Add(sname);
                    }                  
                }
                ConnectSQL.Close();

            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
            }
        }

        private void Button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(comboBox.SelectedItem == null)
            {
                MessageBox.Show("Nie wybrałeś żadnego sklepu.", "Błąd wyboru");
                return;
            }

            string query = "SELECT * FROM " + comboBox.SelectedItem.ToString() + "_Sprzedaz;";
            DownloadFromBase.UpdateBase(query);
            this.Close();
        }
    }
}
