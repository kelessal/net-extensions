using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Net.Extensions
{
    public class Debouncer
    {
        private readonly Timer _timer;
        private bool _isTimerActive;
        private Action _action;
        public Debouncer(int interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += OnTimedEvent;
        }
        
        public void Debouce(Action action)
        {
            if (_isTimerActive)
            {
                _timer.Stop();
            }
            _action = action;
            _timer.Start();
            _isTimerActive = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _isTimerActive = false;
            ((Timer)source).Stop();
            this._action();
        }
    }
}
