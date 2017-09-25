using System.Text;
using UOKOFramework.Security;
using Xunit;

namespace UOKOFramework.Test.Security.ASCIIHelpers
{
    public class GetStringTest : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var asciiHelper = new ASCIIHelper(new byte[] { 65, 66, 67 });

            var value = asciiHelper.GetString(Encoding.ASCII);

            Assert.Equal("ABC", value);
        }
    }
}