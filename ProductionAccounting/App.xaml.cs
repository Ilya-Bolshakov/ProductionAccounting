using Microsoft.Extensions.Hosting;
using ProductionAccounting.Registrators;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductionAccounting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window ActiveWindow => Application.Current.Windows.OfType<Window>()
            .FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow => Application.Current.Windows.OfType<Window>()
            .FirstOrDefault(w => w.IsFocused);

        public static Window CurrentWindow => FocusedWindow ?? ActiveWindow;

        private static IHost _host;

        public static IHost Host => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        public static IHostBuilder CreateHostBuilder(string[] args) => Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(
                 (hostContext, services) => services
                    .AddServices()
                    .AddViewModels()
                    .AddDb(hostContext.Configuration.GetSection("Database"))
                    .AddRepositories()
                 );


        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();
            using var serv = Services.CreateAsyncScope();
            var db = serv.ServiceProvider.GetRequiredService<ProductionAccountingContext>();
            await db.Database.MigrateAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            using var serv = Services.CreateAsyncScope();
            var db = serv.ServiceProvider.GetRequiredService<ProductionAccountingContext>();
            //await db.Database.EnsureDeletedAsync();
            base.OnExit(e);
            await host.StopAsync();
        }
    }
}
