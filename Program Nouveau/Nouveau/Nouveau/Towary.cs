using System;
using System.Collections.Generic;

namespace Nouveau
{
    public class Towary : MainWindow
    {
        public static void SetSklep()
        {
            MainVarriables.NameGrid = ShopName+"_Towar";
            string query = "SELECT * FROM " + ShopName + "_Towar;";
            DownloadFromBase.UpdateBase(query);
        }
    }
}
