using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Windows;
using System.Data;

namespace Nouveau
{
    public class Shop_Setting
    {
        public static void ShopSettingCreate(string name)
        {
            if (name.Length > 3)
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                try
                {
                    ConnectSQL.Open();
                    MySqlCommand cmdacc = new MySqlCommand("SELECT * FROM Sklepy WHERE Nazwa='" + name + "';", ConnectSQL);
                    MySqlDataReader readerCheck = cmdacc.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(readerCheck);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Nazwa sklepu już istnieje !", "Zła nazwa konta");
                        ConnectSQL.Close();
                        return;
                    }
                    string query = "BEGIN; INSERT INTO Sklepy (Nazwa) VALUES ('" + name + "');" +
                        " CREATE TABLE " + name + "_Towar (ID INT, Data DATETIME,Nazwa TEXT,Kategoria TEXT, Netto DOUBLE,Brutto DOUBLE,Ilość INT,Rozmiar TEXT, Opis TEXT, PRIMARY KEY(ID)); " +
                        " CREATE TABLE " + name + "_Sprzedaz (Numer_sprzedazy TEXT, Data DATETIME,Nazwa TEXT,Kategoria TEXT, Netto DOUBLE,Brutto DOUBLE,Sprzedawca TEXT,Rozmiar TEXT, Opis TEXT); COMMIT;";
                    MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    ConnectSQL.Close();
                    MessageBox.Show("Done!", "-");
                }
                catch (Exception e)
                {
                    var error = e;
                    MessageBox.Show(error.ToString());
                }
            }
            else
            {
                MessageBox.Show("Za mało znaków w nazwie sklepu!", "Błąd w nazwie sklepu");
            }
        }
        public static void ShopSettingDelete(string name)
        {
            string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
            MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
            try
            {
                ConnectSQL.Open();
                string query = "BEGIN; DELETE FROM Sklepy WHERE Nazwa='" + name + "';" +
                    " DROP TABLE " + name + "_Sprzedaz;" + " DROP TABLE " + name + "_Towar; COMMIT;";
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);
                MySqlDataReader reader = cmd.ExecuteReader();
                ConnectSQL.Close();
                MessageBox.Show("Done!", "-");
            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
            }
        }
    }
    }
