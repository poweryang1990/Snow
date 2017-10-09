using System.Text;
using Snow.Text;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Snow.Test.Text.ByteHelpers
{
    public class BaseTest
    {
        [Fact]
        public void DefaultEncoding_is_utf8()
        {
            Assert.Equal(Encoding.UTF8, ByteHelper.DefaultEncoding);
        }
    }
}