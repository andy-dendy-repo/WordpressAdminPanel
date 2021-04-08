using System.Windows;
using Unity;
using Unity.Resolution;
using WordpressClient.Data;
using WordpressClient.Services;
using WordpressClient.Services.Interfaces;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GetUnityContainer().Resolve<MainWindow>().Show();
        }

        private IUnityContainer GetUnityContainer()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<AdminDbContext>();

            container.RegisterType<IAuthService, AuthService>();

            container.RegisterType<IGoodsService, GoodsService>();

            container.RegisterType<ICategoryService, CategoryService>();

            return container;
        }
    }
}
