using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace Nouveau
{
    public class Sklep : MainWindow
    {
        public static void SetSklep()
        {
            MainVarriables.NameGrid = ShopName+"_Towar";
            string query = "SELECT * FROM " + ShopName + "_Towar;";
            DownloadFromBase.UpdateBase(query);
        }

    }
}
