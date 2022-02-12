using System.Collections.Generic;

namespace SnakeGame.Snake.Contracts
{
    public interface ISnakeBody
    {
        double Left { get; set; }
        SnakeBody Next { get; set; }
        SnakeBody Previous { get; set; }
        double Top { get; set; }

        void AddSnakeJoint();
        IEnumerator<SnakeBody> GetEnumerator();
        SnakeBody Last();
        void OnForceDirectionChange(SnakeDirection snakeDirection);
        void OnGameEnd();
        void OnTryDirectionChange(SnakeDirection snakeDirection);
        SnakeBody ReverseSnake();
        void SnakeMove();
    }
}