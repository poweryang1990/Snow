using Xunit;

namespace UOKOFramework.Test.Security.ByteEncoders
{
    public class GetHexTest : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteEncoder = BuildByteEncoder("优客");

            var hex = byteEncoder.GetHex();

            Assert.Equal("E4-BC-98-E5-AE-A2", hex);
        }

        [Fact]
        public void when_withHyphen_is_true()
        {
            var byteEncoder = BuildByteEncoder("优客");

            var hex = byteEncoder.GetHex(withHyphen: true);

            Assert.Equal("E4-BC-98-E5-AE-A2", hex);
        }

        [Fact]
        public void when_withHyphen_is_false()
        {
            var byteEncoder = BuildByteEncoder("优客");

            var hex = byteEncoder.GetHex(withHyphen: false);

            Assert.Equal("E4BC98E5AEA2", hex);
        }

        [Fact]
        public void when_lowerCase_is_true()
        {
            var byteEncoder = BuildByteEncoder("优客");

            var hex = byteEncoder.GetHex(withHyphen: true, lowerCase: true);

            Assert.Equal("e4-bc-98-e5-ae-a2", hex);
        }
    }
}