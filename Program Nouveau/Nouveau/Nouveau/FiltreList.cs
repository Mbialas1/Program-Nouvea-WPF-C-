using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace Nouveau
{
    public class FiltreList
    {
        public static void SetList(string combo1, string combo2, string combo3,string text1,string text2, string text3)
        {
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            string[] ComboTab = { combo1, combo2, combo3 };
            string[] TextTab = { text1, text2, text3 };
            string query = "SELECT * FROM " + MainVarriables.NameGrid + " WHERE ";
            bool FirstCheck = false;
            for (int i=0;i<ComboTab.Length;i++)
            {
                if(ComboTab[i] != "Brak")
                {
                    if (!FirstCheck)
                    {
                        if(ComboTab[i] == "Cena Brutto")
                        {
                            double example = Convert.ToDouble(TextTab[i].Replace('.',','));
                            query += "Brutto" + "='" + example.ToString().Replace(',', '.') + "'";
                        }
                        else
                        {
                            query += ComboTab[i] + "='" + TextTab[i] + "'";
                        }     
                        FirstCheck = true;
                    }
                    else
                    {
                        if (ComboTab[i] == "Cena Brutto")
                        {
                            double example = Convert.ToDouble(TextTab[i].Replace('.', ','));
                            query += "AND WHERE Brutto" + "='" + example.ToString().Replace(',', '.') + "'";
                        }
                        else
                        {
                            query += " AND WHERE " + ComboTab[i] + "='" + TextTab[i] + "'";
                        }
                    }
                }
            }
            query += ";";
            try
            {
                DownloadFromBase.UpdateBase(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie znaleziono elementów", "Błąd w wyszukiwaniu");
            }
        }
    }
}
