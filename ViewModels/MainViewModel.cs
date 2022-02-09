using SnakeGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private IPageNavigator _pageNavigator;
        public string AppTitle { get => "SnakeGame"; }
        public BaseViewModel Page { get => _pageNavigator.CurrentPage; }
        public MainViewModel(IPageNavigator pageNavigator)
        {
            
            _pageNavigator = pageNavigator;
            _pageNavigator.PageChanged += OnPageChange;
            _pageNavigator.NavigateTo<MenuViewModel>();
        }

        public void OnPageChange() => OnPropertyChanged(nameof(Page));
        
    }
}
