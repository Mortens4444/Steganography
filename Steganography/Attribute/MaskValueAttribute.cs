namespace Steganography.Attribute
{
    public class MaskValueAttribute : System.Attribute
    {
        public int MaskValue { get; set; }

        public MaskValueAttribute(int maskValue)
        {
            MaskValue = maskValue;
        }
    }
}
