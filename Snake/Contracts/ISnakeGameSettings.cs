using System.Windows.Media;

namespace SnakeGame.Snake.Contracts
{
    public interface ISnakeGameSettings
    {
        Brush AppleColor { get; set; }
        Brush BadAppleColor { get; set; }
        Brush BoardColor { get; set; }
        double PixelHeight { get; set; }
        double PixelScale { get; set; }
        double PixelWidth { get; set; }
        Brush SnakeColor { get; set; }
        Brush SnakeHeadColor { get; set; }
    }
}