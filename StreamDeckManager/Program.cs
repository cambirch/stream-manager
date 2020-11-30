using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreamDeckManager.Actions;
using StreamDeckManager.Managers;
using StreamDeckManager.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamDeckManager
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using IHost _host = new HostBuilder()
                    .ConfigureAppConfiguration((context, configurationBuilder) =>
                    {
                        configurationBuilder.SetBasePath(context.HostingEnvironment.ContentRootPath);
                        configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        services.AddSingleton<frmMain>();
                        services.AddTransient<frmTallyManager>();
                        services.AddOptions();

                        // Configuration
                        services.Configure<OBSConnectionSettings>(context.Configuration.GetSection("OBSConnection"));
                        services.Configure<StreamDeckSettings>(context.Configuration.GetSection("StreamDeck"));
                        services.Configure<RemoteTallySettings>(context.Configuration.GetSection("RemoteTallies"));

                        // Add Managers
                        services.AddSingleton<ObsManager>();
                        services.AddSingleton<ButtonsManager>();
                        services.AddSingleton<ActionFactory>();

                        services.AddSingleton<GetTallyInfo>();
                        services.AddSingleton<TallyManager>();
                    })
                    .Build();
            _host.Start();

            IConfigurationRoot configuration = (IConfigurationRoot)_host.Services.GetService<IConfiguration>();
            IHostEnvironment hostEnvironment = _host.Services.GetService<IHostEnvironment>();

            // Monitor the config file for changes
            using var watcher = new FileSystemWatcher() {
                Path = hostEnvironment.ContentRootPath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "appsettings.json",
            };
            watcher.Changed += async (_, e) => {
                await Task.Delay(500);
                configuration.Reload();
            };
            watcher.EnableRaisingEvents = true;

            var mainWindow = _host.Services.GetRequiredService<frmMain>();
            Application.Run(mainWindow);
        }
    }
}
