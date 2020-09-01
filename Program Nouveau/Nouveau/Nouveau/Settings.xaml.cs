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
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

namespace Nouveau
{
    /// <summary>
    /// Logika interakcji dla klasy Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            MainWindow.IsEnabledSettings = true;
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if(MainWindow.Admin == true)
            {
                TabItem_Sklep.IsEnabled = true;
                UserTabItem.IsEnabled = true;
                TabItem_Towar.IsEnabled = true;
            }
            ComboColorMain.ItemsSource = typeof(Colors).GetProperties();
            ComboColorMain2.ItemsSource = typeof(Colors).GetProperties();
            ComboColorMain3.ItemsSource = typeof(Colors).GetProperties();
            ComboColorMain4.ItemsSource = typeof(Colors).GetProperties();
        }

        private void Button_minimalization(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Button_maximaliaztion(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
            tabControl.Height = this.Height;
            tabControl.Width = this.Width;
        }
        private void Button_exit(object sender, RoutedEventArgs e)
        {
            MainWindow.IsEnabledSettings = false;
            this.Close();
        }

        private void GotFocus_User(object sender, MouseButtonEventArgs e)
        {
            comboBox_Shop.Items.Clear();
            TabItemGotFocus.User_Focus(comboBox_Shop);
            comboBox_Delete_acc.Items.Clear();
            TabItemGotFocus.User_Focus_Delete(comboBox_Delete_acc);
        }

        private void GotFocus_Shop(object sender,MouseButtonEventArgs e)
        {
            comboBox_DeleteShop.Items.Clear();
            TabItemGotFocus.User_Focus(comboBox_DeleteShop);
        }
        private void Button_create_acc_Click(object sender, RoutedEventArgs e)
        {
            Account.Create_Acc(Box_name.Text.ToString(), Box_password.Text.ToString(), comboBox_Shop.Text.ToString(), checkBox.IsChecked.ToString());
        }
        private void Button_delete_acc_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_Delete_acc.SelectedItem == null)
            {
                MessageBox.Show("Wybierz użytkownika!", "Błąd!");
                return;
            }
            if (MessageBox.Show("Czy jesteś pewny? Po kliknięciu wszystkie dane związane z użytkownikiem zostaną trwale usunięte.", "Usuń trwale konto", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Account.Delete_Acc(comboBox_Delete_acc.Text.ToString());
                comboBox_Delete_acc.Items.Remove(comboBox_Delete_acc.SelectedItem);
            }
        }

        private void Button_CreateShop_Click(object sender, RoutedEventArgs e)
        {
            Shop_Setting.ShopSettingCreate(textBox_CreateShop.Text.ToString());
        }

        protected void Button_CreateTowar_Click(object sender, RoutedEventArgs e)
        {
            Towar_Setting.AddNewTowar();
        }
        private void Button_DeleteShop_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_DeleteShop.SelectedItem == null)
            {
                MessageBox.Show("Wybierz sklep!", "Błąd!");
                return;
            }
            if (MessageBox.Show("Czy jesteś pewny? Po kliknięciu wszystkie dane związane ze sklepem zostaną trwale usunięte.", "Usuń trwale konto", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Shop_Setting.ShopSettingDelete(comboBox_DeleteShop.Text.ToString());
                comboBox_DeleteShop.Items.Remove(comboBox_Delete_acc.SelectedItem);
            }
        }
        private void GotFocus_Towar(object sender, MouseButtonEventArgs e)
        {
            GotFocusTowar.SetTabIteam();
        }

        private void LeftTextBox_Netto(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9^,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            if (textBox_Netto.Text != "")
            {
                double GetNetto = Double.Parse(textBox_Netto.Text);
                GetNetto = MainVarriables.VAT * GetNetto / 100;
                double GetBrutto = Double.Parse(textBox_Netto.Text);
                GetBrutto = GetBrutto + GetNetto;
                textBox_Brutto.Text = GetBrutto.ToString("0.00");
            }
        }

        private void ComboColorMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChangeColors(object sender, RoutedEventArgs e)
        {
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            if (ComboColorMain.SelectedItem != null)
            {
                MainWin.ChangeColor(ComboColorMain);
                Type t = ComboColorMain.SelectedItem.GetType();
                //t = Type.GetType(Color);
                //MainVarriables.Bck1 = Color.FromArgb(ComboColorMain.SelectedItem.GetType).ToString();
            }
            if (ComboColorMain2.SelectedItem != null)
            {
                MainWin.ChangeColor2(ComboColorMain2);
                MainVarriables.Bck2 = ComboColorMain2.SelectedItem.ToString();
            }
            if (ComboColorMain3.SelectedItem != null)
            {
                MainWin.ChangeColor3(ComboColorMain3);
                MainVarriables.Bck3 = ComboColorMain3.SelectedItem.ToString();
            }
            if (ComboColorMain4.SelectedItem != null)
            {
                MainWin.ChangeColor4(ComboColorMain4);
                MainVarriables.Bck4 = ComboColorMain4.SelectedItem.ToString();
            }
        }

        private void SaveToFile(object sender, RoutedEventArgs e)
        {
            try
            {  
                string[] lines = { "Name::"+MainVarriables.NameComputer, "VAT::"+MainVarriables.VAT.ToString(), "Bck1::"+MainVarriables.Bck1,"Bck2::"+MainVarriables.Bck2,"Bck3::"+MainVarriables.Bck3,"Bck4::"+MainVarriables.Bck4 };
                System.IO.File.WriteAllLines(@"..\Setts.txt", lines);
                MessageBox.Show("Zapisano wszystkie ustawienia do pliku.", "Poprawny zapis");
            }
            catch (Exception error)
            {
                MessageBox.Show("Nie można znaleźć odpowiedniego pliku. Ponowne uruchomienie programu powinno rozwiązać problem.", "Błąd");
            }
        }

        private void ChangeNameComputer(object sender, RoutedEventArgs e)
        {
            MainVarriables.NameComputer = TextBoxNameShopComputer.Text;
            MessageBox.Show("Zmiany załadowane poprawnie. Nie zapomnij zapisać wszystkich zmian.", "Załadowano pomyślnie!");
        }

        private void ChangeVAT(object sender, RoutedEventArgs e)
        {
            MainVarriables.VAT = Int32.Parse(TextBoxVAT.Text);
            MessageBox.Show("Zmiany załadowane poprawnie. Nie zapomnij zapisać wszystkich zmian.", "Załadowano pomyślnie!");
        }

        private void TextBox_Brutto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
