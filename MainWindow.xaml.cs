using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Media;
using System.Windows.Media.Effects;
using System.Data.SQLite;
using System.Threading;

namespace Launch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AnyControlMouseEnter(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            DropShadowEffect effect = new DropShadowEffect();
            effect.BlurRadius = 20;
            effect.ShadowDepth = 0;
            effect.Color = Color.FromRgb(255, 255, 255);
            control.Effect = effect;
        }

        public void AnyControlMouseLeave(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            DropShadowEffect effect = new DropShadowEffect();
            effect.BlurRadius = 0;
            effect.ShadowDepth = 0;
            control.Effect = effect;
        }

        private void LabelForgPas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Окно восстановления пароля.");
        }

        private void LabelRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            Hide();
            menu.ShowDialog();
            Show();
        }

        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void MinimizeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window.WindowState = WindowState.Minimized;
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Login_Click(sender, e);
            }
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length == 0 || PasswordBox.Password.Length == 0)
            {
                Error("Введите логин и пароль!");
                return;
            }

            string Login = LoginBox.Text.Trim();
            string Password = PasswordBox.Password.Trim();

            try
            {
                if (MySQLite.LoginAccount(Login, Password))
                {
                    MessageBox.Show("Пользователь найден!");
                }
                else
                {
                    MessageBox.Show("Такого пользователя нет!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Error(string ErrorMessage)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() => Snackbar.MessageQueue?.Enqueue(ErrorMessage, null, null, null, false, true, TimeSpan.FromSeconds(2)));
            });
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int proverka = Check(LoginBox.Text.ToString());

            if (proverka == 1)
            {
                Error("Логин содержит недопустимые символы!");
                LoginButton.IsEnabled = false;
            }
            else
            {
                LoginButton.IsEnabled = true;
            }
        }

        private int Check(string proverka)
        {
            for (int i = 0; i < proverka.Length; i++)
            {
                if (!(((proverka[i] > 47) && (proverka[i] < 58)) || (proverka[i] > 64 && proverka[i] < 91) || (proverka[i] > 96 && proverka[i] < 123) || proverka[i] == 95))
                    return 1;
            }
            return 0;
        }

        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
