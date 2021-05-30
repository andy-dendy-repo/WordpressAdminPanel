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
using WordpressClient.Services.Interfaces;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        ServicesForWindow _servicesForWindow;
        public Orders(IUnityContainer container)
        {
            InitializeComponent();
            _servicesForWindow = new ServicesForWindow(container.Resolve<IOrdersService>(), container.Resolve<IGoodsService>());
            LoadData();
        }

        private async void LoadData()
        {
            AllOrdersTable.ItemsSource = await _servicesForWindow.GetOrders();
        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var order = AllOrdersTable.SelectedItem as Order;

            if (order != null)
            {
                await _servicesForWindow.OrdersService.DeleteAsync(order.Id);
                LoadData();
            }
        }
    }
}
