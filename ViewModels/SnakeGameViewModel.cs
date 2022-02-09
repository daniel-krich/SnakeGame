using SnakeGame.Models.Snake;
using SnakeGame.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.ViewModels
{
    public class SnakeGameViewModel : BaseViewModel
    {
        public SnakeGameLogic _snakeGame;
        public SnakeGameViewModel(IPageNavigator navigator)
        {
            
        }
    }
}
