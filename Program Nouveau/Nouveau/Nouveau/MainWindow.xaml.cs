using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using System.Reflection;
using System.IO;

namespace Nouveau
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string NameLogin;
        public static bool Admin;
        public static string ShopName;
        public static bool IsEnabledSettings = false;
        public List<IteamSklep> ShowList { get; set; }
        public MainWindow()
        {
            SetFromFille();
            InitializeComponent();
            Loaded += new RoutedEventHandler(FirstLoaded);
        }

        private void SetFromFille()
        {
            string[] lines = { "1", "2", "3", "4", "5", "6" };
            System.IO.StreamReader file = new System.IO.StreamReader(@"..\Setts.txt");
            for(int i=0;i<lines.Length;i++)
            {
                lines[i] = file.ReadLine();
            }
            MainVarriables.NameComputer = lines[0].Remove(0,6);
            MainVarriables.VAT = Int32.Parse(lines[1].Remove(0,5));
            MainVarriables.Bck1 = lines[2].Remove(0, 6);
            MainVarriables.Bck2 = lines[3].Remove(0, 6);
            MainVarriables.Bck3 = lines[4].Remove(0, 6);
            MainVarriables.Bck4 = lines[5].Remove(0, 6);
            //Color color 
            //GridMainBackGround.Background = new SolidColorBrush(Color.aq);
        }
        public void ChangeColor(ComboBox Iteam)
        {
            Color selectedcolor = (Color)(Iteam.SelectedItem as PropertyInfo).GetValue(1, null);
            GridMainBackGround.Background = new SolidColorBrush(selectedcolor);
        }
        public void ChangeColor2(ComboBox Iteam)
        {
            Color selectedcolor = (Color)(Iteam.SelectedItem as PropertyInfo).GetValue(1, null);
            GridColorHeader.Background = new SolidColorBrush(selectedcolor);
        }
        public void ChangeColor3(ComboBox Iteam)
        {
            Color selectedcolor = (Color)(Iteam.SelectedItem as PropertyInfo).GetValue(1, null);
            GridColorMenu.Background = new SolidColorBrush(selectedcolor);
        }
        public void ChangeColor4(ComboBox Iteam)
        {
            Color selectedcolor = (Color)(Iteam.SelectedItem as PropertyInfo).GetValue(1, null);
            dataGrid.Background = new SolidColorBrush(selectedcolor);
        }
        private void ButtonExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButtonMiniApp_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;     
        }
             private void ButtonFiltr_Click(object sender, RoutedEventArgs e)
        {
            FiltrWindows filtrWin = new FiltrWindows();
            filtrWin.Show();
        }

        private void ButtonLogOutApp_Click(object sender, RoutedEventArgs e)
        {
            LogOut.LogOutButtek();
        }
        private void FirstLoaded(object sender, RoutedEventArgs e)
        {
            LabelA.Text = System.DateTime.Today.Day.ToString() + "/" + System.DateTime.Today.Month.ToString() + "/" + System.DateTime.Today.Year.ToString(); 
            CheckConnection connect = new CheckConnection();
            connect.SetIcon(ImageRedCircle,ImageGreenCircle);
            LoginWindow window = new LoginWindow();    
            window.Show();
        }

            public void SetObject(IteamSklep iteam)
        {
                ShowList = new List<IteamSklep>();
                ShowList.Add(iteam);
                ShowList.Add(iteam);
                ShowList.Add(iteam);
                DataContext = this;
        }

        private void UstawieniaButton(object sender, RoutedEventArgs e)
        {
            if (!IsEnabledSettings)
            {
                Settings windowsSetting = new Settings();
                windowsSetting.Show();
            }
        }
        private void SklepyButton(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            Shops_set windowsShop = new Shops_set();
            windowsShop.Show();
        }

        private void MagazynButton(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            Magazyn.SetSklep();
        }
        private void SprzedazButton(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            Sprzedaz.SetSklep();
        }
        private void TowaryButton(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            Towary.SetSklep();
        }

        public void SetUser(string admin,string shop)
        {
            if (admin == "0")
            {
                ShopName = shop;
                Admin = false;
                Button_towary.IsEnabled = true;
                Button_sprzedaz.IsEnabled = true;
            }
            else
            {
                ShopName = " ";
                Admin = true;
                Button_magazyn.IsEnabled = true;
                Button_sklep.IsEnabled = true;
            }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null && MainVarriables.NameGrid == ShopName + "_Towar")
            {
                GridItemSell GridItemWin = new GridItemSell(row_selected);
                GridItemWin.Show();
            }
            else if (row_selected != null && MainVarriables.NameGrid == "Magazyn")
            {
                GridItemMagazyn GridItemWin = new GridItemMagazyn(row_selected);
                GridItemWin.Show();
            }
        }

        private void ButtonFresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList.RefreshNow();
        }
    }
}
