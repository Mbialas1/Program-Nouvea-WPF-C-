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
using MySql.Data.MySqlClient;

namespace Nouveau
{
    /// <summary>
    /// Logika interakcji dla klasy GridItemMagazyn.xaml
    /// </summary>
    public partial class GridItemMagazyn : Window
    {
        public static DataRowView RowForThisClass;
        public static List<CheckBox> BoxObject = new List<CheckBox>();
        public static List<string> CheckBoxShop = new List<string>();
        public GridItemMagazyn(DataRowView row_selected)
        {
            InitializeComponent();
            SetShops();
            label_id.Content = row_selected["Id"].ToString() + " ID";
            label_nazwa.Content = row_selected["Nazwa"].ToString();
        }

        private static void SetShops()
        {
            
            CheckBoxShop.Clear();
            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                string query = "SELECT * FROM Sklepy;";
                ConnectSQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);

                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                   if (myReader["Nazwa"].ToString() != "-")
                    {
                        CheckBoxShop.Add(myReader["Nazwa"].ToString());
                    }
                }
                ConnectSQL.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się odnaleźć żadnych sklepów.Prawpodobnie może oznaczać to problem z siecią lub bazą danych","Błąd");
            }

            //25,250,0,0
            int PosFirstCheck = 300+50;
            CheckBox MagazinCheck = new CheckBox();
            MagazinCheck.Margin = new Thickness(25, PosFirstCheck-50, 0, 0);
            MagazinCheck.IsChecked = false;
            MagazinCheck.Name = "Magazyn";
            MagazinCheck.VerticalAlignment = VerticalAlignment.Top;
            MagazinCheck.Content = MagazinCheck.Name;
            var Win = Application.Current.Windows.Cast<Window>().FirstOrDefault(GridItemMagazyn => GridItemMagazyn is GridItemMagazyn) as GridItemMagazyn;
            Win.GridThis.Children.Add(MagazinCheck);

            BoxObject.Clear();

            for(int i=0;i<CheckBoxShop.Count;i++)
            {
                BoxObject.Add(new CheckBox());
                BoxObject[i].Margin = new Thickness(25, PosFirstCheck, 0, 0);
                BoxObject[i].IsChecked = false;
                BoxObject[i].Name = CheckBoxShop[i];
                BoxObject[i].Content = CheckBoxShop[i];
                BoxObject[i].VerticalAlignment = VerticalAlignment.Top;
                PosFirstCheck += 50;
                Win.GridThis.Children.Add(BoxObject[i]);
            }
            //560,325,0,0
            Win.button.Margin = new Thickness(560, PosFirstCheck+50, 0, 0);
        }

        private void Button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_maximaliaztion(object sender, RoutedEventArgs e)
        {

        }

        private void Button_minimalization(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
                textBox.IsEnabled = true;
                textBox2.IsEnabled = true;
        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            textBox.IsEnabled = false;
            textBox2.IsEnabled = false;
        }

        private void CheckBox_Copy_Checked(object sender, RoutedEventArgs e)
        {
            textBox_Copy.IsEnabled = true;
        }
        private void CheckBox_Copy_UnChecked(object sender, RoutedEventArgs e)
        {
            textBox_Copy.IsEnabled = false;
        }
        private void CheckBox_Copy1_Checked(object sender, RoutedEventArgs e)
        {
            textBox_Copy1.IsEnabled = true;
        }
        private void CheckBox_Copy1_UnChecked(object sender, RoutedEventArgs e)
        {
            textBox_Copy1.IsEnabled = false;
        }

        private void Button_modifi(object sender, RoutedEventArgs e)
        {
            List <CheckBox> listcheck = new List<CheckBox>();
            listcheck.Add(checkBox);
            listcheck.Add(checkBox_Copy);
            listcheck.Add(checkBox_Copy1);
            listcheck.Add(checkBox_Copy2);
            short _a_ = 0;
            for (int i=0;i<listcheck.Count;i++)
            {
                if (listcheck[i].IsChecked == false)
                {
                    _a_++;
                }
            }
            if(_a_ == listcheck.Count)
            {
                MessageBox.Show("Nie zaznaczono żadnego obiektu!", "Zaznacz obiekt");
                return;
            }
            if(listcheck[0].IsChecked == true)
            {
                if(textBox.Text.Length < 1 || textBox2.Text.Length < 1)
                {
                    MessageBox.Show("Nie można zostawiać pustych pól.", "Puste pole");
                    return;
                }
            }
            if(listcheck[1].IsChecked == true)
            {
                if(textBox_Copy.Text.Length < 1)
                {
                    MessageBox.Show("Nie można zostawiać pustych pól.", "Puste pole");
                    return;
                }
            }
            if(listcheck[2].IsChecked == true)
            {
                if (textBox_Copy1.Text.Length < 1)
                {
                    MessageBox.Show("Nie można zostawiać pustych pól.", "Puste pole");
                    return;
                }
            }
            if(BoxObject.Count > 0)
            {
                short _checkBox_ = 0;
                for(int i = 0; i < BoxObject.Count;i++)
                {
                    if(BoxObject[i].IsChecked == false)
                    {
                        _checkBox_++;
                    }
                }
                var Win = Application.Current.Windows.Cast<Window>().FirstOrDefault(GridItemMagazyn => GridItemMagazyn is GridItemMagazyn) as GridItemMagazyn;
                var Child = Win.GridThis.Children[16] as CheckBox;
                if(Child.IsChecked == false)
                {
                    _checkBox_++;
                }
                if (_checkBox_ == BoxObject.Count+1)
                {
                    MessageBox.Show("Nie zaznaczono sklepu !", "Wybierz sklep");
                    return;
                }
            }

            try
            {
                string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
                MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                string query = "SELECT * FROM Sklepy;";
                ConnectSQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);

                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    if (myReader["Nazwa"].ToString() != "-")
                    {
                        CheckBoxShop.Add(myReader["Nazwa"].ToString());
                    }
                }
                ConnectSQL.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show("Nie udało się zmodyfikować produktu. Sprawdź czy dany produkt znajduje się na pewno w zaznaczonym punkcie.", "Błąd");
            }
        }
        private void LeftTextBox_Netto(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9^,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text != "")
            {
                double GetNetto = Double.Parse(textBox.Text);
                GetNetto = MainVarriables.VAT * GetNetto / 100;
                double GetBrutto = Double.Parse(textBox.Text);
                GetBrutto = GetBrutto + GetNetto;
                textBox2.Text = GetBrutto.ToString("0.00");
            }
        }

        private void TextBox_Brutto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
