using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace Rage.Addservice.Helpers
{
    public static class BitmapImageExtension
    {
        public static void Save(this BitmapImage bitmapSource, Stream stream)
        {
            var writeableBitmap = new WriteableBitmap(bitmapSource);

            for (int i = 0; i < writeableBitmap.Pixels.Length; i++)
            {
                int pixel = writeableBitmap.Pixels[i];

                byte[] bytes = BitConverter.GetBytes(pixel);
                Array.Reverse(bytes);

                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static void Load(this BitmapImage bitmapSource, byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                bitmapSource.SetSource(stream);
            }
        }

        public static void Load(this BitmapImage bitmapSource, Stream stream)
        {
            bitmapSource.SetSource(stream);
        }

    }
}
