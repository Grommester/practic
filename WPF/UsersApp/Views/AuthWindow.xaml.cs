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

namespace UsersApp
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textboxLogin.Text.Trim();
            string pass = PassBox.Password.Trim();
            if (login.Length < 5)
            {
                textboxLogin.ToolTip = "Это поле введено не корректно";
                textboxLogin.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                PassBox.ToolTip = "Это поле введено не корректно";
                PassBox.Background = Brushes.DarkRed;
            }
            else
            {
                textboxLogin.ToolTip = "";
                textboxLogin.Background = Brushes.Transparent;
                PassBox.ToolTip = "";
                PassBox.Background = Brushes.Transparent;

                List<bd> User = Entities1.GetContext().bd.Where(p => p.login == login && p.pass == pass).ToList();
                if (User.Count != 0)
                {
                    MessageBox.Show("Всё хорошо");
                    userWindow userPageWindow = new userWindow();
                    userPageWindow.Show();
                    Hide();
                }

            }
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
