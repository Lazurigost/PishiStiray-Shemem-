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

            services.AddTransient<UserService>();
            services.AddScoped<PageService>();
            services.AddTransient<DeliveryService>();
            services.AddTransient<ProductService>();
            services.AddTransient<SaveFileDialogService>();
            services.AddTransient<OrderService>();
            services.AddSingleton<DocumentService>();
            services.AddTransient<ManufacturerService>();
            #endregion


            #region ViewModels

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SignInPageViewModel>();
            services.AddTransient<ProductsPageViewModel>();
            services.AddTransient<AuthorizedUserUserControlViewModel>();
            services.AddTransient<CartPageViewModel>();
            services.AddTransient<EditProductPageViewModel>();
            services.AddTransient<AddProductPageViewModel>();
            services.AddTransient<SignUpPageViewModel>();
            services.AddTransient<OrdersPageViewModel>();
            services.AddTransient<ManufacturerPageViewModel>();
            services.AddTransient<DeliveryPageViewModel>();

            #endregion


            #region Database Contexts
            services.AddDbContext<TradeContext>(options =>
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TradeDatabase"].ConnectionString;
                options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }, 
            ServiceLifetime.Transient);
            #endregion

            _provider = services.BuildServiceProvider();

            foreach (var service in services)
            {
                _provider.GetRequiredService(service.ServiceType);
            }
        }
        #region ViewModels
        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        public SignInPageViewModel SignInPageViewModel => _provider.GetRequiredService<SignInPageViewModel>();
        public ProductsPageViewModel ProductsPageViewModel => _provider.GetRequiredService<ProductsPageViewModel>();
        public AuthorizedUserUserControlViewModel AuthorizedUserUserControlViewModel => _provider.GetRequiredService<AuthorizedUserUserControlViewModel>();
        public CartPageViewModel CartPageViewModel => _provider?.GetRequiredService<CartPageViewModel>();
        public EditProductPageViewModel EditProductPageViewModel => _provider.GetRequiredService<EditProductPageViewModel>();
        public AddProductPageViewModel AddProductPageViewModel => _provider.GetRequiredService<AddProductPageViewModel>();
        public SignUpPageViewModel SignUpPageViewModel => _provider.GetRequiredService<SignUpPageViewModel>();
        public OrdersPageViewModel OrdersPageViewModel => _provider.GetRequiredService<OrdersPageViewModel>();
        public ManufacturerPageViewModel ManufacturerPageViewModel => _provider.GetRequiredService<ManufacturerPageViewModel>();
        public DeliveryPageViewModel DeliveryPageViewModel => _provider.GetRequiredService<DeliveryPageViewModel>();
        #endregion
    }
}
