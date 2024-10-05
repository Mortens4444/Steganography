using Steganography.Attribute;
using Steganography.Enum;

namespace Steganography.Extension
{
    public static class EnumExtensions
    {
        public static int GetMaskValue(this StegoKeySize keySize)
        {
            var type = keySize.GetType();
            var memInfo = type.GetMember(keySize.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(MaskValueAttribute), false);
            return attributes.Length > 0 ? ((MaskValueAttribute)attributes[0]).MaskValue : 0;
        }
    }
}
