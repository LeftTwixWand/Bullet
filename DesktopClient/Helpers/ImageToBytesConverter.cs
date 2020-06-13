using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace DesktopClient.Helpers
{
    public static class ImageToBytesConverter
    {
        public static async Task<byte[]> ToBytes(IRandomAccessStream stream)
        {
            var dr = new DataReader(stream.GetInputStreamAt(0));
            var bytes = new byte[stream.Size];
            await dr.LoadAsync((uint)stream.Size);
            dr.ReadBytes(bytes);
            return bytes;
        }

        public static IRandomAccessStream ToStream(byte[] array) => new MemoryStream(array).AsRandomAccessStream();

        public static async Task<BitmapImage> ToImage(byte[] buffer)
        {
            if (buffer != null)
            {
                BitmapImage image = new BitmapImage();
                await image.SetSourceAsync(ToStream(buffer));
                return image;
            }
            else
            {
                throw new Exception("Byte array is empty!");
            }
        }
    }
}