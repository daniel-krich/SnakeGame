using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SnakeGame.ViewModels;

namespace SnakeGame.Services
{
    public class PageNavigator : INotifyPropertyChanged, IPageNavigator
    {
        private BaseViewModel _currentPage;
        private IServiceProvider _serviceProvider;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action PageChanged;
        public BaseViewModel CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                PageChanged?.Invoke();
            }
        }

        public PageNavigator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<T>() =>
            CurrentPage = _serviceProvider.GetRequiredService<T>() as BaseViewModel;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        
    }
}
