using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Text.RegularExpressions;

namespace Nouveau
{
    /// <summary>
    /// Logika interakcji dla klasy GridItemSell.xaml
    /// </summary>
    public partial class GridItemSell : Window
    {

        public static DataRowView RowForThisClass;
        protected string Netto;
        protected string Brutto;
        public static List<bool> List_CheckBox = new List<bool>();
        public GridItemSell(DataRowView row_selected)
        {
            InitializeComponent();
            if (MainVarriables.NameGrid == MainWindow.ShopName + "_Towar")
            {
                text_id.Text = row_selected["Id"].ToString();
                text_data.Text = row_selected["Data"].ToString();
                text_nazwa.Text = row_selected["Nazwa"].ToString();
                text_kategoria.Text = row_selected["Kategoria"].ToString();
                text_ilosc.Text = row_selected["Ilość"].ToString();
                text_rozmiar.Text = row_selected["Rozmiar"].ToString();
                text_opis.Text = row_selected["Opis"].ToString();
                textBlock_Netto.Text = row_selected["Netto"].ToString();
                textBlock_Brutto.Text = row_selected["Brutto"].ToString();
                text_User.Text = MainWindow.NameLogin;
                RowForThisClass = row_selected;
                Netto = row_selected["Netto"].ToString();
                Brutto = row_selected["Brutto"].ToString();
            }
        }

        private void Button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_minimalization(object sender, RoutedEventArgs e)
        {
            
        }
        private void Rabat_Text(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9^]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            if (textBox_rabat.Text != "" && Int32.Parse(textBox_rabat.Text.ToString()) < 101)
            {
                double Rabat = Double.Parse(textBox_rabat.Text.ToString());
                double GetNetto = Double.Parse(Netto);
                Rabat = (Rabat/100) * Double.Parse(Netto);
                GetNetto = Double.Parse(Netto) - Rabat;
                double GetBrutto = Double.Parse(Brutto);
                Rabat = Double.Parse(textBox_rabat.Text);
                Rabat = (Rabat/100) * Double.Parse(Brutto);
                GetBrutto = Double.Parse(Brutto) - Rabat;

                textBlock_Brutto.Text = GetBrutto.ToString("0.00");
                textBlock_Netto.Text = GetNetto.ToString("0.00");
            }
            else
            {
                textBlock_Brutto.Text = Brutto.ToString();
                textBlock_Netto.Text = Netto.ToString();
            }
        }

        private void Button_maximaliaztion(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(textBox_rabat.Text == "")
            {
                textBox_rabat.Text = "0";
            }
            if (Int32.Parse(RowForThisClass["Ilość"].ToString()) > 0 && textBox_nrsprze.Text.Length > 0 && Int32.Parse(textBox_rabat.Text.ToString()) < 100)
            {
                if (MessageBox.Show("Czy jesteś pewny?", "Sprzedaj towar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    SellTowar.Sell(RowForThisClass, textBox_nrsprze.Text.ToString(),textBlock_Netto.Text.ToString(),textBlock_Brutto.Text.ToString(),textBlock_OpisSprze.Text.ToString());
                }
            }
            else if (Int32.Parse(RowForThisClass["Ilość"].ToString()) < 1 || textBox_nrsprze.Text.Length < 1 || Int32.Parse(textBox_rabat.Text.ToString()) > 100)
            {
                MessageBox.Show("Coś jest nie tak. Sprawdź czy ilość sztuk towaru nie jest mniejsza niż 1, czy rabat nie jest większy niż 100% lub czy pole tekstowe numeru sprzedaży nie pozostało puste.","Błąd towaru");
            }
        }

        private void GotFocus_Znajdz(object sender, MouseButtonEventArgs e)
        {
            GotFocusSearchItem.GetTextBox();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            List_CheckBox.Clear();
            List_CheckBox.Add(checkBox_nazwa.IsChecked.Value);
            List_CheckBox.Add(checkBox_kategoria.IsChecked.Value);
            List_CheckBox.Add(checkBox_cena.IsChecked.Value);
            List_CheckBox.Add(checkBox_rozmiar.IsChecked.Value);
            SearchBaseForItem.SearchBase();
        }
    }
}
