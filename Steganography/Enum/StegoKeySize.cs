using Steganography.Attribute;

namespace Steganography.Enum
{
    public enum StegoKeySize
    {
        [MaskValue(0b1111_0000)]
        Two = 2,

        [MaskValue(0b1111_1100)]
        Four = 4,

        [MaskValue(0b1111_1110)]
        Eight = 8
    }
}
