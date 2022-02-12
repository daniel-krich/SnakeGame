using SnakeGame.Core;

namespace SnakeGame.Snake.Contracts
{
    public interface ISnakeControls
    {
        RelayCommand CommandDown { get; set; }
        RelayCommand CommandLeft { get; set; }
        RelayCommand CommandRight { get; set; }
        RelayCommand CommandUp { get; set; }
    }
}