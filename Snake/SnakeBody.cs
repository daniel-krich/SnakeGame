using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        public SnakeDirection _snakeDirection;
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
            _snakeEvents.SnakeTryDirectionChanged += OnTryDirectionChange;
            _snakeEvents.SnakeDirectionChanged += OnForceDirectionChange;
            _snakeEvents.SnakeGameOver += OnGameEnd;
        }

        /// <summary>
        /// Creates a new head to the snake, thus increasing the snake length by 1.
        /// </summary>
        public void AddSnakeJoint()
        {
            SnakeBody lastPart = Last(); // current head
            if (lastPart == this)
            {
                // init the tail or the "root"
                this.Width = _snakeGameSettings.PixelWidth / _snakeGameSettings.PixelScale;
                this.Height = _snakeGameSettings.PixelHeight / _snakeGameSettings.PixelScale;
                this.Fill = _snakeGameSettings.SnakeHeadColor;
                this.Left = _snakeGameSettings.PixelWidth / 2;
                this.Top = _snakeGameSettings.PixelHeight / 2;
            }

            SnakeBody newHead = new SnakeBody(_snakeEvents, _snakeGameSettings, _snakeCollection)
            {
                Width = lastPart.Width,
                Height = lastPart.Height,
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

        /// <summary>
        /// reverses the snake.
        /// </summary>
        /// <returns>returns the new "root" / snake tail</returns>
        public SnakeBody ReverseSnake()
        {
            SwapSnakeColor();
            return ReverseSnakeNodes();
        }

        /// <summary>
        /// Reverses the snake nodes.
        /// </summary>
        /// <returns>Returns the new "root" / snake tail</returns>
        private SnakeBody ReverseSnakeNodes()
        {
            SnakeBody Cursor = this;
            while (true)
            {
                var cacheNext = Cursor.Next;
                Cursor.Next = Cursor.Previous;
                Cursor.Previous = cacheNext;

                if (cacheNext is not null)
                    Cursor = cacheNext;
                else
                    return Cursor;
            }
        }


        /// <summary>
        /// Head and tail exchange colors
        /// </summary>
        private void SwapSnakeColor()
        {
            SnakeBody startCursor = this;
            SnakeBody endCursor = Last();
            if (startCursor != null && endCursor != null)
            {
                var cacheColor = startCursor.Fill;
                startCursor.Fill = endCursor.Fill;
                endCursor.Fill = cacheColor;
            }
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
            if (_snakeDirection == SnakeDirection.Pause) return;
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

        public void OnTryDirectionChange(SnakeDirection snakeDirection)
        {
            if (this.Previous != null && this.Next == null) // Looking only for the "Head".
            {
                if (snakeDirection == SnakeDirection.Left && this.Left <= this.Previous.Left ||
                    snakeDirection == SnakeDirection.Right && this.Left >= this.Previous.Left ||
                    snakeDirection == SnakeDirection.Up && this.Top <= this.Previous.Top ||
                    snakeDirection == SnakeDirection.Down && this.Top >= this.Previous.Top)
                {
                    _snakeDirection = snakeDirection;
                    _snakeEvents.InvokeSnakeDirectionChanged(snakeDirection);
                }
            }
        }

        public void OnForceDirectionChange(SnakeDirection snakeDirection)
        {
            Trace.WriteLine(snakeDirection.ToString());
            _snakeDirection = snakeDirection;
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
            _snakeEvents.SnakeTryDirectionChanged -= OnTryDirectionChange;
            _snakeEvents.SnakeDirectionChanged -= OnForceDirectionChange;
            _snakeEvents.SnakeGameOver -= OnGameEnd;
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
