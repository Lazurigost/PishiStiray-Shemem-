﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PishiStirayNET.Data;
using PishiStirayNET.Services;
using PishiStirayNET.VeiwModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStirayNET
{
    internal class DI
    {
        public static ServiceProvider? _provider;

        public static void Init()
        {
            ServiceCollection services = new ServiceCollection();

            #region Services
            services.AddSingleton<UserService>();

            #endregion


            #region ViewModels

            services.AddTransient<MainWindowViewModel>();

            #endregion


            #region Database Contexts
            services.AddDbContext<TradeContext>(options =>
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TradeDatabase"].ConnectionString;
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            #endregion

            _provider = services.BuildServiceProvider();

            foreach (var service in services)
            {
                _provider.GetRequiredService(service.ServiceType);
            }
        }

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
    }
}