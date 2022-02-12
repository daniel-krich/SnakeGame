using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core
{
    public static class Utilities
    {
        public static Random Random { get; } = new Random();

        public static bool ChanceToBoolean(int percentage)
        {
            if (percentage > 100 || percentage < 0)
                throw new ArgumentOutOfRangeException("Allowed range 0-100");

            if (Random.Next(100 / percentage) == (100 / percentage) - 1)
                return true;
            else
                return false;
        }
    }
}
