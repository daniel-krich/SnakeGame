using SnakeGame.Snake.Contracts;

namespace SnakeGame.Snake.Contracts
{
    public interface ISnakeGameMain
    {
        ISnakeControls Controls { get; set; }
        bool IsNotActive { get; }
        bool IsPaused { get; set; }
        bool IsPlaying { get; set; }
        ISnakeGameSettings SnakeSettings { get; set; }
        SnakeCollection SnakeShapesCollection { get; set; }
        ISnakeScore SnakeStats { get; set; }

        void Exit();
        void Start();
    }
}