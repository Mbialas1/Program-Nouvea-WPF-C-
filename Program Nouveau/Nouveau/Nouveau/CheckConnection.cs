using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nouveau
{
    public class CheckConnection
    { 
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public virtual void SetIcon(Image Green,Image Red)
        {
           if(CheckForInternetConnection() == true)
            {
               Green.Visibility = Visibility.Visible;
            }
           else
            {
               Red.Visibility = Visibility.Visible;
               MessageBox.Show("Program wykrył, że nie posiadasz aktualnie dostępu do sieci! Uruchom ponownie program aby połączył się z siecią lub przejdź do trybu offline.", "Brak połączenia internetowego");
            }
        }
    }
}
