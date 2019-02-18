using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ScreenshotMockupGenerator
{
    class Utils
    {

        // Create a Graphics (Canvas) with highest quality
        public static Graphics CreateGraphics(Bitmap bmp)
        {
            Graphics gra = Graphics.FromImage((Image)bmp);
            //set the resize quality modes to high quality
            gra.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gra.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gra.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            gra.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gra.TextRenderingHint = TextRenderingHint.AntiAlias;
            //gra.CompositingMode = CompositingMode.SourceCopy;

            return gra;
        }

        // Convert a Bitmap 32bpp to a new Bitmap 24 bpp
        public static Bitmap ConvertTo24bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        // Helper to save jpg files
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        // Find limits of a frame -> Transparent rectangle in the center (middle of image)
        public static Rectangle FindFrameLimits(Bitmap b)
        {
            int[] DX = { -1, 1, 0, 0 };
            int[] DY = { 0, 0, -1, 1 };

            if (b != null)
            {
                int cx = b.Width / 2;
                int cy = b.Height / 2;
                int x1 = cx, x2 = cx, y1 = cy, y2 = cy; // Limit points
                for (int i = 0; i < 4; i++)
                {
                    int x = cx, y = cy;
                    Color col;
                    do
                    {
                        if (x < 0 || x >= b.Width || y < 0 || y >= b.Height) break;
                        col = b.GetPixel(x, y);
                        x += DX[i];
                        y += DY[i];

                    } while (col.A <= 10);       // Almost trasparent

                    x1 = Math.Min(x1, x);
                    x2 = Math.Max(x2, x);
                    y1 = Math.Min(y1, y);
                    y2 = Math.Max(y2, y);
                }
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return Rectangle.Empty;
        }

        // Return calculated rect to draw
        public static Rectangle RectangleCalc(int w, int h, Bitmap b, Parameters.Position pos, float scale0)
        {

            float bw = b.Width;
            float bh = b.Height;
            float scale1 = ((float)w) / bw;
            float scale2 = ((float)h) / bh;
            float scale = Math.Min(scale1, scale2) * scale0;
            bw *= scale;
            bh *= scale;

            float cx = w / 2f;
            float cy = h / 2f;

            float x = cx - bw * pos.posx;
            float y = cy - bh * pos.posy;

            return new Rectangle((int)x, (int)y, (int)bw, (int)bh);
        }

        // Parse an argb or rgb string in hex to a Color object
        public static Color ColorFromString(string argb)   // argb o rgb
        {
            if (argb == null) return Color.Black;
            int a = 255, r = 0, g = 0, b = 0;
            int p = 0;
            if (argb.Length == 8)
            {
                a = int.Parse(argb.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                p = 2;
            }
            if (argb.Length == 6 || argb.Length == 8)
            { 
                r = int.Parse(argb.Substring(p+0, 2), System.Globalization.NumberStyles.HexNumber);
                g = int.Parse(argb.Substring(p+2, 2), System.Globalization.NumberStyles.HexNumber);
                b = int.Parse(argb.Substring(p+4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return Color.FromArgb(a, r, g, b);
        }
    }
}
