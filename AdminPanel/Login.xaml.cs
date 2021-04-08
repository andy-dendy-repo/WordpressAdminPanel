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
using Unity.Resolution;
using WordpressClient.Services.Interfaces;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private IAuthService _authService;
        private IUnityContainer _unityContainer;
        public Login(IUnityContainer container)
        {
            InitializeComponent();
            _unityContainer = container;
            _authService = container.Resolve<IAuthService>();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_authService.SignIn(tbUsename.Text, tbPassword.Text))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect credentials!");
            }
        }
    }
}
