using System;
using System.Linq;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Nouveau
{
   public class GotFocusTowar : Settings
    {
        static protected List<string> ListOfTextBox = new List<string>();
        public static void SetTabIteam()
        {
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
                //GotFocusTowar GetThisClass = new GotFocusTowar();
                GotFocusTowar.CreateTextBox();
            }
            catch (Exception e)
            {
                var error = e;
                MessageBox.Show(error.ToString());
            }
        }
        private static void CreateTextBox()
        {
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(Settings => Settings is Settings) as Settings;
                int PosFirstTextBox = 392 + 30;
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
                        LabelCreate[i].Name = ListOfTextBox[i] + "_LabelCountTowar"; 
                        MainWin.GridTowar.Children.Add(LabelCreate[i]);

                        TextBoxCreate[i] = new TextBox();
                        TextBoxCreate[i].Text = "0";
                        TextBoxCreate[i].HorizontalAlignment = HorizontalAlignment.Left;
                        TextBoxCreate[i].VerticalAlignment = VerticalAlignment.Top;
                        TextBoxCreate[i].Margin = new Thickness(76, PosFirstTextBox, 0, 0);
                        TextBoxCreate[i].Name = ListOfTextBox[i] + "_TextBoxCountTowar";
                        MainWin.GridTowar.Children.Add(TextBoxCreate[i]);

                        PosFirstTextBox += 40;
                        MainWin.GridTowar.Height += 40;
                    }

                    MainWin.CreateNewTowar.Content = "Dodaj towar";
                    MainWin.CreateNewTowar.HorizontalAlignment = HorizontalAlignment.Left;
                    MainWin.CreateNewTowar.VerticalAlignment = VerticalAlignment.Top;
                    MainWin.CreateNewTowar.Margin = new Thickness(20, PosFirstTextBox + 30, 0, 0);
                    MainWin.GridTowar.Height += 40;
                }
        }
    }

}
