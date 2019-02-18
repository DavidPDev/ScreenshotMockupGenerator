using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ScreenshotMockupGenerator
{
    class Generator
    {
        public static void Generate(Parameters parameters)
        {
            Bitmap bmp = new Bitmap(parameters.width, parameters.height);
            Graphics gra = Utils.CreateGraphics(bmp);

            // Draw with red (to show glitches if present)
            SolidBrush brushBack = new SolidBrush(Color.Red);
            Rectangle allRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            gra.FillRectangle(brushBack, allRect);

            if (parameters.ExistsBackground ()) DrawBackground(gra, parameters);
            if (parameters.ExistsDevice())      DrawDevice(gra, parameters);
            if (parameters.ExistsText())        DrawText.Draw(gra, parameters);

            Save(bmp, parameters.outputFile, parameters.outputFormat);
        }

        private static void DrawBackground (Graphics gra, Parameters parameters)
        {
            Parameters.Background background = parameters.background;
            Bitmap b = new Bitmap(background.file);

            float bw = b.Width;
            float bh = b.Height;
            float scale1 = ((float)parameters.width) / bw;
            float scale2 = ((float)parameters.height) / bh;
            float scale = Math.Max(scale1, scale2) * 1.01f;
            bw *= scale;
            bh *= scale;

            float cx = parameters.width / 2f;
            float cy = parameters.height / 2f;

            float x = cx - bw * background.pos.posx;
            float y = cy - bh * background.pos.posy;

            gra.DrawImage(b, (int)x, (int)y, (int)bw, (int)bh);
        }

        private static void DrawDevice(Graphics gra, Parameters parameters)
        {
            Parameters.Device device = parameters.device;
            Bitmap b1 = new Bitmap (device.fileDevice);

            Rectangle r = Utils.FindFrameLimits(b1);
            if (r == Rectangle.Empty) throw new Exception("Device frame error. Can't find limits.");

            Graphics gra2 = Utils.CreateGraphics(b1);

            if (device.fileScreenshot != null && device.fileScreenshot.Length > 0)
            {
                Bitmap b2 = new Bitmap(device.fileScreenshot);

                if (device.style == Parameters.DeviceStyle.DrawTop)
                {
                    float factorxy = (float)b2.Height / (float)b2.Width;
                    gra2.DrawImage(b2, r.X, r.Y, r.Width, r.Width * factorxy);
                }
                else if (device.style == Parameters.DeviceStyle.Stretch)
                {
                    gra2.DrawImage(b2, r.X, r.Y, r.Width, r.Height);
                }
            }

            Rectangle r2 = Utils.RectangleCalc(parameters.width, parameters.height, b1, device.pos, device.scale);
            if (r2 == Rectangle.Empty) throw new Exception ("Device error. Can't draw.");

            gra.DrawImage(b1, r2.X, r2.Y, r2.Width, r2.Height);

        }

        private static void Save(Bitmap bmp, string filename, Parameters.OutputFormat format)
        {
            // If output format is png but filename includes .jpg change it
            if (filename.ToUpper().IndexOf(".JPG") > 0 &&
                (format == Parameters.OutputFormat.PNG32 || 
                 format == Parameters.OutputFormat.PNG24) )
            {
                format = Parameters.OutputFormat.JPG80;
            }

            if (format == Parameters.OutputFormat.PNG32)        // 32 bpp
            {
                bmp.Save(filename, ImageFormat.Png);
            }
            else if (format == Parameters.OutputFormat.PNG24) { // 24 bpp
                Bitmap b2 = Utils.ConvertTo24bpp(bmp);
                b2.Save(filename, ImageFormat.Png);
            }
            else // jpg
            {
                long quality = 75L;
                if      (format == Parameters.OutputFormat.JPG90) quality = 90L;
                else if (format == Parameters.OutputFormat.JPG80) quality = 80L;
                ImageCodecInfo jpgEncoder = Utils.GetEncoder(ImageFormat.Jpeg);
                // Create an Encoder object based on the GUID
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp.Save(filename, jpgEncoder, myEncoderParameters);
            }
        }

    }
}
