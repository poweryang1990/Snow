using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.EnumerableExtension
{
    public class WhereIfTest
    {
        [Fact]
        public void when_condition_is_false_where_should_not_take_effect()
        {
            var numbers = new[] { 1, 2, 3 };

            var result = numbers.WhereIf(false, item => item > 2);

            Assert.Equal(new[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void when_condition_is_true_where_should_take_effect()
        {
            var numbers = new[] { 1, 2, 3 };

            var result = numbers.WhereIf(true, item => item > 2);

            Assert.Equal(new[] { 3 }, result);
        }
    }
}
