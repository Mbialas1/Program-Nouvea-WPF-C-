using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Globalization; 
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nouveau
{
    /// <summary>
    /// Logika interakcji dla klasy FiltrWindows.xaml
    /// </summary>
    public partial class FiltrWindows : Window
    {

        private bool dp_vis1 = false;
        private bool dp_vis2 = false;
        private bool dp_vis3 = false;
        public FiltrWindows()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var MainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            if (MainWin.dataGrid.Items.Count < 1)
            {
                MessageBox.Show("Nie została aktywowana żadna lista!", "Błąd odczytu listy");
            }
            else
            {
                    string a = ((ComboBoxItem)comboBox_1.SelectedItem).Content as string;
                    string a1 = "";
                if (a != "Brak")
                {
                    if (a == "Data")
                    {
                        a1 = dp1.Text.ToString();
                        MessageBox.Show(a1);
                    }
                    else
                    {
                        a1 = textBox_1.Text;
                    }
                }

                string b = ((ComboBoxItem)comboBox_2.SelectedItem).Content as string;
                string b1 = "";
                if (b != "Brak")
                {
                    if (b == "Data")
                    {
                        b1 = dp2.Text.ToString();
                    }
                    else
                    {
                        b1 = textBox_2.Text;
                    }
                }
                string c = ((ComboBoxItem)comboBox_3.SelectedItem).Content as string;
                string c1 = "";
                if (c != "Brak")
                {
                    if (c == "Data")
                    {
                        c1 = dp3.Text.ToString();
                    }
                    else
                    {
                        c1 = textBox_3.Text;
                    }
                }

                if (a == "Brak" && b == "Brak" && c == "Brak")
                {
                    MessageBox.Show("Nie wybrano żadnej opcji filtrowania","Błąd użytkownika");
                }
                else
                {
                    FiltreList.SetList(a, b, c, a1, b1, c1);
                }
            }
        }

        private void textChangedBox1(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_1.SelectedIndex != 7)
            {
                this.textBox_1.IsEnabled = true;
            }
            if(comboBox_1.SelectedIndex == 3)
            {
                
                //dp1.Format = DateTimePickerFormat.Custom;
                //dp1.CustomFormat = "yyyy/MM/dd";
                this.dp1.Visibility = Visibility.Visible;
                this.textBox_1.Visibility = Visibility.Hidden;
                dp_vis1 = true;
            }
            else if(comboBox_1.SelectedIndex != 3 && dp_vis1)
            {
                dp_vis1 = false;
                this.dp1.Visibility = Visibility.Hidden;
                this.textBox_1.Visibility = Visibility.Visible;
            }

        }
        private void textChangedBox2(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_2.SelectedIndex != 7)
            {
                textBox_2.IsEnabled = true;
            }
            if (comboBox_2.SelectedIndex == 3)
            {
                this.dp2.Visibility = Visibility.Visible;
                this.textBox_2.Visibility = Visibility.Hidden;
                dp_vis2 = true;
            }
            else if (comboBox_2.SelectedIndex != 3 && dp_vis2)
            {
                dp_vis2 = false;
                this.dp2.Visibility = Visibility.Hidden;
                this.textBox_2.Visibility = Visibility.Visible;
            }
        }
        private void textChangedBox3(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_3.SelectedIndex != 7)
            {
                textBox_3.IsEnabled = true;
            }
            if (comboBox_3.SelectedIndex == 3)
            {
                this.dp3.Visibility = Visibility.Visible;
                this.textBox_3.Visibility = Visibility.Hidden;
                dp_vis3 = true;
            }
            else if (comboBox_3.SelectedIndex != 3 && dp_vis3)
            {
                dp_vis3 = false;
                this.dp3.Visibility = Visibility.Hidden;
                this.textBox_3.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
