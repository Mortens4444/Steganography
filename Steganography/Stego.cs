using Steganography.Enum;
using Steganography.Extension;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Steganography
{
    public static class Stego
    {
        private const int ByteLength = 8;
        private const int BitmapFileHeaderLength = 14;

        public static byte[] HideSecretInImage(Image image, string secret, StegoKeySize stegoKeySize)
        {
            secret += "\0";
            var bytes = image.ToByteArray(ImageFormat.Bmp);

            int numberOfBytesToHideOneByte = (int)stegoKeySize;
            if (bytes.Length < secret.Length * numberOfBytesToHideOneByte + BitmapFileHeaderLength)
            {
                throw new InvalidOperationException("The image is too small for this secret to hide");
            }

            var mask = stegoKeySize.GetMaskValue();
            for (var i = 0; i < secret.Length; i++)
            {
                var secretCharacterInBinary = Convert.ToString(secret[i], 2).PadLeft(8, '0');
                var nextCharsIndex = bytes.Length - (i + 1) * ByteLength * numberOfBytesToHideOneByte;
                for (var j = 0; j < numberOfBytesToHideOneByte; j++)
                {
                    var nextCharIndex = nextCharsIndex + j * ByteLength;
                    var secretPart = secretCharacterInBinary.Substring(j * (ByteLength / numberOfBytesToHideOneByte), ByteLength / numberOfBytesToHideOneByte);
                    var number = Convert.ToByte(secretPart, 2);
                    bytes[nextCharIndex] = (byte)((bytes[nextCharIndex] & ~mask) | (number << (ByteLength - (ByteLength / numberOfBytesToHideOneByte))));
                }
            }
            return bytes;
        }

        public static string GetSecretFromImage(Image image, StegoKeySize stegoKeySize)
        {
            var bytes = image.ToByteArray(ImageFormat.Bmp);
            var secret = new StringBuilder();

            int numberOfBytesToHideOneByte = (int)stegoKeySize;
            int partsLength = ByteLength / numberOfBytesToHideOneByte;
            var mask = stegoKeySize.GetMaskValue();

            for (var i = 0; ; i++)
            {
                var secretCharInBinary = new StringBuilder();
                var baseIndex = bytes.Length - ((i + 1) * ByteLength * numberOfBytesToHideOneByte);

                for (var j = 0; j < numberOfBytesToHideOneByte; j++)
                {
                    var imageByteIndex = baseIndex + j * ByteLength;
                    var part = Convert.ToString((bytes[imageByteIndex] & mask) >> (ByteLength - partsLength), 2).PadLeft(partsLength, '0');

                    secretCharInBinary.Append(part);
                }

                var secretChar = (char)Convert.ToByte(secretCharInBinary.ToString(), 2);
                if (secretChar == '\0')
                {
                    break;
                }

                secret.Append(secretChar);
            }

            return secret.ToString();
        }
    }
}
