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

namespace Nouveau
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        protected static string LoginText;
        protected static string PasswordText;
        public static bool IsOpenWin = false;
        
        public LoginWindow()
        {
            InitializeComponent();
            IsOpenWin = true;
        }

        public void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            IsOpenWin = false;
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginText = TextBoxLogin.Text.ToString();
            PasswordText = TextBoxPass.Password.ToString();
            Login ConnectWithShop = new Login();
            ConnectWithShop.SetWindow(this);
            ConnectWithShop.LoginOn(CheckBoxOffline);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Login_Click(sender, e);
            }
        }
    }
}
