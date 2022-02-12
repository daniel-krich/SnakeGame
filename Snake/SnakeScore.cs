using SnakeGame.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Snake
{
    public class SnakeScore : ObservableObject
    {
        private SnakeEvents _snakeEvents;
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SnakeSpeed));
            }
        }

        public int SnakeSpeed
        {
            get
            {
                if(Score > 150)
                    return 1000 / 15;
                else if(Score > 100)
                    return 1000 / 14;
                else if (Score > 75)
                    return 1000 / 13;
                else if (Score > 50)
                    return 1000 / 12;
                else if (Score > 25)
                    return 1000 / 11;
                else if (Score >= 0)
                    return 1000 / 10;
                return 1000 / 10;
            }
        }

        public SnakeScore(SnakeEvents snakeEvents)
        {
            _snakeEvents = snakeEvents;
            _snakeEvents.SnakeAppleEaten += OnAppleEaten;
            _snakeEvents.SnakeGameStart += OnGameStart;
            _snakeEvents.SnakeGameOver += OnGameEnd;
        }

        public void OnAppleEaten(bool isbadapple)
        {
            if(!isbadapple) // add score only if it is a good apple
                Score++;
        }

        public void OnGameStart()
        {
            Score = 0;
        }

        public void OnGameEnd()
        {
        }
    }
}
