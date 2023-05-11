using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Net.Extensions
{
    public class Throttler
    {
        private readonly Timer _timer;
        private bool _isTimerActive;
        private Action _action;
        public Throttler(int interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += OnTimedEvent;
        }
        public void Throttle(Action action)
        {
            if (!_isTimerActive)
            {
                _isTimerActive = true;
                _timer.Start();
            }
            _action = action;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _isTimerActive = false;
            ((Timer)source).Stop();
            this._action();
        }
    }
}
