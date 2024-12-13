using Microsoft.Win32;
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
    /// Логика взаимодействия для redactProduct.xaml
    /// </summary>
    public partial class redactProduct : FluentWindow
    {
        Product product;
        public string pathToPng;
        public redactProduct(Product product)
        {
            InitializeComponent();
            this.product = product;
            if (product.pathToPng != "None")
            {
                image.Source = new BitmapImage(new Uri(product.pathToPng));
            }
            textBoxNameProduct.Text = product.name;
            textBoxPrice.Text = product.price.ToString();
            textBoxRating.Text = product.rating.ToString();
            comboBoxCategory.Items.Add("Для зала");
            comboBoxCategory.Items.Add("Для спальни");
            comboBoxCategory.Items.Add("Для прихожей");
            foreach (var item in comboBoxCategory.Items)
            {
                if (product.category == item.ToString())
                {
                    comboBoxCategory.SelectedItem = item;
                }
            }
        }

        private async void change_data_Click(object sender, RoutedEventArgs e)
        {
            MessageBox messageBox = new MessageBox();
            if (double.TryParse(textBoxPrice.Text, out double dd) == false || dd <= 0)
            {
                messageBox.Content = "Введите корректную цену";
                messageBox.ShowDialogAsync();
                return;
            }
            else if (double.TryParse(textBoxRating.Text, out double d) == false || d > 5 || d < 0)
            {
                messageBox.Content = "Введите корректное значение рейтинга";
                messageBox.ShowDialogAsync();
                return;
            }
            else if (textBoxRating.Text == "" || textBoxPrice.Text == "" || textBoxNameProduct.Text == "")
            {
                messageBox.Content = "Введите все значения";
                messageBox.ShowDialogAsync();
                return;
            }
            Product newProduct = new Product(textBoxNameProduct.Text, comboBoxCategory.SelectedItem.ToString(),
                Double.Parse(textBoxPrice.Text), Double.Parse(textBoxRating.Text), pathToPng);
            if (pathToPng == "")
            {
                newProduct.pathToPng = "None";
            }
            string url = $@"https://localhost:7223/api/Products/editProduct?id_old_product={product.id}";
            var content = new StringContent(JsonConvert.SerializeObject(newProduct), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.PutAsync(url,content);
            if (httpResponseMessage.IsSuccessStatusCode )
            {
                System.Windows.MessageBox.Show("Данные измененны");
            }
            else
            {
                System.Windows.MessageBox.Show("Произошла ошибка...");
            }

            //List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("Items.json"));
            //Product changeProduct = products.First(x => this.product.id == x.id);
            //Product newProduct = new Product(textBoxNameProduct.Text, comboBoxCategory.SelectedItem.ToString(),
            //    Convert.ToDouble(textBoxPrice.Text), Convert.ToDouble(textBoxRating.Text), "None");
            //newProduct.id = changeProduct.id; 
            //products.Remove(changeProduct);
            //products.Add(newProduct);
            //File.WriteAllText("Items.json", JsonConvert.SerializeObject(products));


        }

        private void select_photo_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                if (!(op.FileName.EndsWith("jpg")) && !(op.FileName.EndsWith("png")) && !(op.FileName.EndsWith("jpeg")))
                {

                    System.Windows.MessageBox.Show("НЕТ");
                    return;
                }
                image.Source = new BitmapImage(new Uri(op.FileName));
                pathToPng = op.FileName;
            }
        }
    }
}
