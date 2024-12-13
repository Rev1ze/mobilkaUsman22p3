using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для TableUsers.xaml
    /// </summary>
    public partial class TableUsers : FluentWindow
    {
        private int id = 0;
        public TableUsers()
        {
            InitializeComponent();
            setUsers();
            MainWindow.checkExist();

        }
        public void setId(int id)
        {
            this.id = id;
        }
        public async void setUsers()
        {
            HttpClient client1 = new HttpClient();
            string url1 = @$"https://localhost:7223/api/User/UsersAll";
            HttpResponseMessage message = await client1.GetAsync(url1);
            if (message.IsSuccessStatusCode)
            {
                string jsonText = message.Content.ReadAsStringAsync().Result;
                if (jsonText != "")
                {
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonText);
                    dg.ItemsSource = users;
                    

                    //dg.Items.Remove("Id");
                    //dg.Items.Remove("Password");
                    //dg.Items.Remove("IsBlocked");
                    
                }
            }
        }

        private void update_button_click(object sender, RoutedEventArgs e)
        {
            setUsers();
        }



        private void TitleBar_CloseClicked(TitleBar sender, RoutedEventArgs args)
        {
            MainSiteHZ mainSiteHZ = new MainSiteHZ();
            mainSiteHZ.setId(this.id);
            mainSiteHZ.Show();
            Close();

        }
    }
}
