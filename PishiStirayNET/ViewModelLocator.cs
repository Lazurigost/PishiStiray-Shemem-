using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PishiStiray.Services;
using PishiStiray.VeiwModels;
using System.Configuration;

namespace PishiStiray
{
    internal class ViewModelLocator
    {
        public static ServiceProvider? _provider;

        public static void Init()
        {
            ServiceCollection services = new ServiceCollection();

            #region Services

            services.AddSingleton<UserService>();
            services.AddSingleton<PageService>();
            services.AddSingleton<ProductService>();
            #endregion


            #region ViewModels

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SignInPageViewModel>();
            services.AddTransient<ProductsPageViewModel>();
            services.AddTransient<AuthorizedUserUserControlViewModel>();
            services.AddTransient<CartPageViewModel>();

            #endregion


            #region Database Contexts
            services.AddDbContext<TradeContext>(options =>
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TradeDatabase"].ConnectionString;
                options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }, ServiceLifetime.Singleton);
            #endregion

            _provider = services.BuildServiceProvider();

            foreach (var service in services)
            {
                _provider.GetRequiredService(service.ServiceType);
            }
        }

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        public SignInPageViewModel SignInPageViewModel => _provider.GetRequiredService<SignInPageViewModel>();
        public ProductsPageViewModel ProductsPageViewModel => _provider.GetRequiredService<ProductsPageViewModel>();
        public AuthorizedUserUserControlViewModel AuthorizedUserUserControlViewModel => _provider.GetRequiredService<AuthorizedUserUserControlViewModel>();
        public CartPageViewModel CartPageViewModel => _provider?.GetRequiredService<CartPageViewModel>();
    }
}
