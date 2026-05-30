using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using QRCoder;

namespace Sailock.Helpers
{
    public static class QrCodeHelper
    {
        public static BitmapImage GenerateQrBitmap(string uri)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new BitmapByteQRCode(qrData);

            byte[] qrBytes = qrCode.GetGraphic(6);

            var bitmap = new BitmapImage();
            using var stream = new MemoryStream(qrBytes);
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
    }
}