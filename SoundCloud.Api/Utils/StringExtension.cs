using System.Text;

namespace SoundCloud.Api.Utils
{
    internal static class StringExtension
    {
        internal static void AppendLineIfNotEmpty(this StringBuilder builder, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            builder.AppendLine(str);
        }

        internal static byte[] GetBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}