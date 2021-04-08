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
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Window
    {
        ICategoryService _categoryService;
        public Categories(IUnityContainer container)
        {
            InitializeComponent();
            _categoryService = container.Resolve<ICategoryService>();
            LoadData();
        }

        private async void LoadData()
        {
            AllCategoriesTable.ItemsSource = Mapper.Map<IList<Category>, IList<WpTerms>>( await _categoryService.GetAllAsync());
        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var category = AllCategoriesTable.SelectedItem as Category;

            if (category != null)
            {
                await _categoryService.DeleteAsync(category.TermId);
                LoadData();
            }

        }

        private async void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category()
            {
                Name = catName.Text,
                Slug = catSlug.Text
            };

            await _categoryService.Add(Mapper.Map<WpTerms, Category>(category));
            LoadData();
        }
    }
}
