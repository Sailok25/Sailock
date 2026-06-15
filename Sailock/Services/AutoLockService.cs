using System;
using System.Windows.Threading;

namespace Sailock.Services
{
    public class AutoLockService
    {
        private DispatcherTimer _timer;
        private Action _onLock;
        private TimeSpan _timeout = TimeSpan.FromMinutes(2);

        public void Enable(Action onLock, TimeSpan? timeout = null)
        {
            _onLock = onLock;

            if (timeout.HasValue)
                _timeout = timeout.Value;

            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Tick += (s, e) =>
                {
                    _timer.Stop();
                    _onLock?.Invoke();
                };
            }

            _timer.Stop();
            _timer.Interval = _timeout;
            _timer.Start();
        }

        public void Disable()
        {
            _timer?.Stop();
            _onLock = null;
        }

        public void Reset()
        {
            if (_timer != null && _onLock != null)
            {
                _timer.Stop();
                _timer.Start();
            }
        }

        public static TimeSpan ParseTimeout(string option)
        {
            return option switch
            {
                "30 sec" => TimeSpan.FromSeconds(30),
                "1 min" => TimeSpan.FromMinutes(1),
                "5 min" => TimeSpan.FromMinutes(5),
                _ => TimeSpan.FromMinutes(2) // "2 min" es el default
            };
        }
    }
}