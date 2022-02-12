using System;

namespace SnakeGame.Snake.Contracts
{
    public interface ISnakeEvents
    {
        event Action<bool> SnakeAppleEaten;
        event Action<SnakeDirection> SnakeDirectionChanged;
        event Action SnakeGameOver;
        event Action SnakeGameStart;
        event Action<SnakeDirection> SnakeTryDirectionChanged;

        void InvokeSnakeAppleEaten(bool isbadapple);
        void InvokeSnakeDirectionChanged(SnakeDirection param);
        void InvokeSnakeGameOver();
        void InvokeSnakeGameStart();
        void InvokeSnakeTryDirectionChanged(SnakeDirection param);
    }
}