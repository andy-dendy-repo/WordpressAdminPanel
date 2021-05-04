using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        IGoodsService _goodsService;
        public Products(IUnityContainer container)
        {
            InitializeComponent();
            _goodsService = container.Resolve<IGoodsService>();
            LoadData();
        }

        private async void LoadData()
        {
            AllProductsTable.ItemsSource = Mapper.Map<IList<Product>, IList<WpPosts>>(await _goodsService.GetAllAsync());
        }

        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            WpPosts product = new WpPosts()
            {
                PostContent = prodContent.Text,
                PostName = prodTitle.Text
            };

            await _goodsService.AddWithMeta(
                product, 
                prodArticul.Text,
                prodCharasteristics.Text,
                prodDescription.Text,
                prodDiscount.Text,
                prodPrice.Text
                );

            LoadData();
        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var product = AllProductsTable.SelectedItem as Product;

            if (product != null)
            {
                await _goodsService.DeleteAsync(product.Id);
                LoadData();
            }
        }
    }
}
