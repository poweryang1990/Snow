using System;
using Xunit;

namespace UOKOFramework.Test
{
    public class ClockTest
    {
        [Fact]
        public void when_set_Now_should_return_set_time()
        {
            var now = DateTimeOffset.Parse("2017-09-14 12:34:56");

            IClock clock = new Clock
            {
                Now = now
            };

            Assert.Equal(now, clock.Now);
            Assert.Equal(1505363696, clock.UnixTime);
        }
    }
}
