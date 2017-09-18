using System;
using Xunit;

namespace UOKOFramework.Test
{
    public class DateTimeProviderTest
    {
        [Fact]
        public void when_SetNow_and_SetUtcNow_should_return_set_time()
        {
            var now = DateTime.Parse("2017-09-14 12:34:56");
            var utcNow = DateTime.Parse("2017-09-15 12:34:56");

            IDateTimeProvider dateTimeProvider = new DateTimeProvider()
                .SetNow(now)
                .SetUtcNow(utcNow);

            Assert.Equal(now, dateTimeProvider.Now);
            Assert.Equal(utcNow, dateTimeProvider.UtcNow);
        }
    }
}
