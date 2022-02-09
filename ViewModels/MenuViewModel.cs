using SnakeGame.Core;
using SnakeGame.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public RelayCommand Play { get; set; }
        public RelayCommand Exit { get; set; }
        public MenuViewModel(IPageNavigator navigator)
        {
            Play = new RelayCommand(o => navigator.NavigateTo<SnakeGameViewModel>());
            Exit = new RelayCommand(o => App.Current.Shutdown());
        }
    }
}
