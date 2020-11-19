using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PizzastycznieFrontend.ApiHandler;
using PizzastycznieFrontend.Authentication;
using PizzastycznieFrontend.Forms;

namespace PizzastycznieFrontend
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var mainForm = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();
            
            services.Configure<AppSettings>(config.GetSection("App"));

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddMemoryCache();
            services.AddHttpClient();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IApiHandler, ApiHandler.ApiHandler>();
            services.AddScoped<MainForm>();
            services.AddScoped<LoginForm>();
        }
    }
}