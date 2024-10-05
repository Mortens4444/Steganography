using Steganography.Enum;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Steganography.Extension
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            if (image == null)
            {
                return null;
            }
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, format);
            return memoryStream.ToArray();
        }

        public static Image SaveImageWithSecret(this Image image, string secret, string filename, StegoKeySize stegoKeySize)
        {
            var bytes = Stego.HideSecretInImage(image, secret, stegoKeySize);
            File.WriteAllBytes(filename, bytes);
            return bytes.ByteArrayToImage();
        }

        public static string GetSecretFromImage(this Image image, StegoKeySize stegoKeySize)
        {
            return Stego.GetSecretFromImage(image, stegoKeySize);
        }
    }
}
