using SnakeGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SnakeGame.Snake
{
    public class SnakeGameSettings : ObservableObject
    {
        private Brush _boardColor;
        private Brush _snakeColor;
        private Brush _snakeHeadColor;
        private Brush _appleColor;
        private int _pixelWidth;
        private int _pixelHeight;
        private int _pixelScale;

        public SnakeGameSettings()
        {
            PixelScale = 26;
            PixelWidth = 500;
            PixelHeight = 500;
            BoardColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            SnakeColor = new SolidColorBrush(Color.FromRgb(36, 143, 36));
            SnakeHeadColor = new SolidColorBrush(Color.FromRgb(20, 82, 20));
            AppleColor = new SolidColorBrush(Color.FromRgb(255, 51, 51));
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

        public int PixelWidth
        {
            get => _pixelWidth;
            set
            {
                _pixelWidth = value;
                OnPropertyChanged();
            }
        }

        public int PixelHeight
        {
            get => _pixelHeight;
            set
            {
                _pixelHeight = value;
                OnPropertyChanged();
            }
        }

        public int PixelScale
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
