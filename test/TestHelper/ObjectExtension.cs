using Snow.Extensions;

namespace TestHelper
{
    public static class ObjectExtension
    {
        public static byte[] GetAssemblyResourceBytes(this object @this, string fileName)
        {
            return @this
                   .GetAssembly()
                   .GetResourceBytes(fileName);
        }

        public static string GetBelongAssemblyResourceText(this object @this, string fileName)
        {
            return @this.GetAssemblyResourceBytes(fileName).GetString();
        }
    }
}
