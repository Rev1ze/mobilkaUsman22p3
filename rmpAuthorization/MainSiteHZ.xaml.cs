using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Controls;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для MainSiteHZ.xaml
    /// </summary>
    public partial class MainSiteHZ : FluentWindow
    {
        int id_user = 0;
        List<Product> allProducts;
        public MainSiteHZ()
        {
            InitializeComponent();
            MainWindow.checkExist();
            comboBoxCategory.Items.Add("Для зала");
            comboBoxCategory.Items.Add("Для прихожей");
            comboBoxCategory.Items.Add("Для спальни");
            comboBoxSorting.Items.Add("По цене (Убывание)");
            comboBoxSorting.Items.Add("По цене (Возрастание)");
            comboBoxSorting.Items.Add("По рейтингу (Убывание)");
            comboBoxSorting.Items.Add("По рейтингу (Возрастание)");
            comboBoxFiltration.Items.Add("По рейтингу ");
            comboBoxFiltration.Items.Add("По цене");
            comboBoxFiltration.Items.Add("Для зала");
            comboBoxFiltration.Items.Add("Для прихожей");
            comboBoxFiltration.Items.Add("Для спальни");
            setProducts();
        }
        public void setId(int id)
        {
            id_user = id;
        }
        public async void setProducts()
        {
            HttpClient client = new HttpClient();
            string url = @"https://localhost:7223/api/Products/GetItems";
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string items = httpResponseMessage.Content.ReadAsStringAsync().Result;
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(items);

                stackPanelProducts.Children.Clear();
                if (products.Count == 0)
                {
                    System.Windows.MessageBox.Show("Пустые");
                    return;
                }
                foreach (Product product in products)
                {
                    ProductUserControl productUserControl = new ProductUserControl();
                    productUserControl.setContent(product);
                    stackPanelProducts.Children.Add(productUserControl);
                    stackPanelProducts.Children.Add(new Wpf.Ui.Controls.TextBlock() { Width = 25 });

                }
            }


        }

        private async void add_product_Click(object sender, RoutedEventArgs e)
        {
            MessageBox messageBox = new MessageBox();
            if (Double.TryParse(textBoxPrice.Text, out double d) == false)
            {
                messageBox.Content = "Некорректная цена";
                messageBox.ShowDialogAsync();
                return;
            }
            if (double.Parse(textBoxPrice.Text) <= 0)
            {
                messageBox.Content = "Слишком дешево";
                messageBox.ShowDialogAsync();
                return;
            }
            if (textBoxName.Text == "" || comboBoxCategory.SelectedIndex == -1)
            {
                messageBox.Content = "Заполните пустые поля";
                messageBox.ShowDialogAsync();
                return;
            }

            Product product = new Product(textBoxName.Text, comboBoxCategory.Text, Double.Parse(textBoxPrice.Text), 0, "None");
            product.setUniqueId();

            string url = "https://localhost:7223/api/Products/GetItems";
            List<Product> products = new List<Product>();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responeProducts = await httpClient.GetAsync(url);
            if (responeProducts.IsSuccessStatusCode)
            {
                string json2 = responeProducts.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(json2);
            }
            else
            {
                System.Windows.MessageBox.Show("Произошла ошибка при получении товаров");
                return;
            }

            if (products.Exists(x => x.name == textBoxName.Text && comboBoxCategory.Text == x.category && x.price == double.Parse(textBoxPrice.Text)))
            {
                messageBox.Content = $"{textBoxName.Text} уже существует";
                messageBox.ShowDialogAsync();
                return;
            }
            string json = JsonConvert.SerializeObject(product);
            HttpClient client = new HttpClient();
            string url2 = "https://localhost:7223/api/Products/addProduct";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await client.PostAsync(url2, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                System.Windows.MessageBox.Show("Товар добавлен успешно");
            }
            else
            {
                System.Windows.MessageBox.Show("Произошла ошибка");

            }

            setProducts();
        }

        private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Double.TryParse(e.Text, out double d) == false && e.Text != ".")
            {
                e.Handled = true;
            }
        }

        private async void comboBoxSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string url = "https://localhost:7223/api/Products/GetItems";
            List<Product> productNow = new List<Product>();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responeProducts = await httpClient.GetAsync(url);
            if (responeProducts.IsSuccessStatusCode)
            {
                string json2 = responeProducts.Content.ReadAsStringAsync().Result;
                productNow = JsonConvert.DeserializeObject<List<Product>>(json2);
            }
            else
            {
                System.Windows.MessageBox.Show("Произошла ошибка при получении товаров");
                return;
            }

            if (comboBoxSorting.SelectedIndex == 0)
            {
                productNow = productNow.OrderBy(x => x.price).Reverse().ToList();
            }
            else if (comboBoxSorting.SelectedIndex == 1)
            {

                productNow = productNow.OrderBy(x => x.price).ToList();
            }
            else if (comboBoxSorting.SelectedIndex == 2)
            {
                productNow = productNow.OrderBy(x => x.rating).Reverse().ToList();
            }
            else if (comboBoxSorting.SelectedIndex == 3)
            {
                productNow = productNow.OrderBy(x => x.rating).ToList();
            }
            setProductRedacted(productNow);


        }
        private void setProductRedacted(List<Product> products)
        {
            stackPanelProducts.Children.Clear();
            foreach (Product product in products)
            {
                ProductUserControl productUserControl = new ProductUserControl();
                productUserControl.setContent(product);
                stackPanelProducts.Children.Add(productUserControl);
                stackPanelProducts.Children.Add(new Wpf.Ui.Controls.TextBlock() { Width = 25 });
            }
        }
        private async void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFind.Text == "")
            {
                MessageBox messageBox = new MessageBox();
                messageBox.Content = "Заполните поле поиска";
                messageBox.ShowDialogAsync();
                return;
            }
            string url = "https://localhost:7223/api/Products/GetItems";
            List<Product> products = new List<Product>();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responeProducts = await httpClient.GetAsync(url);
            if (responeProducts.IsSuccessStatusCode)
            {
                string json2 = responeProducts.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(json2);
            }
            else
            {
                System.Windows.MessageBox.Show("Произошла ошибка при получении товаров");
                return;
            }
            List<Product> newProducts = new List<Product>();
            foreach (var item in products)
            {
                if (item.name.ToLower().Contains(textBoxFind.Text.ToLower()))
                {
                    newProducts.Add(item);
                }
            }
            setProductRedacted(newProducts);
        }

        private void BackBaseData_Click(object sender, RoutedEventArgs e)
        {
            setProducts();
        }

        private async void Filtration_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox messageBox = new MessageBox();

            if (comboBoxFiltration.SelectedIndex == -1)
            {
                messageBox.Content = "Выберите поле внутри списка";
                messageBox.ShowDialogAsync();
                return;
            }
            string url = "https://localhost:7223/api/Products/GetItems";
            List<Product> products = new List<Product>();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responeProducts = await httpClient.GetAsync(url);
            if (responeProducts.IsSuccessStatusCode)
            {
                string json2 = responeProducts.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(json2);
            }
            else
            {
                System.Windows.MessageBox.Show("Произошла ошибка при получении товаров");
                return;
            }
            List<Product> newProduct = new List<Product>();


            if (comboBoxFiltration.SelectedIndex > 1 && comboBoxFiltration.SelectedIndex < 5)
            {
                string categ = comboBoxFiltration.SelectedItem.ToString();
                foreach (var item in products)
                {
                    if (item.category == categ)
                    {
                        newProduct.Add(item);
                    }
                }
                setProductRedacted(newProduct);
                if (newProduct.Count == 0)
                {
                    stackPanelProducts.Children.Clear();
                    Wpf.Ui.Controls.TextBlock textBlock = new Wpf.Ui.Controls.TextBlock();
                    textBlock.Text = "Товаров нет";
                    stackPanelProducts.Children.Add(textBlock);
                }
                return;
            }
            if (textBoxFiltration1.Text == "" || textBoxFiltration2.Text == "")
            {
                messageBox.Content = "Заполните поля диапазона";
                messageBox.ShowDialogAsync();
                return;
            }
            double more = double.Parse(textBoxFiltration2.Text);
            double not_more = double.Parse(textBoxFiltration1.Text);
            foreach (var item in products)
            {
                if ((item.price < more && item.price > not_more) && comboBoxFiltration.SelectedIndex == 1)
                {
                    newProduct.Add(item);
                }
                else if ((item.rating < more && item.rating > not_more) && comboBoxFiltration.SelectedIndex == 0 && more <= 5 && not_more >= 0)
                {
                    newProduct.Add(item);
                }
            }
            setProductRedacted(newProduct);
            if (newProduct.Count == 0)
            {
                stackPanelProducts.Children.Clear();
                Wpf.Ui.Controls.TextBlock textBlock = new Wpf.Ui.Controls.TextBlock();
                textBlock.Text = "Товаров нет";
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanelProducts.Children.Add(textBlock);
            }

        }

        private void comboBoxFiltration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void stackPanelProducts_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            setProducts();
        }

        private void All_Profiles_Click(object sender, RoutedEventArgs e)
        {
            TableUsers users = new TableUsers();
            users.setId(this.id_user);
            users.Show();
            this.Close();
        }

        private void Lichka_Click(object sender, RoutedEventArgs e)
        {
            Lichkabinet lichkabinet = new Lichkabinet(this.id_user);
            lichkabinet.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
