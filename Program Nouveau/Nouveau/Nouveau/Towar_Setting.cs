using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Nouveau
{
    public class Towar_Setting : GotFocusTowar
    {
        public static void AddNewTowar()
        {
            string[] TextBoxTabReady = { "textBox_ID", "textBox_Nazwa", "textBox_Kategoria", "textBox_Netto", "textBox_Brutto", "textBox_Rozmiar", "textBox_Opis", "textBox_MagazynIlosc" };
            List<string> ListTextBoxDesign = new List<string>(TextBoxTabReady);
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(Settings => Settings is Settings) as Settings;
            for (int i=0;i<ListOfTextBox.Count;i++) // For, który wpisuje wygenerowane textboxy sklepów z bazy danych
            {
                    ListTextBoxDesign.Add(ListOfTextBox[i]+"_TextBoxCountTowar");
            }

            var FindObject = MainWin.GridTowar.Children.OfType<TextBox>().ToList();
            List<string> ListOfIteamToShop = new List<string>();

            for (int i=0;i<ListTextBoxDesign.Count; i++) //For, który sprawdza czy nie pozostał żaden TextBox pusty
            {
                if (FindObject[i].Text == "" || FindObject[i].Text == " ")
                {
                    MessageBox.Show("Któreś z pól pozostało puste.", "Błąd!");
                    return;
                }
                ListOfIteamToShop.Add(FindObject[i].Text.ToString());
            }

            string query = "BEGIN; ";
            int j = -1;
            for (int i = 7; i < ListTextBoxDesign.Count; i++)
            {
                if (i == 7 && Int32.Parse(FindObject[i].Text) > 0)
                {
                    DateTime aadata = DateTime.Now;
                    query += "INSERT INTO Magazyn (ID,Data,Nazwa,Kategoria,Netto,Brutto,Ilość,Rozmiar,Opis) VALUES ('"+ListOfIteamToShop[0]+"',NOW(),'" + ListOfIteamToShop[1] + "','" + ListOfIteamToShop[2] + "','" + ListOfIteamToShop[3].Replace(',','.') + "','" + ListOfIteamToShop[4].Replace(',','.') +"','"+FindObject[i].Text+"','" + ListOfIteamToShop[5] + "','" + ListOfIteamToShop[6] + "'); ";
                }
                else if (i > 7 && Int32.Parse(FindObject[i].Text) > 0)
                {
                    query += "INSERT INTO " + ListOfTextBox[j] + "_Towar" + " (Id,Data,Nazwa,Kategoria,Netto,Brutto,Ilość,Rozmiar,Opis) VALUES ('"+ListOfIteamToShop[0]+"',NOW(),'" + ListOfIteamToShop[1] + "','" + ListOfIteamToShop[2] + "','" + ListOfIteamToShop[3].Replace(',','.') + "','" + ListOfIteamToShop[4].Replace(',','.') + "','"+FindObject[i].Text+"','" + ListOfIteamToShop[5] + "','" + ListOfIteamToShop[6] + "'); ";
                }
                j++;
            }
           query += "COMMIT;";
            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                ConnectSQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);
                MySqlDataReader myReader = cmd.ExecuteReader();
                MessageBox.Show("Towar został dodany.", "Dodano towar!");
                ConnectSQL.Close();
            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(e.ToString(),"Nie powiodła się transakcja");
            }
        }
    }
}
