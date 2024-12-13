using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using static System.Net.WebRequestMethods;
using MessageBox = Wpf.Ui.Controls.MessageBox;
using TextBlock = Wpf.Ui.Controls.TextBlock;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : FluentWindow // toRMP
    {
        private List<TextBox> tboxes = new List<TextBox>() {};
        public RegistrationWindow()
        {
            InitializeComponent();
            tboxes.Add(tboxGmail);
            tboxes.Add(tboxLogin);
            tboxes.Add(tboxPassword);
            tboxes.Add(tboxSurname);
            tboxes.Add(tboxName);
            calendar.Date = DateTime.Now;
            MainWindow.checkExist();
        }


        private async void registrationClick(object sender, RoutedEventArgs e)
        {
            stackPanelProblems.Children.Clear();
            TextBlock tblock = new TextBlock();
            tblock.FontSize = 18;
            if (tboxes.Any(x => x.Text == ""))
            {
                tblock.Text = "Не все данные введены";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (tboxPassword.Password.ToString().Length < 6)
            {
                tblock.Text = "Пароль слишком короткий!";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (!(tboxPassword.Password.ToString() == tboxPasswordConfirm.Password.ToString()))
            {
                tblock.Text = "Пароли не совпадают!";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (tboxPassword.Password.Any(x => char.IsLetterOrDigit(x) || x == '*' || x== '!'))
            {

                tblock.Text = "Пароль должен содержать спец символ!";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (tboxLogin.Text.Any(x => !char.IsLetterOrDigit(x) ))
            {
                tblock.Text = "Неккоректные символы внутри логина";
                stackPanelProblems.Children.Add(tblock);
                return;
            }

            //MessageBox zxc = new MessageBox() { Content = (tboxGmail.Text.Split('@')[0].Any(x => char.IsLetter(x)) == false) };
            //zxc.ShowDialogAsync();
            Regex g = new Regex("^([a-zA-Z0-9]|\\d)+@[a-zA-z]+.[a-zA-z]+$");
            if (g.IsMatch(tboxGmail.Text) == false || tboxGmail.Text.Contains("..") || tboxGmail.Text.StartsWith(".")
                || !(tboxGmail.Text.Contains("@")) || (tboxGmail.Text.Split('@')[0].Any(x => char.IsLetter(x)) == false)
                || !tboxGmail.Text.All(x => x == '.' || char.IsLetterOrDigit(x) || x == '@'))

            {
                tblock.Text = "Неверная почта!";stackPanelProblems.Children.Add(tblock);return;
            }
            else if (tboxSurname.Text.Any(x => !char.IsLetter(x)) || tboxName.Text.Any(x => !char.IsLetter(x)) || tboxMidName.Text.Any(x => !char.IsLetter(x)))
            {
                tblock.Text = "Неккоректные символы внутри\n фио";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if ((tboxMidName.Text == "") && !char.IsUpper(tboxMidName.Text.First()))
            {
                tblock.Text = "Фио с большой буквы и\n" +
                    "Больше одного символа";
                stackPanelProblems.Children.Add(tblock);
                return;
            }
            else if (!char.IsUpper(tboxSurname.Text.First()) || !char.IsUpper(tboxName.Text.First()) ||
                tboxName.Text.Length == 1 || tboxSurname.Text.Length == 1)
            {
                tblock.Text = "Фио с большой буквы и\n" +
                    "Больше одного символа";
                stackPanelProblems.Children.Add(tblock);
                return;
            }

            DateTime d = Convert.ToDateTime($"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year - 18}");
            DateTime d1 = Convert.ToDateTime($"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year - 100}");
            if (!(calendar.Date.Value < d) || calendar.Date.Value < d1)
            {
                tblock.Text = "Некорректный возраст!";stackPanelProblems.Children.Add(tblock);return;
            }
            string jsonText = "";
            HttpClient client1 = new HttpClient();
            string url1 = @$"https://localhost:7223/api/User/UsersAll";
            HttpResponseMessage message = await client1.GetAsync(url1);
            if (message.IsSuccessStatusCode)
            {
                jsonText = message.Content.ReadAsStringAsync().Result;
            }

            List<User> users = new List<User>();
            if (jsonText != "")
            {
                users = JsonConvert.DeserializeObject<List<User>>(jsonText);
            }
            if (users.Exists(x => x.UserName == tboxLogin.Text))
            {
                tblock.Text = "Логин уже существует!"; stackPanelProblems.Children.Add(tblock); return;
            }
            else if (users.Exists(x => x.Email == tboxGmail.Text))
            {
                tblock.Text = " Почта уже занята!"; stackPanelProblems.Children.Add(tblock); return;
            }
            HttpClient client = new HttpClient();
            User user1 = new User(tboxLogin.Text, tboxPassword.Password.ToString(), tboxName.Text, tboxSurname.Text, tboxMidName.Text, tboxGmail.Text,"891123213",calendar.Date.Value.ToString(), false);
            string url = "https://localhost:7223/api/User/Registration";
            var json = JsonConvert.SerializeObject(user1);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    System.Windows.MessageBox.Show("Вы успешно зарегались");

                }
            }
            catch (Exception)
            {

            }

        }


        private void previewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int d;
            if (int.TryParse(e.Text, out d))
            {
                e.Handled = true;
            }
        }

        private void tboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void tboxGmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length > 50)
            {
                e.Handled= true;
            }
            if (Regex.IsMatch(e.Text, @"\p{IsCyrillic}"))
            {
                e.Handled = true;
            }

        }
    }
}
