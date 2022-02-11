using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Snake
{
    public enum SnakeDirection
    {
        Pause,
        Left,
        Up,
        Right,
        Down
    }

    public class SnakeEvents
    {
        public event Action<SnakeDirection> SnakeTryDirectionChanged;
        public event Action<SnakeDirection> SnakeDirectionChanged;
        public event Action SnakeAppleEaten;
        public event Action SnakeGameOver;
        public event Action SnakeGameStart;

        public void InvokeSnakeTryDirectionChanged(SnakeDirection param) => SnakeTryDirectionChanged?.Invoke(param);
        public void InvokeSnakeDirectionChanged(SnakeDirection param) => SnakeDirectionChanged?.Invoke(param);
        public void InvokeSnakeAppleEaten() => SnakeAppleEaten?.Invoke();
        public void InvokeSnakeGameOver() => SnakeGameOver?.Invoke();
        public void InvokeSnakeGameStart() => SnakeGameStart?.Invoke();
    }
}
