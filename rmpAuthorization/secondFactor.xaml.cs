using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Media.Animation;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для secondFactor.xaml
    /// </summary>
    public partial class secondFactor : FluentWindow
    {
        string email;
        int id = 0;
        Random random = new Random();
        int code_verication;
        public secondFactor(string email)
        {
            InitializeComponent();
            MainWindow.checkExist();
            this.email = email;
            code_verication = random.Next(10000,99999);
            int rand = code_verication;
            var fromAddress = new MailAddress("usman1610200627@gmail.com", "Usman228");
            var toAddress = new MailAddress(email, "q");
            const string fromPassword = "irse btjv qfvq hfin";
            const string subject = "Код аутентификации";
            string body = $"Ваш код аутентификации {rand}";

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
            }
        }

        public void setId(int id)
        {
            this.id = id;
        }
        private void enter_code_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCode.Text == code_verication.ToString())
            {
                MainSiteHZ mainSiteHZ = new MainSiteHZ();
                mainSiteHZ.Show();
                mainSiteHZ.setId(id);
                this.Close();
            }
            else
            {
                MessageBox messageBox = new MessageBox();
                messageBox.Content = "Неверный код аутентификации";
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
