using AdminPanel.Models;
using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUnityContainer _container;
        ServicesForWindow _servicesForWindow;
        ICategoryService _categoryService;
        IList<Category> _categories;
        public MainWindow(IUnityContainer container)
        {
            InitializeComponent();
            _container = container;
            _categoryService = _container.Resolve<ICategoryService>();
            _servicesForWindow = new ServicesForWindow(_container.Resolve<IOrdersService>(),_container.Resolve<IGoodsService>());
            MainGrid.IsEnabled = false;

            var result = _container.Resolve<Login>().ShowDialog();

            if (result.Value)
            {
                LoadData();
                MainGrid.IsEnabled = true;
            }
            else
            {
                Close();
            }
        }

        private async Task<IList<Product>> FilterByCategories()
        {
            IList<Product> goods = null;

            if (_categories != null)
            {
                var ids = _categories.Where(x => x.IsSelected == true).Select(x => x.TermId).ToList();

                goods = Mapper.Map<IList<Product>, IList<WpPosts>>(await _servicesForWindow.GoodsService.GetProductsFilteredByCategoryIds(ids));

                if (goods.FirstOrDefault() != null)
                    AllGoodsTable.ItemsSource = goods.Where(x=>x!=null);
                else
                    AllGoodsTable.ItemsSource = null;
            }
            else
            {
                goods = Mapper.Map<IList<Product>, IList<WpPosts>>(await _servicesForWindow.GoodsService.GetAllAsync());

                AllGoodsTable.ItemsSource = goods.Where(x => x != null);
            }

            return goods;
        }

        private async void LoadData()
        {
            IList<Product> goods = await FilterByCategories();

            _categories = Mapper.Map<IList<Category>, IList<WpTerms>>(await _categoryService.GetAllAsync());

            lbCategories.ItemsSource = _categories;

            var orders = await _servicesForWindow.GetOrders();

            AllOrdersTable.ItemsSource = orders;

            try
            {

                Statistics statistics = new Statistics()
                {
                    AllOrdersCount = orders.Count,
                    AllProductsCount = goods.Count,
                    LastOrderMadeDate = orders.Max(x => x.PostDate),
                    LastProductMadeDate = goods.Max(x => x.PostDate),
                    DeletedProductsCount = goods.Count(x => x.PostStatus == "publish"),
                    DeletedOrdersCount = orders.Count(x => x.PostStatus == "publish"),
                };

                gbStatistics.DataContext = statistics;
            }
            catch { }
        }

        private void miCategories_Click(object sender, RoutedEventArgs e)
        {
            _container.Resolve<Categories>().Show();
        }

        private void miLogin_Click(object sender, RoutedEventArgs e)
        {
            var result = _container.Resolve<Login>().ShowDialog();

            if(result.Value)
            {
                LoadData();
                MainGrid.IsEnabled = true;
            }
        }

        private void miRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void miProducts_Click(object sender, RoutedEventArgs e)
        {
            _container.Resolve<Products>().Show();
        }

        private async void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            _ = await FilterByCategories();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _container.Resolve<Orders>().Show();
        }
    }
}
