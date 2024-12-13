using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
using TextBlock = Wpf.Ui.Controls.TextBlock;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для Lichkabinet.xaml
    /// </summary>
    public partial class Lichkabinet : FluentWindow
    {
        public List<TextBox> tboxes = new List<TextBox>() { };
        public bool firstTouch = false;
        public int userId;
        public string oldGmail;
        public Lichkabinet(int id_user)
        {
            InitializeComponent();
            tboxes.Add(tboxGmail);
            tboxes.Add(tboxLogin);
            tboxes.Add(tboxSurname);
            tboxes.Add(tboxName);
            MainWindow.checkExist();
            userId = id_user;
            setInfo();
        }
        private async void setInfo()
        {
            HttpClient httpClient = new HttpClient();
            string url = $"https://localhost:7223/api/User/{userId}";
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var userNowString = await (httpResponseMessage.Content.ReadAsStringAsync());
                var userNow = JsonConvert.DeserializeObject<User>(userNowString);
                tboxGmail.Text = userNow.Email;
                oldGmail = userNow.Email;
                tboxLogin.Text = userNow.UserName;
                tboxName.Text = userNow.Name;
                tboxSurname.Text = userNow.Surname;
                tboxMidName.Text = userNow.MidName;
            }
            else
            {
                System.Windows.MessageBox.Show($"Произошла невиданная ошибка................ {httpResponseMessage.StatusCode}");
            }
        }
        private void previewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (previewtext(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool previewtext(string eText)
        {
            int d;
            if (int.TryParse(eText, out d))
            {
                return true;
            }
            else if (eText == " ")
            {
                return true;
            }
            else if (!Regex.IsMatch(eText, @"\p{L}"))
            {
                return true;
            }
            return false;
        }


        private void tboxName_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void returnToLogIn(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow(this.mainMenuUsername,this.mainMenuPassword,tboxGmail.Text,tboxLogin.Text,tboxPassword.te);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void tboxGmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (tboxGmailPreview(e.Text))
            {

                e.Handled = true;
            }
        }
        private bool tboxGmailPreview(string etext)
        {
            if (Regex.IsMatch(etext, @"\p{IsCyrillic}") || Regex.IsMatch(etext, @"\}"))
            {
                return true;
            }
            return false;
        }
        
        private void previewKeyDown(object sender, KeyEventArgs e)
        {
            if (previewkeydown(e))
            {
                e.Handled = true;
            }

        }
        private bool previewkeydown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                return true;
            }
            return false;
        }

        private async void change_data_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = new List<User>();
            HttpClient client1 = new HttpClient();
            string url1 = @$"https://localhost:7223/api/User/UsersAll";
            HttpResponseMessage message = await client1.GetAsync(url1);
            if (message.IsSuccessStatusCode)
            {
                string jsonText = message.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(jsonText);
            }
            else
            {
                System.Windows.MessageBox.Show("Произошла ошибка..........");
                return;
            }
            stackPanelProblems.Children.Clear();
            TextBlock tblock = new TextBlock();
            tblock.FontSize = 18;
            Regex g = new Regex("^([a-zA-z]|\\d|.)+@[a-zA-z]+\\.[a-zA-z]+$");
            User oldUser = users.Find(x => x.Email == oldGmail);
            if (oldUser is null)
            {
                System.Windows.MessageBox.Show("Произошла ошибка");
                return;
            }

            if (tboxes.Any(x => x.Text == ""))
            {
                tblock.Text = "Не все данные введены";
                stackPanelProblems.Children.Add(tblock);
                return;
            }

            else if (tboxSurname.Text.Any(x => !char.IsLetter(x)) || tboxName.Text.Any(x => !char.IsLetter(x)) || tboxMidName.Text.Any(x => !char.IsLetter(x)))
            {
                tblock.Text = "Неккоректные символы внутри фио";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (tboxPassword.Password.ToString().Length < 6 && tboxPassword.Password.Length > 0)
            {
                tblock.Text = "Пароль слишком короткий!";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (tboxPasswordNew.Password.ToString().Length < 6 && tboxPassword.Password.Length > 0)
            {
                tblock.Text = "Пароль слишком короткий!";
                stackPanelProblems.Children.Add(tblock);
                return;
            }

            else if (g.IsMatch(tboxGmail.Text) == false || tboxGmail.Text.Contains("..") || tboxGmail.Text.StartsWith(".") &&
                tboxGmail.Text.Contains("@") && (tboxGmail.Text.Split('@')[0].Any(x => char.IsLetter(x)) == false) || tboxGmail.Text.Split('@')[0].Last() == '.')
            {
                tblock.Text = "Почта не подходит!"; stackPanelProblems.Children.Add(tblock); return;
            }
            if (tboxGmail.Text.Split('@').Length > 2)
            {
                tblock.Text = "Почта не подходит!"; stackPanelProblems.Children.Add(tblock); return;
            }

            else if (tboxLogin.Text.Any(x => !char.IsLetterOrDigit(x)))
            {
                tblock.Text = "Неккоректные символы внутри логина";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (users.Exists(x => x.Email == tboxGmail.Text) && oldGmail != tboxGmail.Text)
            {
                tblock.Text = " Почта уже занята!"; stackPanelProblems.Children.Add(tblock); return;
            }
            for (int i = 0; i < tboxGmail.Text.Length; i++)
            {
                if (tboxGmail.Text[i] != '.' && char.IsLetterOrDigit(tboxGmail.Text[i]) == false && tboxGmail.Text[i] != '@')
                {
                    tblock.Text = "Почта не подходит!"; stackPanelProblems.Children.Add(tblock); return;
                }
            }
            User user = oldUser;
            user.Id = oldUser.Id;
            user.Email = tboxGmail.Text;
            user.Name = tboxName.Text;
            user.MidName = tboxMidName.Text;
            user.Surname = tboxSurname.Text;
            if (tboxPasswordNew.Text != "")
            {
                if (tboxPassword.Password != oldUser.Password)
                {

                    tblock.Text = "Заполните старый пароль";
                    stackPanelProblems.Children.Add(tblock);
                    return;
                }
                user.Password = tboxPasswordNew.Password.ToString();
            }
            else
            {
                user.Password = oldUser.Password;
            }
            
            user.UserName = tboxLogin.Text;
            oldGmail = tboxGmail.Text;

            string urik = $@"https://localhost:7223/api/User/EditUser?iduser={oldUser.Id}";
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.PutAsync(urik, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                System.Windows.MessageBox.Show("Данные измененны");
            }
            else
            {
                System.Windows.MessageBox.Show($"Ошибка {httpResponseMessage.Content.ReadAsStringAsync().Result}");
            }

        }


        private void TitleBar_CloseClicked(TitleBar sender, RoutedEventArgs args)
        {
            MainSiteHZ mainSiteHZ = new MainSiteHZ();
            mainSiteHZ.setId(this.userId);
            mainSiteHZ.Show();
            Close();
        }
    }
}
