using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using SnakeGame.ViewModels;
using SnakeGame.Services;
using SnakeGame.Snake;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SnakeGame.Core
{
    public static class AppServices
    {
        public static IServiceProvider ConfigureDependencies()
        {
            IServiceCollection service = new ServiceCollection();

            service.AddTransient<SnakeScore>();
            service.AddTransient<SnakeGameMain>();
            service.AddTransient<SnakeControls>();
            service.AddSingleton<SnakeCollection>();
            service.AddSingleton<SnakeGameSettings>();
            service.AddSingleton<SnakeEvents>();

            service.AddTransient<FunctionDelayer>();

            //ViewModels
            service.AddTransient<MenuViewModel>();
            service.AddTransient<SnakeGameViewModel>();

            //Singletons
            service.AddSingleton<IPageNavigator, PageNavigator>();
            service.AddSingleton<MainViewModel>();

            service.AddSingleton<MainWindow>(o => new MainWindow()
            {
                DataContext = o.GetRequiredService<MainViewModel>()
            });
            return service.BuildServiceProvider();
        }
    }
}
