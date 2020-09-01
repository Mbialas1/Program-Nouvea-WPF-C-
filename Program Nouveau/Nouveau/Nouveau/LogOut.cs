using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Nouveau
{
    public static class LogOut
    {
        static public void LogOutButtek() // Wyłączenie wszystkich ważnych funkcji związanych z kontem użytkownika.
        {
            if (!LoginWindow.IsOpenWin)
            {
                LoginWindow log = new LoginWindow();
                var MainWin = Application.Current.Windows.Cast<MainWindow>().FirstOrDefault(MainWindow => MainWindow is MainWindow) as MainWindow;
                MainWin.Button_magazyn.IsEnabled = false;
                MainWin.Button_sklep.IsEnabled = false;
                MainWin.Button_sprzedaz.IsEnabled = false;
                MainWin.Button_towary.IsEnabled = false;
                MainWin.NazwaLogin.Text = "Brak zalogowanego";
                MainVarriables.NameGrid = "";
                MainWindow.NameLogin = "";
                MainWindow.Admin = false;
                MainWindow.ShopName = "";
                MainWin.dataGrid.ItemsSource = null;
                MainWin.Logoutlog.Content = "Zaloguj";
                LoginWindow login = new LoginWindow();
                login.Show();
            }
            }
        }
    }

