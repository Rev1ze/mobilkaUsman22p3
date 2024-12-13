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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace rmpAuthorization
{
    /// <summary>
    /// Логика взаимодействия для ProductUserControl.xaml                                                                      
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        public Product product;
        int id;
        public ProductUserControl()
        {
            InitializeComponent();
        }
        public void setContent(Product yourProduct)
        {
            MainWindow.checkExist();
            if (yourProduct.pathToPng != null && yourProduct.pathToPng != "" && yourProduct.pathToPng != "None")
            {
                image.Source = new BitmapImage(new Uri(yourProduct.pathToPng, UriKind.Absolute));
            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"image/noimage.png", UriKind.Relative));
            }
            textBlockNameProduct.Text = "Товар: " + yourProduct.name;
            textBlockPrice.Text = "Цена: " + yourProduct.price + " тг";
            textBlockCategory.Text = "Категория " + yourProduct.category;
            textBlockRating.Text = "Рейтинг: " + yourProduct.rating + "★";
            product = yourProduct;

        }

        private void buttonRedact_Click(object sender, RoutedEventArgs e)
        {
            redactProduct redactProduct = new redactProduct(product);
            redactProduct.ShowDialog();
            this.textBlockCategory.Text = "Категория " +  redactProduct.comboBoxCategory.Text;
            textBlockPrice.Text = "Цена: " + redactProduct.textBoxPrice.Text + " тг";
            textBlockRating.Text = "Рейтинг: " + redactProduct.textBoxRating.Text + "★";
            textBlockNameProduct.Text = "Товар: " + redactProduct.textBoxNameProduct.Text;
            if (redactProduct.pathToPng == null || redactProduct.pathToPng == "")
            {
                return;
            }
            image.Source = new BitmapImage(new Uri(redactProduct.pathToPng,UriKind.Absolute));
        }
        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
             MessageBoxResult q = MessageBox.Show("Точно хотите удалить","", MessageBoxButton.YesNo);
            if (q is MessageBoxResult.No)
            {
                return;
            }
            string url = $@"https://localhost:7223/api/Products/DeleteProduct?id={product.id}";
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response  = await httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                System.Windows.MessageBox.Show("Товар удален");
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                System.Windows.MessageBox.Show($"Произошла ошибка :{response.Content.ReadAsStringAsync().Result}");
            }

        }
    }
}
