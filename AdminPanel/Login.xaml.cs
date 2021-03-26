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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private IAuthService _authService;
        private IUnityContainer _container;
        public Login(IUnityContainer container, IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            _container = container;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(_authService.SignIn(tbUsename.Text, tbPassword.Text))
            {

            }
            else
            {

            }
        }
    }
}
