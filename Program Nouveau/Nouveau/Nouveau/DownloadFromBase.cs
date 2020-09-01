using System;
using System.Linq;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
namespace Nouveau
{
    public class DownloadFromBase : MainWindow
    {
        public static void UpdateBase(string query)
        {
            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                ConnectSQL.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, ConnectSQL))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                    MainWin.dataGrid.ItemsSource = dt.DefaultView;
                    ConnectSQL.Close();
                }
            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
            }
        }
    }
}
