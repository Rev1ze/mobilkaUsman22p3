using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Wpf.Ui.Controls;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : FluentWindow
    {
        string your_mail;
        public ChangePassword(string your_mail)
        {
            InitializeComponent();
            MainWindow.checkExist();
            this.your_mail = your_mail;
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            forgotPassword forgotPassword = new forgotPassword();
            forgotPassword.Show();
            this.Close();
        }

        private async void change_password_Click(object sender, RoutedEventArgs e)
        {
                MessageBox messageBox = new MessageBox();
            if (textBoxPassword.Password == "")
            {
                messageBox.Content = $"Заполните пароль";
                messageBox.ShowDialogAsync();
                return;
            }
            if (textBoxPassword.Password.Length < 6)
            {
                messageBox.Content = $"Пароль слишком короткий";
                messageBox.ShowDialogAsync();
                return;
            }
            if (textBoxPassword.Password != textBoxConfirmPassword.Password)
            {
                messageBox.Content = $"Пароли не совпадают {textBoxPassword.Password}";
                messageBox.ShowDialogAsync();
                return;
            }
            string url = @$"https://localhost:7223/api/User/UsersAll";
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
            List<User> users = new List<User>();
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                users = JsonConvert.DeserializeObject<List<User>>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            else
            {
                messageBox.Content = "Пользователь не найден";
                messageBox.ShowDialogAsync();
                return;
            }
            User nowUser = users.Find(x => x.Email == your_mail);
            if (nowUser == null) 
            {
                messageBox.Content = "Пользователь не найден";
                messageBox.ShowDialogAsync();
                return;
            }
            string url1 = $@"https://localhost:7223/api/User/EditUser?iduser={nowUser.Id}";
            HttpClient client1 = new HttpClient();
            nowUser.Password = textBoxPassword.Password;
            var content = new StringContent(JsonConvert.SerializeObject(nowUser), Encoding.UTF8,"application/json");
            HttpResponseMessage httpResponseMessage1 = await client.PutAsync(url1,content);
            if (httpResponseMessage1.IsSuccessStatusCode)
            {
                messageBox.Content = "Пароль успешно сменен!";
                messageBox.ShowDialogAsync();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                return;
            }
            else
            {
                messageBox.Content = $"Произошла ошибка {httpResponseMessage1.Content.ReadAsStringAsync().Result}!";
                messageBox.ShowDialogAsync();

            }


        }
    }
}
