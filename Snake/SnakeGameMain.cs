using SnakeGame.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Snake
{
    public class SnakeGameMain : ObservableObject
    {
        private SnakeBody _rootSnake;
        private SnakeApple _apple;
        private bool _isplaying;
        private bool _ispaused;
        private FunctionDelayer _funcDelay;
        private SnakeEvents _snakeEvents;
        private SnakeDirection _currentDirection;

        public SnakeControls Controls { get; set; }
        public SnakeScore SnakeStats { get; set; }
        public SnakeGameSettings SnakeSettings { get; set; }
        public bool IsPaused
        {
            get => _ispaused && _isplaying;
            set
            {
                _ispaused = value;
                OnPropertyChanged();
            }
        }
        public bool IsPlaying
        {
            get => _isplaying;
            set
            {
                _isplaying = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotActive));
            }
        }

        public bool IsNotActive { get => !IsPlaying; }
        
        public SnakeCollection SnakeShapesCollection { get; set; }
        
        public SnakeGameMain(SnakeGameSettings snakeGameSettings, SnakeEvents snakeEvents,
            SnakeCollection snakeCollection, SnakeControls snakeControls,
            FunctionDelayer functionDelayer, SnakeScore snakeScore)
        {
            _funcDelay = functionDelayer;
            _snakeEvents = snakeEvents;
            Controls = snakeControls;
            SnakeStats = snakeScore;
            SnakeSettings = snakeGameSettings;
            SnakeShapesCollection = snakeCollection;
            //
        }

        private void MainLoop(object sender, EventArgs e)
        {
            _funcDelay.ChangeInt(SnakeStats.SnakeSpeed);
            _funcDelay.Do();

            SnakeBody cursor = _rootSnake.Last();

            // Border check
            if (cursor.Left > SnakeSettings.PixelWidth - (SnakeSettings.PixelWidth / SnakeSettings.PixelScale) ||
                cursor.Top > SnakeSettings.PixelHeight - (SnakeSettings.PixelHeight / SnakeSettings.PixelScale) ||
                cursor.Top < 0 ||
                cursor.Left < 0)
            {
                //Trace.WriteLine("out of bounds");
                Exit();
                return;
            }

            // Check is the snake is not "eating" itself
            foreach (SnakeBody snk in _rootSnake)
            {
                if (cursor.Left == snk.Left &&
                    cursor.Top == snk.Top &&
                    snk != cursor)
                {
                    //Trace.WriteLine("Snake override");
                    Exit();
                    return;
                }
            }

            // Apple has eaten check
            var distance = Math.Sqrt((Math.Pow(_apple.Left - cursor.Left, 2) + Math.Pow(_apple.Top - cursor.Top, 2)));
            if (distance < SnakeSettings.PixelWidth / SnakeSettings.PixelScale)
            {
                if (_apple.IsBadApple)
                {
                    _rootSnake = _rootSnake.ReverseSnake();
                    _snakeEvents.InvokeSnakeAppleEaten(_apple.IsBadApple);

                    SnakeBody headSnake = _rootSnake.Last();

                    if (headSnake.Left < headSnake.Previous.Left)
                        _snakeEvents.InvokeSnakeDirectionChanged(SnakeDirection.Left);
                    else if (headSnake.Left > headSnake.Previous.Left)
                        _snakeEvents.InvokeSnakeDirectionChanged(SnakeDirection.Right);
                    else if(headSnake.Top < headSnake.Previous.Top)
                        _snakeEvents.InvokeSnakeDirectionChanged(SnakeDirection.Up);
                    else if (headSnake.Top > headSnake.Previous.Top)
                        _snakeEvents.InvokeSnakeDirectionChanged(SnakeDirection.Down);

                }
                else
                {
                    _rootSnake.AddSnakeJoint();
                    _snakeEvents.InvokeSnakeAppleEaten(_apple.IsBadApple);
                }
            }
            //
            
        }
        public void Start()
        {
            _snakeEvents.InvokeSnakeGameStart();

            _rootSnake = new SnakeBody(_snakeEvents, SnakeSettings, SnakeShapesCollection);
            _rootSnake.AddSnakeJoint();
            _rootSnake.AddSnakeJoint();
            SnakeShapesCollection.Add(_rootSnake);
            //
            _apple = new SnakeApple(_snakeEvents, SnakeSettings, SnakeShapesCollection);
            SnakeShapesCollection.Add(_apple);
            _apple.GenerateApple();
            //
            _funcDelay.CreateDo(() => _rootSnake.SnakeMove(), SnakeStats.SnakeSpeed);

            CompositionTarget.Rendering += MainLoop;
            _snakeEvents.SnakeDirectionChanged += OnDirectionChange;

            IsPlaying = true;
            IsPaused = true;
        }

        public void OnDirectionChange(SnakeDirection snakeDirection)
        {
            _currentDirection = snakeDirection;
            IsPaused = false;
        }

        public void Exit()
        {
            SnakeShapesCollection.Clear();
            CompositionTarget.Rendering -= MainLoop;
            _snakeEvents.SnakeDirectionChanged -= OnDirectionChange;
            IsPlaying = false;
            _snakeEvents.InvokeSnakeGameOver();
        }
    }
}
