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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace Launch
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                menuwindow.DragMove();
            }
        }

        private void ExitLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MinimizeLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            menuwindow.WindowState = WindowState.Minimized;
        }

        private void RegisterNewAccount(object sender, RoutedEventArgs e)
        {
            if (BoxesIsEmpty())
            {
                Error("Заполните все поля!");
                return;
            }

            try
            {
                Convert.ToInt64(PhoneNumberBox.Text);
            }
            catch
            {
                Error("Номер телефона введён некорректно!");
                return;
            }

            if (!string.Equals(PasswordBox.Password, RepeatPasswordBox.Password))
            {
                Error("Пароли не совпадают!");
                return;
            }

            if (!(bool)DataCheckBox.IsChecked)
            {
                Error("Условаие обработки данных не принято.");
                return;
            }

            try
            {
                MySQLite.RegisterAccount(LoginBox.Text, PasswordBox.Password, PhoneNumberBox.Text);
                MessageBox.Show("Пользователь успешно добавлен!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool BoxesIsEmpty()
        {
            return string.IsNullOrWhiteSpace(LoginBox.Text) ||
                   string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                   string.IsNullOrWhiteSpace(RepeatPasswordBox.Password) ||
                   string.IsNullOrWhiteSpace(PhoneNumberBox.Text);
        }

        private async void Error(string ErrorMessage)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() => Snackbar.MessageQueue?.Enqueue(ErrorMessage, null, null, null, false, true, TimeSpan.FromSeconds(2)));
            });
        }
    }
}
