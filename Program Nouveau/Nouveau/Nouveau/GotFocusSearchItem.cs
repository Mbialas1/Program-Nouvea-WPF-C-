using System;
using System.Linq;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Nouveau
{
    public class GotFocusSearchItem
    {

        protected static List<string> ListOfTextBox = new List<string>();
        public static void GetTextBox()
        {
            int PosFirstTextBox = 167;
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(GridItemSell => GridItemSell is GridItemSell) as GridItemSell;

            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                string query = "SELECT * FROM Sklepy;";
                ConnectSQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);

                MySqlDataReader myReader = cmd.ExecuteReader();
                ListOfTextBox.Clear();
                while (myReader.Read())
                {
                    string sname = myReader.GetString("Nazwa");
                    if (sname != "-")
                    {
                        ListOfTextBox.Add(sname);
                    }
                }
                ConnectSQL.Close();
            }

            catch (Exception e)
            {
                var error = e;
                MessageBox.Show("Nie znaleziono nic!", "Brak danych");
                return;
            }

                if (ListOfTextBox.Count > 0)
                {
                    Label[] LabelCreate = new Label[ListOfTextBox.Count];
                    TextBox[] TextBoxCreate = new TextBox[ListOfTextBox.Count];
                    for (int i = 0; i < LabelCreate.Length; i++)
                    {
                        LabelCreate[i] = new Label();
                        LabelCreate[i].Content = ListOfTextBox[i].ToString() + ":";
                        LabelCreate[i].HorizontalAlignment = HorizontalAlignment.Left;
                        LabelCreate[i].VerticalAlignment = VerticalAlignment.Top;
                        LabelCreate[i].Margin = new Thickness(10, PosFirstTextBox, 0, 0);
                        LabelCreate[i].Name = ListOfTextBox[i] + "_ShopLabel";
                        MainWin.Tab_search.Children.Add(LabelCreate[i]);

                        TextBoxCreate[i] = new TextBox();
                        TextBoxCreate[i].Text = "-";
                        TextBoxCreate[i].HorizontalAlignment = HorizontalAlignment.Left;
                        TextBoxCreate[i].VerticalAlignment = VerticalAlignment.Top;
                        TextBoxCreate[i].Margin = new Thickness(76, PosFirstTextBox, 0, 0);
                        TextBoxCreate[i].Name = ListOfTextBox[i] + "_TextBoxShop";
                        TextBoxCreate[i].IsEnabled = false;
                        MainWin.Tab_search.Children.Add(TextBoxCreate[i]);

                        PosFirstTextBox += 40;
                        MainWin.Tab_search.Height += 40;
                    }

                }

            }
        }
    }