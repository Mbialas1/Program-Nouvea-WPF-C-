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
    public class Login : LoginWindow
    {
        private LoginWindow FormLogin;

        public void SetWindow(LoginWindow window)
        {
            FormLogin = window;
        }

        public void LoginOn(CheckBox checkOnline)
        {
         if(checkOnline.IsChecked == true)
            {
                Offline();
            }
         else
            {
                Online();
            }
        }

        private void Offline()
        {
            if (String.IsNullOrEmpty(LoginText) || String.IsNullOrWhiteSpace(PasswordText))
            {
                MessageBox.Show("Login i hasło nie mogą pozostać puste !", "Błąd podczas logowania");
                return;
            }
            CheckUsers();
        }

        private void Online()
        {
            if (String.IsNullOrEmpty(LoginText) || String.IsNullOrWhiteSpace(PasswordText))
            {
                MessageBox.Show("Login i hasło nie mogą pozostać puste !", "Błąd podczas logowania");
                return;
            }

            if(CheckConnection.CheckForInternetConnection() == false)
            {
                MessageBox.Show("Nie masz połączenia z internetem! Spróbuj zalogować się offline.", "Błąd podczas połączenia z internetem");
                return;
            }


            string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
             MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
                try
                {
                    ConnectSQL.Open();
                    string query = "SELECT * FROM Login WHERE Login = '" + LoginText + "' AND Hasło = '" + PasswordText + "';";
                    MySqlCommand cmd = new MySqlCommand(query,ConnectSQL);
                    MySqlDataReader reader = cmd.ExecuteReader();
                        var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dw = dt.Rows[0];
                        MainWin.SetUser(dw[2].ToString(), dw[3].ToString());
                        MainWindow.NameLogin = LoginText;
                    }
                    else
                    {
                        MessageBox.Show("Nie prawidłowe dane!", "Błąd logowania");
                        ConnectSQL.Close();
                        return;
                    }
                        MainWin.NazwaLogin.Text = "Jako: " + LoginText;
                        ConnectSQL.Close();
                        LoginWindow.IsOpenWin = false;
                        MainWin.Logoutlog.Content = "Wyloguj";
                        FormLogin.Close();               
                }
                catch(Exception ex)
                {
                    var error = ex.Message;
                    MessageBox.Show("Serwer nie odpowiada. Spróbuj ponownie lub sprawdź połączenie internetowe.", "Czas minął");
                }

            }       
        

        private void CheckUsers()
        {
            string firstline;
            string secondline;
            string path = @"../../Profiles/"+LoginText+".txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("Nie prawidłowe dane !","Błąd podczas logowania");
                return;
            }
            using (StreamReader sr = File.OpenText(path))
            {
                firstline = sr.ReadLine();
                string Password = sr.ReadLine();
                Char[] DeletePass = { 'P', 's', ':' };
                Password = Password.TrimStart(DeletePass);
                if(Password != PasswordText)
                {
                    MessageBox.Show("Nie prawidłowe dane !", "Błąd podczas logowania");
                    return;
                }
                secondline = Password;
            }

            using (StreamWriter sr = new StreamWriter(path))
            {
                sr.WriteLine(firstline);
                sr.WriteLine("Ps:" + secondline);
                sr.WriteLine("Data:"+ System.DateTime.Today.Day.ToString()+"/"+ System.DateTime.Today.Month.ToString()+"/"+ System.DateTime.Today.Year.ToString());
            }
            MessageBox.Show("Zalogowałeś się do programu offline. Nie będziesz miał teraz aktualizowanych danych z serwera. Wyłącz program i zaloguj się online.", "Tryb offline");
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            MainWin.NazwaLogin.Text = "Jako: "+LoginText;
            MainWin.Logoutlog.Content = "Wyloguj";
            FormLogin.Close();
       }
    }
}
