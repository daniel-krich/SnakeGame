using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SnakeGame.ViewModels;
using SnakeGame.Views;
using SnakeGame.Core;
using SnakeGame.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider services = AppServices.ConfigureDependencies();
            MainWindow = services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
