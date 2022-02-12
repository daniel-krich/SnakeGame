namespace SnakeGame.Snake.Contracts
{
    public interface ISnakeScore
    {
        int Score { get; set; }
        int SnakeSpeed { get; }

        void OnAppleEaten(bool isbadapple);
        void OnGameEnd();
        void OnGameStart();
    }
}