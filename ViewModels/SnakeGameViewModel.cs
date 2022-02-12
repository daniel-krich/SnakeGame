using SnakeGame.Core;
using SnakeGame.Snake;
using SnakeGame.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SnakeGame.Snake.Contracts;

namespace SnakeGame.ViewModels
{
    
    public class SnakeGameViewModel : BaseViewModel
    {
        public ISnakeGameMain SnakeGame { get; set; }

        public RelayCommand Replay { get; set; }
        public RelayCommand NavToMenu { get; set; }
        public SnakeGameViewModel(IPageNavigator navigator, ISnakeGameMain snakeGameLogic)
        {
            SnakeGame = snakeGameLogic;
            SnakeGame.Start();

            NavToMenu = new RelayCommand(o => {
                SnakeGame.Exit();
                navigator.NavigateTo<MenuViewModel>();
            });
            Replay = new RelayCommand(o => SnakeGame.Start());
        }
    }
}
