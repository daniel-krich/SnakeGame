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
using SnakeGame.Snake.Contracts;

namespace SnakeGame.Core
{
    public static class AppServices
    {
        public static IServiceProvider ConfigureDependencies()
        {
            IServiceCollection service = new ServiceCollection();

            service.AddTransient<ISnakeGameMain, SnakeGameMain>();
            service.AddTransient<ISnakeControls, SnakeControls>();
            service.AddSingleton<SnakeCollection>();
            service.AddSingleton<ISnakeScore, SnakeScore>();
            service.AddSingleton<ISnakeGameSettings, SnakeGameSettings>();
            service.AddSingleton<ISnakeEvents, SnakeEvents>();

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
