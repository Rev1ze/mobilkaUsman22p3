 using EasyCaptcha.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
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
using Wpf.Ui.Controls;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для captcha.xaml
    /// </summary>
    public partial class captcha : FluentWindow
    {
        string email;
        int id = 0;

        public captcha(string email)
        {
            InitializeComponent();
            MainWindow.checkExist();
            Captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
            this.email = email;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        private void send_captcha_Text(object sender, RoutedEventArgs e)
        {
            if (Captcha.CaptchaText.ToLower() == textBoxCaptcha.Text)
            {
                secondFactor secondFactor = new secondFactor(email);
                secondFactor.Show();
                secondFactor.setId(id);
                this.Close();
            }
            else if (Captcha.CaptchaText.ToLower() != textBoxCaptcha.Text)
            {
                MessageBox messageBox = new MessageBox();
                messageBox.Content = "Неверный текст с картинки";
                messageBox.ShowDialogAsync();
                //List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Users.json"));
                //User nowuser = users.First(x=> x.Email == email);
                //users.Remove(nowuser);
                //nowuser.IsBlocked = true;
                //users.Add(nowuser);
                //File.WriteAllText("Users.json",JsonConvert.SerializeObject(users));
                //Environment.Exit(0);
            }
            else if (textBoxCaptcha.Text == "")
            {
                MessageBox messageBox = new MessageBox();
                messageBox.Content = "Введите капчу";
                messageBox.ShowDialogAsync();
            }
        }

        private void TitleBar_CloseClicked(TitleBar sender, RoutedEventArgs args)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
