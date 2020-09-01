using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Nouveau
{
    public class SearchBaseForItem : GotFocusSearchItem
    {
        
        public static List<string> List_NameBox = new List<string> { "Nazwa", "Kategoria", "Netto", "Rozmiar" };
        static byte CountTimeError = 0;
        public static void SearchBase()
        {
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(GridItemSell => GridItemSell is GridItemSell) as GridItemSell;
            var FindObject = MainWin.Tab_search.Children.OfType<TextBox>().ToList();
            string[] query = new string[FindObject.Capacity];
            CountTimeError = 0;
            query[0] += "SELECT * FROM Magazyn WHERE (";
            for (int i = 0; i < GridItemSell.List_CheckBox.Count; i++)
            {
                if (GridItemSell.List_CheckBox[i] == true)
                {
                    string check = query[0];
                    char last = check[check.Length - 1];
                    if (last == '(')
                    {
                        query[0] += "" + List_NameBox[i] + "='" + GridItemSell.RowForThisClass[List_NameBox[i]] + "' ";
                    }
                    else
                    {
                        query[0] += "AND " + List_NameBox[i] + "='" + GridItemSell.RowForThisClass[List_NameBox[i]] + "' ";
                    }
                }
                else
                {
                    CountTimeError++;
                }
            }
            if (CountTimeError > 3)
            {
                MessageBox.Show("Nie zaznaczono po czym mam szukać.", "Błąd!");
                return;
            }
            query[0] += "); ";

            if (ListOfTextBox.Capacity > 0)
            {
                for (int i = 0; i < ListOfTextBox.Count; i++)
                {
                    query[i+1] += "SELECT * FROM " + ListOfTextBox[i] + "_Towar WHERE(";
                    for (int j = 0; j < GridItemSell.List_CheckBox.Count; j++)
                    {
                        if (GridItemSell.List_CheckBox[j] == true)
                        {
                            string check = query[i+1];
                            char last = check[check.Length - 1];
                            if (last == '(')
                            {
                                query[i+1] += "" + List_NameBox[j] + "='" + GridItemSell.RowForThisClass[List_NameBox[j]] + "' ";
                            }
                            else
                            {
                                query[i+1] += "AND " + List_NameBox[j] + "='" + GridItemSell.RowForThisClass[List_NameBox[j]] + "' ";
                            }
                        }
                    }
                    query[i+1] += "); ";
                }
            }
            int counttime = 0;
            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                ConnectSQL.Open();
                for (int i = 0; i < FindObject.Count; i++)
                {
                    counttime = 0;
                    int CountItema = 0;

                    MySqlCommand cmd = new MySqlCommand(query[i], ConnectSQL);

                    MySqlDataReader myReader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(myReader);

                    do
                    {
                        DataRow dw = dt.Rows[counttime];
                        CountItema += Int32.Parse(dw[6].ToString());
                        counttime++;
                    } while (counttime < dt.Rows.Count);


                    FindObject[i].Text = CountItema.ToString();
                   
                }

                ConnectSQL.Close();
            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
                return;
            }
        }
    }
}
