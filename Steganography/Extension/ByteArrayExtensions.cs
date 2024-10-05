using System.Drawing;
using System.IO;

namespace Steganography.Extension
{
    public static class ByteArrayExtensions
    {
        public static Image ByteArrayToImage(this byte[] imageBytes)
        {
            return imageBytes == null ? null : Image.FromStream(new MemoryStream(imageBytes));
        }
    }
}
