using System;
using System.Threading;

namespace Mail.IntegrationTests.Helper
{
    public class Wait
    {
        private readonly int _timeout;
        private readonly int _interval;
        private const int DefaultInterval = 100;
        private Wait(TimeSpan timeout, int interval = DefaultInterval)
        {
            _interval = interval;
            _timeout = (int)timeout.TotalMilliseconds;
        }

        public static Wait For(TimeSpan timing)
        {
            return new Wait(timing);
        }

        public void Until(Func<bool> condition)
        {
            Until(condition,
                () => 
                {
                    throw new TimeoutException($"Expected {condition} after waiting for {_timeout} milliseconds");
                });
        }

        public void Until(Func<bool> condition, Action success, Action failure)
        {
            var timeIsUp = DateTime.Now.AddMilliseconds(_timeout);
            while (DateTime.Now.CompareTo(timeIsUp) < 0)
            {
                if (condition())
                {
                    success();
                    return;
                }
                new ManualResetEvent(false).WaitOne(_interval);
            }
            failure();
        }

        public void Until(Func<bool> condition, Action failure)
        {
            Until(condition, () => { }, failure);
        }

        public void ThenContinue()
        {
            new ManualResetEvent(false).WaitOne(_timeout);
        }
    }
}
