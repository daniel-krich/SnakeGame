using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using SnakeGame.Core;
using SnakeGame.Snake.Contracts;
using System.Windows.Media.Imaging;

namespace SnakeGame.Snake
{
    public class SnakeApple : Shape, ISnakeApple
    {
        private ISnakeEvents _snakeEvents;
        private ISnakeGameSettings _snakeGameSettings;
        private SnakeCollection _snakeCollection;
        protected override Geometry DefiningGeometry { get => new EllipseGeometry(new Rect(new Point(0, 0), new Size(Width, Height))); }

        public double Left
        {
            get => Canvas.GetLeft(this);
            set => Canvas.SetLeft(this, value);
        }

        public double Top
        {
            get => Canvas.GetTop(this);
            set => Canvas.SetTop(this, value);
        }

        public bool IsBadApple { get; set; }

        public SnakeApple(ISnakeEvents snakeEvents, ISnakeGameSettings snakeGameSettings, SnakeCollection snakeCollection)
        {
            _snakeEvents = snakeEvents;
            _snakeGameSettings = snakeGameSettings;
            _snakeCollection = snakeCollection;
            _snakeEvents.SnakeAppleEaten += OnAppleEaten;
            _snakeEvents.SnakeGameOver += OnGameEnd;
        }

        public void OnAppleEaten(bool isbadapple)
        {
            GenerateAppleTypeAndPosition();
        }

        public void OnGameEnd()
        {

        }

        public void GenerateAppleTypeAndPosition()
        {
            int elements = 0;
            double randLeft, randTop;
            do
            {
                randLeft = Utilities.Random.Next((int)(_snakeGameSettings.PixelWidth / _snakeGameSettings.PixelScale), (int)(_snakeGameSettings.PixelWidth - (int)(_snakeGameSettings.PixelWidth / _snakeGameSettings.PixelScale)));
                randTop = Utilities.Random.Next((int)(_snakeGameSettings.PixelHeight / _snakeGameSettings.PixelScale), (int)(_snakeGameSettings.PixelHeight - (int)(_snakeGameSettings.PixelHeight / _snakeGameSettings.PixelScale)));
                elements = _snakeCollection.Where(s =>
                {
                    if (s is SnakeBody sb)
                    {
                        var distance = Math.Sqrt((Math.Pow(sb.Left - randLeft, 2) + Math.Pow(sb.Top - randTop, 2)));
                        if (distance < _snakeGameSettings.PixelWidth / _snakeGameSettings.PixelScale)
                            return true;
                        else
                            return false;
                    }
                    else return false;
                }).Count();
            } while (elements > 0);

            if (Utilities.ChanceToBoolean(15)) // 15 percent chance for a bad apple
            {
                
                Fill = _snakeGameSettings.BadAppleColor;
                IsBadApple = true;
            }
            else
            {
                Fill = _snakeGameSettings.AppleColor;
                IsBadApple = false;
            }

            Left = randLeft;
            Top = randTop;
            Width = _snakeGameSettings.PixelWidth / _snakeGameSettings.PixelScale;
            Height = _snakeGameSettings.PixelHeight / _snakeGameSettings.PixelScale;
        }
    }
}
