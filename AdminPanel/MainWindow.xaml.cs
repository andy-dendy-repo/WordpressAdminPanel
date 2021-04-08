using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        IList<Category> _categories;
        public MainWindow(IUnityContainer container)
        {
            InitializeComponent();
            _container = container;
            _goodsService = _container.Resolve<IGoodsService>();
            _categoryService = _container.Resolve<ICategoryService>();
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

        private async void LoadData()
        {
            if (_categories != null)
            {
                var ids = _categories.Where(x => x.IsSelected == true).Select(x=>x.TermId).ToList();

                AllGoodsTable.ItemsSource = Mapper.Map<IList<Product>, IList<WpPosts>>(await _goodsService.GetProductsFilteredByCategoryIds(ids));
            }
            else
            {
                AllGoodsTable.ItemsSource = Mapper.Map<IList<Product>, IList<WpPosts>>(await _goodsService.GetAllAsync());
            }


            _categories = Mapper.Map<IList<Category>, IList<WpTerms>>(await _categoryService.GetAllAsync());

            lbCategories.ItemsSource = _categories;

            
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
    }
}
