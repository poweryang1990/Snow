using UOKOFramework.Extensions;

namespace UOKOFramework.TestHelper
{
    public static class ObjectExtension
    {
        public static byte[] GetResourceBytes(this object @this, string fileName)
        {
            return @this
                 .GetType()
                 .Assembly
                 .GetResourceBytes(_ => _.EndsWith(fileName));
        }

        public static string GetResourceText(this object @this, string fileName)
        {
            return @this.GetResourceBytes(fileName).GetString();
        }
    }
}
