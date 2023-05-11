using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Net.Extensions.Test
{
    public class RateLimitTests
    {
        [Fact]
        public async void DebounceTest()
        {
            var debouncer = new Debouncer(2000);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            debouncer.Debouce( () =>
            {
                stopWatch.Stop();
            });
            await Task.Delay(1000);

            debouncer.Debouce( () =>
            {
                stopWatch.Stop();
            });
            await Task.Delay(10000);
            var total = stopWatch.ElapsedMilliseconds;
        }
        [Fact]
        public async void ThrottleTest()
        {
            var throttler = new Throttler(2000);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            throttler.Throttle(() =>
            {
                stopWatch.Stop();
            });
            await Task.Delay(1000);
            throttler.Throttle(() =>
            {
                stopWatch.Stop();
            });
            await Task.Delay(5000);
            var total=stopWatch.ElapsedMilliseconds;
        }
    }
}
