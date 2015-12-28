using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Rage.Addservice.Persistence
{
    public class ImageToByteConverter
    {
        public static byte[] ConvertToByteArray(Bitmap image) 
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);

            return ms.ToArray();
        }
    }
}
