using Steganography.Enum;
using Steganography.Extension;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Steganography
{
    internal static class Program
    {
        static void Main()
        {
            var inputImageFilePath = Path.Combine(Application.StartupPath, "Family.png");
            var secretTextFilePath = Path.Combine(Application.StartupPath, "SecretMessage.txt");
            var outputImageFilePath = Path.Combine(Application.StartupPath, "ImageWithSecret.bmp");

            var image = Image.FromFile(inputImageFilePath);
            var secret = File.ReadAllText(secretTextFilePath);
            var stegoKeySize = StegoKeySize.Two;

            var outputImage = image.SaveImageWithSecret(secret, outputImageFilePath, stegoKeySize);
            var foundSecret = outputImage.GetSecretFromImage(stegoKeySize);

            Console.WriteLine(foundSecret);
        }
    }
}
