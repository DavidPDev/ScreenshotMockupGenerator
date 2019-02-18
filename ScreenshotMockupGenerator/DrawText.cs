using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ScreenshotMockupGenerator
{
    class DrawText
    {
        private static string CHARS_TO_MEASURE = "ABCDEFG abcdef ghijklmn .,";   // All must fit in a line. Used to calc font size

        public static void Draw(Graphics gra, Parameters parameters)
        {
            Parameters.Text text = parameters.text;
            string textStr = text.text.Replace("\\n", "\n");

            float fontSize = 100f;  // reference size (example 100)
            float sizeToFit = Math.Min(parameters.width, parameters.height);

            // Calc desired font size
            Font font = new Font(text.font, fontSize);
            SizeF stringSize = gra.MeasureString(CHARS_TO_MEASURE, font);
            fontSize *= (sizeToFit / stringSize.Width) * text.scale;
            fontSize = (float)(Math.Round(fontSize));

            // new font
            font = new Font(text.font, fontSize);
            stringSize = gra.MeasureString(textStr, font);

            Color col1 = Utils.ColorFromString(text.color1);
            Color col2 = Utils.ColorFromString(text.color2);

            float tx = ((float)parameters.width) * text.pos.posx;
            float ty = ((float)parameters.height) * text.pos.posy;
            SolidBrush brush1 = new SolidBrush(col1);
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            if      (text.pos.alix == Parameters.AlignmentX.Left)   format.Alignment = StringAlignment.Near;
            else if (text.pos.alix == Parameters.AlignmentX.Center) format.Alignment = StringAlignment.Center;
            else if (text.pos.alix == Parameters.AlignmentX.Right)  format.Alignment = StringAlignment.Far;
            if      (text.pos.aliy == Parameters.AlignmentY.Top)    format.LineAlignment = StringAlignment.Near;
            else if (text.pos.aliy == Parameters.AlignmentY.Center) format.LineAlignment = StringAlignment.Center;
            else if (text.pos.aliy == Parameters.AlignmentY.Bottom) format.LineAlignment = StringAlignment.Far;

            if (text.style==Parameters.TextStyle.Outline)
            {
                // https://stackoverflow.com/questions/4200843/outline-text-with-system-drawing
                GraphicsPath p = new GraphicsPath();
                p.AddString(textStr, font.FontFamily, (int)FontStyle.Regular, gra.DpiY * font.SizeInPoints / 72,
                    new Point((int)tx, (int)ty), format);
                float z = (gra.DpiY * fontSize / 72f) * 0.1f * text.styleThickness;
                Pen pen = new Pen (col2, z);
                gra.DrawPath(pen, p);
            }
            else if (text.style == Parameters.TextStyle.Glow)
            {
                GraphicsPath p = new GraphicsPath();
                p.AddString(textStr, font.FontFamily, (int)FontStyle.Regular, gra.DpiY * fontSize / 72,
                    new Point((int)tx, (int)ty), format);

                float z = (gra.DpiY * fontSize / 72f) * 0.025f * text.styleThickness;

                int STEPS1 = 12;
                int STEPS2 = 10;
                // step1 (no glow)
                for (int i=1; i< STEPS1; i++)
                {
                    Color col2i = Color.FromArgb(120, col2.R, col2.G, col2.B);
                    Pen pen = new Pen(col2i, z * (float)i);
                    pen.LineJoin = LineJoin.Round;
                    gra.DrawPath(pen, p);
                }
                float s2 = z * (float)STEPS1;
                // steps2 (glow)
                for (int i = 1; i < STEPS2; i++)
                {
                    Color col2i = Color.FromArgb(40, col2.R, col2.G, col2.B);
                    Pen pen = new Pen(col2i, s2 + (z/6f) * (float)(i));
                    pen.LineJoin = LineJoin.Round;
                    gra.DrawPath(pen, p);
                }
            }
            else if (text.style==Parameters.TextStyle.Shadow)
            {
                SolidBrush brush2 = new SolidBrush(col2);
                float z = (gra.DpiY * fontSize / 72f) * 0.075f * text.styleThickness;
                gra.DrawString(textStr, font, brush2, tx+z, ty+z, format);
            }
            gra.DrawString(textStr, font, brush1, tx, ty, format);
        }
    }
}
