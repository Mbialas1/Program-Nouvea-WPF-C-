using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace Nouveau
{
    public class Account
    {
        static int NRAdmin;
        public static void Create_Acc(string nazwa, string haslo, string sklep, string admin)
        {
            char[] _Nazwa = new char[nazwa.Length+1];
            if (nazwa.Length < 3 || haslo.Length < 6)
            {
                MessageBox.Show("Nazwa konta lub hasło ma za mało znaków! Pamiętaj: Hasło musi mieć minimum 6 znaków.", "Błąd w tworzeniu konta");
                return;
            }
            else
            {
                for (int i = 0; i < nazwa.Length; i++)
                {
                    _Nazwa[i] = nazwa[i];
                    if (_Nazwa[i] == 32)
                    {
                        MessageBox.Show("Nie można używać spacji w nazwie konta!", "Błąd w nazwie konta");
                        return;
                    }
                }
                char [] _Haslo = new char[haslo.Length+1];
                for(int i = 0; i < haslo.Length; i++)
                {
                    _Haslo[i] = haslo[i];
                    if(_Haslo[i] == 32)
                    {
                        MessageBox.Show("W haśle nie może występować spacja", "Błąd w tworzeniu hasła.");
                        return;
                    }
                }
            }
            if(sklep.Length == 0)
            {
                sklep = "-";
            }
            
            if (admin == "True") NRAdmin = 1;
            else if (admin == "False") NRAdmin = 0;
            
            string query = "INSERT INTO Login (Login,hasło,Admin,Sklep) VALUES ('"+nazwa+"','"+haslo+"','"+NRAdmin+"','"+sklep+"');";
            SetCommandBase(query,nazwa);
        }

        private static void SetCommandBase(string query,string acc_Existing)
        {
            string ConnectInfo = "SERVER=db4free.net;DATABASE=nouveaudb;UID=nouveaudb;PASSWORD=Sklepnouveau11;OldGuids = true;";
            MySqlConnection ConnectSQL = new MySqlConnection(ConnectInfo);
            try
            {
                ConnectSQL.Open();
                if(acc_Existing.Length != 0)
                {
                    MySqlCommand cmdacc= new MySqlCommand("SELECT * FROM Login WHERE Login='"+acc_Existing+"';", ConnectSQL);
                    MySqlDataReader readerCheck = cmdacc.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(readerCheck);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Nazwa konta już istnieje !", "Zła nazwa konta");
                        ConnectSQL.Close();
                        return;
                    }
                }
                MySqlCommand cmd = new MySqlCommand(query, ConnectSQL);
                MySqlDataReader reader = cmd.ExecuteReader();
                ConnectSQL.Close();
                MessageBox.Show("Done!", "-");
            }
            catch (Exception e)
            {
                MessageBox.Show("Nie udało się utworzyć konta.");
            }

        }

        public static void Delete_Acc(string sklep)
        {
            string query = "DELETE FROM Login WHERE Login ='"+sklep+"';";
            SetCommandBase(query,"");
        }

    }

}
