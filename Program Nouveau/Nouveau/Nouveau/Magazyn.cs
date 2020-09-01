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
    public class Magazyn : MainWindow
    {
        public static void SetSklep()
        {
            MainVarriables.NameGrid = "Magazyn";
            string query = "SELECT * FROM Magazyn;";
            DownloadFromBase.UpdateBase(query);
        }
    }
}
