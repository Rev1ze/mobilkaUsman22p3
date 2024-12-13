using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using Wpf.Ui.Controls;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace rmpAuthorization // toRMP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        int tries = 0;
        static string pathProducts = @"C:\\sharpAuthoriz\\rmpAuthorization\\bin\\Debug\\items.json";
        static string pathUsers = @"C:\sharpAuthoriz\rmpAuthorization\bin\Debug\Users.json";
        public static void checkExist()
        {
            if (File.Exists(pathProducts) == false)
            {
                File.Create(pathProducts);
            }
            if (File.Exists(pathUsers) == false)
            {
                File.Create(pathUsers);
            }
            try
            {
                var lst =  JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(pathUsers));
            }
            catch (Exception e)
            {
                File.WriteAllText(pathUsers, "[]");

            }
            try
            {
                var lst =  JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(pathProducts));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show($"Ошибка {e.Message}");
                File.WriteAllText(pathUsers, "[]");
            }



        }

        public MainWindow()
        {
            InitializeComponent();
            MainWindow.checkExist();
        }

        private void registrationButtonClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {

            MessageBox box = new MessageBox();
            if (tboxLogin.Text == "" || tboxPassword.Password == "")
            {
                box.Content = "Введите логин и пароль";
                box.ShowDialogAsync();
                return;
            }
            //else if (!tboxLogin.Text.All(x => char.IsLetterOrDigit(x) || x == '@' || x == '.') || !tboxPassword.Text.All(x => char.IsLetterOrDigit(x)|| char.IsSymbol(x)))
            //{
               //box.Content = $"Введите корректные символы в логин и пароль";
                //box.ShowDialogAsync(); return;
            //}
            if (File.Exists("Users.json") == false)
            {
                File.Create("Users.json");
            }
            logIn();
        }

        private async void logIn()
        {
            MessageBox box = new MessageBox();
            string url = $"https://localhost:7223/api/User/Authorization?login={tboxLogin.Text}&passw={tboxPassword.Password}";
            HttpClient client = new HttpClient();
            var content = new StringContent(url, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await client.PostAsync(url,content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string userok = httpResponseMessage.Content.ReadAsStringAsync().Result;
                User userNow = JsonConvert.DeserializeObject<User>(userok);

                if (userNow != null)
                {
                    if (userNow.IsBlocked == true)
                    {
                        box.Content = "Вы заблокированы...";
                        box.ShowDialogAsync();
                        return;
                    }
                    if (userNow.Password == tboxPassword.Password)
                    {
                        captcha captcha = new captcha(userNow.Email);
                        captcha.Show();
                        captcha.setId(userNow.Id);
                        this.Close();
                        return;
                    }
                    else if (userNow.Password != tboxPassword.Password && tries < 5)
                    {
                        box.Content = "Неверный пароль";
                        box.ShowDialogAsync();
                        tries++;
                        return;
                    }

                }
                else
                {
                    box.Content = "Пользователь не найден";
                    box.ShowDialogAsync();
                }

            }
            else
            
            {
                box.Content = "Произошла невиданная ошибка";
                 box.ShowDialogAsync();
            }


        }

        private void forgotten_password_Click(object sender, RoutedEventArgs e)
        {
            forgotPassword forgotPassword = new forgotPassword();
            forgotPassword.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainSiteHZ mainSiteHZ = new MainSiteHZ();
            mainSiteHZ.setId(1);
            mainSiteHZ.Show();
            this.Close();
        }
    }
}
