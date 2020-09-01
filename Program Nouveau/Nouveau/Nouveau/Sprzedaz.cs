using System;
using System.Collections.Generic;

namespace Nouveau
{
    public class Sprzedaz : MainWindow
    {
        public static void SetSklep()
        {
            MainVarriables.NameGrid = ShopName+"_Sprzedaz";
            string query = "SELECT * FROM " + ShopName + "_Sprzedaz;";
            DownloadFromBase.UpdateBase(query);
        }
}
}
