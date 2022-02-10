using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Snake
{
    public class SnakeBody : Shape, IEnumerable<SnakeBody>
    {
        private SnakeEvents _snakeEvents;
        private SnakeGameSettings _snakeGameSettings;
        private SnakeCollection _snakeCollection;
        private SnakeDirection _snakeDirection;
        protected override Geometry DefiningGeometry { get => new RectangleGeometry(new Rect(new Point(0, 0), new Size(Width, Height))); }

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

        public SnakeBody Previous { get; set; }
        public SnakeBody Next { get; set; }

        public SnakeBody(SnakeEvents snakeEvents, SnakeGameSettings snakeGameSettings, SnakeCollection snakeCollection)
        {
            _snakeCollection = snakeCollection;
            _snakeGameSettings = snakeGameSettings;
            _snakeEvents = snakeEvents;
            _snakeEvents.SnakeTryDirectionChanged += OnDirectionChange;
            _snakeEvents.SnakeGameOver += OnGameEnd;
        }

        public void AddSnakeJoint()
        {
            SnakeBody lastPart = Last();

            SnakeBody newHead = new SnakeBody(_snakeEvents, _snakeGameSettings, _snakeCollection)
            {
                Width = _snakeGameSettings.PixelWidth / _snakeGameSettings.PixelScale,
                Height = _snakeGameSettings.PixelHeight / _snakeGameSettings.PixelScale,
                Fill = _snakeGameSettings.SnakeHeadColor,
                Left = lastPart.Left,
                Top = lastPart.Top
            };

            RepositionHead(newHead);

            newHead.Previous = lastPart;

            lastPart.Fill = _snakeGameSettings.SnakeColor; // change former head to body part color
            lastPart.Next = newHead; // reference the next object

            _snakeCollection.Add(newHead); // add to collection for render
        }

        private void RepositionHead(SnakeBody head)
        {
            if (_snakeDirection == SnakeDirection.Left || _snakeDirection == SnakeDirection.Pause)
                head.Left -= head.Width;
            else if (_snakeDirection == SnakeDirection.Right)
                head.Left += head.Width;
            else if (_snakeDirection == SnakeDirection.Up)
                head.Top -= head.Height;
            else if (_snakeDirection == SnakeDirection.Down)
                head.Top += head.Height;
        }

        public void SnakeMove()
        {
            SnakeBody cursor = Last();
            double lastLeft = cursor.Left, lastTop = cursor.Top;
            //
            RepositionHead(cursor);
            //
            while ((cursor = cursor?.Previous) != null)
            {
                var currLeft = cursor.Left;
                var currTop = cursor.Top;
                //
                cursor.Left = lastLeft;
                cursor.Top = lastTop;
                //
                lastLeft = currLeft;
                lastTop = currTop;
            }
            
        }

        public void OnDirectionChange(SnakeDirection snakeDirection)
        {
            //Todo: make a cleaner if statement....

            if(_snakeDirection == SnakeDirection.Left &&
                snakeDirection == SnakeDirection.Right ||
                _snakeDirection == SnakeDirection.Right &&
                snakeDirection == SnakeDirection.Left) // can't go from right to left or vise versa
            {
                
            }
            else if (_snakeDirection == SnakeDirection.Up &&
                snakeDirection == SnakeDirection.Down ||
                _snakeDirection == SnakeDirection.Down &&
                snakeDirection == SnakeDirection.Up) // can't go from up to down or vise versa
            {
                
            }
            else
            {
                _snakeDirection = snakeDirection;
            }
        }

        public SnakeBody Last()
        {
            SnakeBody cursor = this;
            while (cursor?.Next != null)
                cursor = cursor.Next;
            return cursor;
        }

        public void OnGameEnd()
        {

        }

        public IEnumerator<SnakeBody> GetEnumerator()
        {
            SnakeBody cursor = this;
            while (cursor != null)
            {
                yield return cursor;
                cursor = cursor?.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
