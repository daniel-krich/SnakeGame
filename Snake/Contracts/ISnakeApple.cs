namespace SnakeGame.Snake.Contracts
{
    public interface ISnakeApple
    {
        bool IsBadApple { get; set; }
        double Left { get; set; }
        double Top { get; set; }

        void GenerateAppleTypeAndPosition();
        void OnAppleEaten(bool isbadapple);
        void OnGameEnd();
    }
}