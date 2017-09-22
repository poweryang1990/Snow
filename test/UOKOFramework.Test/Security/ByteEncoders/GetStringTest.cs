using System.Text;
using UOKOFramework.Security;
using Xunit;

namespace UOKOFramework.Test.Security.ByteEncoders
{
    public class GetStringTest : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteEncoder = new ByteEncoder(new byte[] { 65, 66, 67 });

            var value = byteEncoder.GetString(Encoding.ASCII);

            Assert.Equal("ABC", value);
        }
    }
}