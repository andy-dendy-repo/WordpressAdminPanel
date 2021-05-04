using AdminPanel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
        ICategoryService _categoryService;
        IList<Category> _categories;

        public Products(IUnityContainer container)
        {
            InitializeComponent();
            _goodsService = container.Resolve<IGoodsService>();
            _categoryService = container.Resolve<ICategoryService>();
            LoadData();
        }

        private async void LoadData()
        {
            var products = Mapper.Map<IList<Product>, IList<WpPosts>>(await _goodsService.GetAllAsync());

            foreach (var product in products)
            {
                product.ProductMeta = await GetProductMeta(product.Id);
            }

            AllProductsTable.ItemsSource = products;

            _categories = Mapper.Map<IList<Category>, IList<WpTerms>>(await _categoryService.GetAllAsync());

            lbCategories.ItemsSource = _categories;
        }

        private async Task<ProductMeta> GetProductMeta(ulong id)
        {
            ProductMeta productMeta = new ProductMeta() 
            {
                Categories = Mapper.Map<IList<Category>, IList<WpTerms>>(await _goodsService.GetCategoriesByPostId(id))
            };

            return productMeta;
        }

        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            WpPosts product = new WpPosts()
            {
                PostContent = prodContent.Text,
                PostName = prodTitle.Text.ToLower(),
                PostTitle = prodTitle.Text
            };

            await _goodsService.AddWithMeta(
                product,
                prodArticul.Text,
                prodCharasteristics.Text,
                prodDescription.Text,
                prodDiscount.Text,
                prodPrice.Text,
                _categories.Where(x=>x.IsSelected).Select(x=>x.TermId).ToList()
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
