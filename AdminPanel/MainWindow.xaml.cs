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
        IGoodsService _goodsService;
        ICategoryService _categoryService;
        IOrdersService _ordersService;

        IList<Category> _categories;
        public MainWindow(IUnityContainer container)
        {
            InitializeComponent();
            _container = container;
            _goodsService = _container.Resolve<IGoodsService>();
            _categoryService = _container.Resolve<ICategoryService>();
            _ordersService = _container.Resolve<IOrdersService>();

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

                goods = Mapper.Map<IList<Product>, IList<WpPosts>>(await _goodsService.GetProductsFilteredByCategoryIds(ids));

                AllGoodsTable.ItemsSource = goods;
            }
            else
            {
                goods = Mapper.Map<IList<Product>, IList<WpPosts>>(await _goodsService.GetAllAsync());

                AllGoodsTable.ItemsSource = goods;
            }

            return goods;
        }

        private async void LoadData()
        {
            IList<Product> goods = await FilterByCategories();

            _categories = Mapper.Map<IList<Category>, IList<WpTerms>>(await _categoryService.GetAllAsync());

            lbCategories.ItemsSource = _categories;

            var orders = Mapper.Map<IList<Order>, IList<WpPosts>>(await _ordersService.GetAllAsync());

            foreach(var order in orders)
            {
                order.OrderMeta = await GetOrderMeta(order.Id);
            }

            AllOrdersTable.ItemsSource = orders;

            Statistics statistics = new Statistics()
            {
                AllOrdersCount = orders.Count,
                AllProductsCount = goods.Count,
                LastOrderMadeDate = orders.Max(x=>x.PostDate),
                LastProductMadeDate = goods.Max(x => x.PostDate),
                DeletedProductsCount = goods.Count(x=>x.PostStatus== "publish"),
                DeletedOrdersCount = orders.Count(x => x.PostStatus == "publish"),
            };

            gbStatistics.DataContext = statistics;
        }

        private async Task<OrderMeta> GetOrderMeta(ulong id)
        {
            var list = await _ordersService.GetMetaByPostId(id);

            OrderMeta orderMeta = null;

            if (list.Count != 0)
            {
                orderMeta = new OrderMeta();
                orderMeta.Ids = list.FirstOrDefault(x => x.MetaKey == "ids")?.MetaValue;
                orderMeta.Name = list.FirstOrDefault(x => x.MetaKey == "name")?.MetaValue;
                orderMeta.SecondName = list.FirstOrDefault(x => x.MetaKey == "second_name")?.MetaValue;
                orderMeta.Phone = list.FirstOrDefault(x => x.MetaKey == "phone")?.MetaValue;
                orderMeta.Address = list.FirstOrDefault(x => x.MetaKey == "address")?.MetaValue;
            }

            return orderMeta;
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
    }
}
