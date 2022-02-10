using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core
{
    public class FunctionDelayer
    {
        private Action _action;
        private long _timeInt;
        private long _currentTimeWithInterval;
        public FunctionDelayer() { }

        public FunctionDelayer(Action action, int time) => CreateDo(action, time);

        public void CreateDo(Action action, int time)
        {
            _action = action;
            _timeInt = time * 10000;
        }

        public void ChangeInt(long time)
        {
            _timeInt = time * 10000;
        }

        public void Do()
        {
            if (_currentTimeWithInterval < DateTime.UtcNow.Ticks)
            {
                _action?.Invoke();
                _currentTimeWithInterval = DateTime.UtcNow.Ticks + _timeInt;
            }
        }
    }
}
