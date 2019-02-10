using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AWSDriver
{
    /// <summary>
    /// 画像加工
    /// </summary>
    public class ImageEditor
    {
        private static readonly int ImageLength = 224;

        /// <summary>
        /// 画像をクリッピングする
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static MemoryStream SquareClipFromImageFile(string filePath)
        {
            using (var image = new Bitmap(filePath))
            {
                var lengthOfOneSide = Math.Min(image.Width, image.Height);

                using (var canvas = new Bitmap(ImageLength, ImageLength))
                using (var graphics = Graphics.FromImage(canvas))
                {
                    // 切取範囲
                    var positionX = image.Width == lengthOfOneSide ? 0 : (image.Width - lengthOfOneSide) / 2;
                    var positionY = image.Height == lengthOfOneSide ? 0 : (image.Height - lengthOfOneSide) / 2;
                    var srcRect = new Rectangle(positionX, positionY, lengthOfOneSide, lengthOfOneSide);
                    // 描画範囲
                    var desRect = new Rectangle(0, 0, ImageLength, ImageLength);
                    graphics.DrawImage(image, desRect, srcRect, GraphicsUnit.Pixel);

                    var ms = new MemoryStream();
                    canvas.Save(ms, ImageFormat.Jpeg);
                    ms.Seek(0, SeekOrigin.Begin);
#if DEBUG
                    try
                    {
                        using (var file = new FileStream("file.jpg", FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            var bytes = new byte[ms.Length];
                            ms.Read(bytes, 0, (int)ms.Length);
                            file.Write(bytes, 0, bytes.Length);
                        }
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    catch (IOException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                    }
#endif
                    return ms;
                }
            }
        }
    }
}
