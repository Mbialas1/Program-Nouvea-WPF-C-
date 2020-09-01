using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nouveau
{
   public class RefreshList
    {
        public static void RefreshNow()
        {
            string query = "SELECT * FROM " + MainVarriables.NameGrid + ";";
            DownloadFromBase.UpdateBase(query);
        }
    }
}
