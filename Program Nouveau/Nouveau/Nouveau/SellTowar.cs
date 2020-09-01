using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Nouveau
{
    public class SellTowar
    {
        public static void Sell(DataRowView row_selected, string nr_sprze, string netto,string brutto,string opis)
        {
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(GridItemSell => GridItemSell is GridItemSell) as GridItemSell;
            int ilosc = Int32.Parse(row_selected["Ilość"].ToString()) - 1;
            if(opis.Length < 0)
            {
                opis = "-";
            }
            string query = "UPDATE "+MainWindow.ShopName+"_Towar SET Ilość="+ilosc+" WHERE ID="+row_selected["Id"].ToString()+"; ";
            query += "INSERT INTO " + MainWindow.ShopName + "_Sprzedaz (Numer_sprzedazy,Data,Nazwa,Kategoria,Netto,Brutto,Sprzedawca,Rozmiar,Opis) VALUES ('" + nr_sprze + "',NOW(),'" + row_selected["Nazwa"].ToString() + "','" + row_selected["Kategoria"].ToString() + "','" + netto.Replace(',', '.') + "','" + brutto.Replace(',', '.') + "','" + MainWindow.NameLogin+"','"+row_selected["Rozmiar"].ToString()+"','"+opis+"');" ;
            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                ConnectSQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);
                MySqlDataReader reader = cmd.ExecuteReader();
                ConnectSQL.Close();
            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
                return;
            }
            MessageBox.Show("Sprzedano prawidłowo towar. Odśwież listę.");
            MainWin.Close();
        }
    }
}
