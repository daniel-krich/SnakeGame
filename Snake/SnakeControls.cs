using SnakeGame.Core;
using SnakeGame.Snake.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Snake
{
    public class SnakeControls : ISnakeControls
    {
        private ISnakeEvents _snakeEvents;
        public RelayCommand CommandLeft { get; set; }
        public RelayCommand CommandUp { get; set; }
        public RelayCommand CommandRight { get; set; }
        public RelayCommand CommandDown { get; set; }

        public SnakeControls(ISnakeEvents snakeEvents)
        {
            _snakeEvents = snakeEvents;

            CommandLeft = new RelayCommand(o =>
                _snakeEvents.InvokeSnakeTryDirectionChanged(SnakeDirection.Left)
            );

            CommandUp = new RelayCommand(o =>
                _snakeEvents.InvokeSnakeTryDirectionChanged(SnakeDirection.Up)
            );

            CommandRight = new RelayCommand(o =>
                _snakeEvents.InvokeSnakeTryDirectionChanged(SnakeDirection.Right)
            );

            CommandDown = new RelayCommand(o =>
                _snakeEvents.InvokeSnakeTryDirectionChanged(SnakeDirection.Down)
            );
        }
    }
}
