using System;
using System.Linq;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Controls;

namespace Nouveau
{
   public class TabItemGotFocus
    {
        public static void User_Focus(ComboBox combo)
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
                    combo.Items.Add(sname);
                }
                    ConnectSQL.Close();
                
            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
            }
        }
        public static void User_Focus_Delete(ComboBox combo)
        {
            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                string query = "SELECT * FROM Login;";
                ConnectSQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);
                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString("Login");
                    combo.Items.Add(sname);
                }
                ConnectSQL.Close();

            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
            }
        }
    }
}
