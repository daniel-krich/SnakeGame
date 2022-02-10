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
                if (Score > 100)
                    return 50;
                else if (Score > 50)
                    return 100;
                else if (Score > 25)
                    return 150;
                else if (Score >= 0)
                    return 200;
                return 200;
            }
        }

        public SnakeScore(SnakeEvents snakeEvents)
        {
            _snakeEvents = snakeEvents;
            _snakeEvents.SnakeAppleEaten += OnAppleEaten;
            _snakeEvents.SnakeGameStart += OnGameStart;
            _snakeEvents.SnakeGameOver += OnGameEnd;
        }

        public void OnAppleEaten()
        {
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
