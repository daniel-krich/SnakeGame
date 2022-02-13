using SnakeGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Snake.Contracts;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakeGame.Snake
{
    public class SnakeGameSettings : ObservableObject, ISnakeGameSettings
    {
        private Brush _boardColor;
        private Brush _snakeColor;
        private Brush _snakeHeadColor;
        private Brush _appleColor;
        private Brush _badAppleColor;
        private double _pixelWidth;
        private double _pixelHeight;
        private double _pixelScale;

        public SnakeGameSettings()
        {
            ImageBrush BadAppleBrush = new ImageBrush();
            BadAppleBrush.ImageSource = new BitmapImage(new Uri(@"Images/BadApple.png", UriKind.Relative));

            ImageBrush AppleBrush = new ImageBrush();
            AppleBrush.ImageSource = new BitmapImage(new Uri(@"Images/GoodApple.png", UriKind.Relative));

            PixelScale = 26.0;
            PixelWidth = 500.0;
            PixelHeight = 500.0;
            BoardColor = new SolidColorBrush(Color.FromRgb(242, 242, 242));
            SnakeColor = new SolidColorBrush(Color.FromRgb(0, 230, 77));
            SnakeHeadColor = new SolidColorBrush(Color.FromRgb(0, 204, 68));
            AppleColor = AppleBrush;
            BadAppleColor = BadAppleBrush;
        }

        public Brush BoardColor
        {
            get => _boardColor;
            set
            {
                _boardColor = value;
                OnPropertyChanged();
            }
        }

        public Brush SnakeColor
        {
            get => _snakeColor;
            set
            {
                _snakeColor = value;
                OnPropertyChanged();
            }
        }

        public Brush SnakeHeadColor
        {
            get => _snakeHeadColor;
            set
            {
                _snakeHeadColor = value;
                OnPropertyChanged();
            }
        }

        public Brush AppleColor
        {
            get => _appleColor;
            set
            {
                _appleColor = value;
                OnPropertyChanged();
            }
        }

        public Brush BadAppleColor
        {
            get => _badAppleColor;
            set
            {
                _badAppleColor = value;
                OnPropertyChanged();
            }
        }

        public double PixelWidth
        {
            get => _pixelWidth;
            set
            {
                _pixelWidth = value;
                OnPropertyChanged();
            }
        }

        public double PixelHeight
        {
            get => _pixelHeight;
            set
            {
                _pixelHeight = value;
                OnPropertyChanged();
            }
        }

        public double PixelScale
        {
            get => _pixelScale;
            set
            {
                _pixelScale = value;
                OnPropertyChanged();
            }
        }
    }
}
