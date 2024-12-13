using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
using Wpf.Ui.Controls;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для forgotPassword.xaml
    /// </summary>
    
    public partial class forgotPassword : FluentWindow
    {
        string gmail;
        Random random = new Random();
        int code_verication;
        DispatcherTimer timer = new DispatcherTimer();
        bool isTimeOut = false;
        int seconds = 300;
        public forgotPassword()
        {
            InitializeComponent();
            MainWindow.checkExist();

            code_verication = random.Next(10000, 99999);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int minutes = seconds / 60;
            int sec2 = seconds % 60;
            labelTimer.Content = $"Осталось времени {minutes}:{sec2}";
            seconds -= 1;
            if (seconds == 0)
            {
                isTimeOut = true;
                timer.Stop();
            }
        }

        private void sendOnEmailClick(object sender, RoutedEventArgs e)
        {
            sendingEmail(textBoxGmail.Text);
        }
        private void sendingEmail(string your_mail)
        {
            Regex g = new Regex("^([a-zA-Z0-9]|\\d)+@[a-zA-z]+.[a-zA-z]+$");
            if (timer.IsEnabled == true) 
            {
                Wpf.Ui.Controls.MessageBox messageBox = new Wpf.Ui.Controls.MessageBox();
                messageBox.Content = "Дождитесь окончания таймера!";
                messageBox.ShowDialogAsync();
                return;
            }
            if (g.IsMatch(your_mail) == false || your_mail.Contains("..") || your_mail.StartsWith(".")
                || !(your_mail.Contains("@")) || (your_mail.Split('@')[0].Any(x => char.IsLetter(x)) == false)
                || !your_mail.All(x => x == '.' || char.IsLetterOrDigit(x) || x == '@'))
            {
                Wpf.Ui.Controls.MessageBox messageBox = new Wpf.Ui.Controls.MessageBox();
                messageBox.Content = "Неверная почта";
                messageBox.ShowDialogAsync();
                 return;
            }
            
            gmail = textBoxGmail.Text;
            int rand = code_verication;
            var fromAddress = new MailAddress("usman1610200627@gmail.com", "Usman228");
            var toAddress = new MailAddress(your_mail, "q");
            const string fromPassword = "irse btjv qfvq hfin";
            const string subject = "Код восстановления";
            string body = $"Ваш код восстановления {rand}";
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
                isTimeOut = false;
                seconds = 300;
                textBoxCode.IsEnabled = true;
                buttonSendCode.IsEnabled = true;
                timer.Start();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
             MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void send_code_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCode.Text == code_verication.ToString() && !isTimeOut)
            {
                ChangePassword changePassword = new ChangePassword(gmail);
                changePassword.Show();
                this.Close();
            }
            else if (textBoxCode.Text != code_verication.ToString() && !isTimeOut)
            {
                Wpf.Ui.Controls.MessageBox messageBox = new Wpf.Ui.Controls.MessageBox();
                messageBox.Content = "Неверный код :(";
                messageBox.ShowDialogAsync();

            }
            else if (isTimeOut)
            {
                Wpf.Ui.Controls.MessageBox messageBox = new Wpf.Ui.Controls.MessageBox();
                messageBox.Content = "Время вышло :(";
                messageBox.ShowDialogAsync();
                buttonSendCode.IsEnabled = false;
                textBoxCode.IsEnabled = false;

                code_verication = random.Next(10000,99999);
            }
        }
    }
}
