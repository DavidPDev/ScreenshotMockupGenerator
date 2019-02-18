
namespace ScreenshotMockupGenerator
{
    public class Parameters
    {
        public enum OutputFormat
        {
            PNG24,       // .png 24 bits (no transparencies) [DEFAULT]
            PNG32,       // .png 32 bits
            JPG90,       // .jpg 90 quality
            JPG80,       // .jpg 80 quality
        }

        public enum DeviceStyle
        {
            DrawTop,    // Only show the top part of the device [DEFAULT]
            Stretch,    // Fullsize of device, screnshot MUST stretch exactly (may deform)
        }

        public enum AlignmentX
        {
            Left,   
            Center,
            Right,
        }
        public enum AlignmentY
        {
            Top,
            Center,
            Bottom,
        }

        public enum TextStyle
        {
            Simple,
            Shadow,
            Outline,
            Glow,
        }

        public class Position
        {
            public float posx;              // in 0..1 percentaje  (0.5f middle)
            public float posy;              // in 0..1 percentaje  (0.5f middle)
            public AlignmentX alix;          // Alignment position
            public AlignmentY aliy;          // Alignment position
            private void Defaults()
            {
                posx = 0.5f;
                posy = 0.5f;
                alix = AlignmentX.Center;
                aliy = AlignmentY.Center;
            }
            public Position () { Defaults(); }
            public Position (float px, float py) { Defaults(); posx = px; posy = py; }
        }

        public class Background
        {
            public string file;             // File
            public Position pos;            // Position
            private void Defaults()
            {
                file = "";
                pos = new Position();
            }
            public Background () { Defaults(); }
            public Background (string f) { Defaults();  file = f; }
        }

        public class Device
        {
            public string fileDevice;       // Device frame file
            public string fileScreenshot;   // Screenshot image
            public DeviceStyle style;       // Style (stretch or not)
            public Position pos;            // Position
            public float scale;             // General scale of device
            private void Defaults()
            {
                fileDevice = "";
                fileScreenshot = "";
                style = DeviceStyle.DrawTop;
                pos = new Position();
                scale = 1.0f;
            }
            public Device() { Defaults(); }
            public Device (string f1, string f2, DeviceStyle st, float sc)
            {
                Defaults();
                fileDevice = f1; fileScreenshot = f2; style = st; scale = sc;
            }
            public Device(string f1, string f2, DeviceStyle st, Position p, float sc)
            {
                Defaults();
                fileDevice = f1; fileScreenshot = f2; style = st; pos = p; scale = sc;
            }
        }

        public class Text
        {
            public string text;             // Text string
            public Position pos;            // Position
            public string color1;           // ARGB Main color
            public string color2;           // ARGB Secundary color (Shadow, glow)
            public TextStyle style;         // Predefined styles
            public float styleThickness;    // Thickness of the style (glow, outline, shadow):  1.0:default 2.0=2xBigger
            public string font;             // font name
            public float scale;             // scale factor  1.0:normal  2.0:2xBigger
            private void Defaults()
            {
                text = "";
                pos = new Position();
                color1 = "FF000000";
                color2 = "FFFFFFFF";
                style = TextStyle.Glow;
                styleThickness = 1.0f;
                font = "Corbel";
                scale = 1.0f;
            }
            public Text () { Defaults(); }
            public Text(string t) { Defaults(); text = t; }
            public Text(string t, Position p) { Defaults(); text = t; pos = p; }
        }

        public int width;                   // Resulting size X
        public int height;                  // Resulting size Y
        public string outputFile;           // File output
        public OutputFormat outputFormat;   // Format
        public Background background;       // Background
        public Device device;               // Device frame + screenshot
        public Text text;                   // Text

        private void Defaults()
        {
            width = 720;
            height = 1280;
            outputFile = "out\\test.png";
            outputFormat = OutputFormat.PNG24;
            background = new Background();
            device = new Device();
            text = new Text();
        }

        public bool ExistsBackground () { return background != null && background.file != null && background.file.Length > 0; }
        public bool ExistsDevice     () { return device != null && ((device.fileDevice != null && device.fileDevice.Length > 0) || (device.fileScreenshot != null && device.fileScreenshot.Length > 0)); }
        public bool ExistsText       () { return text != null && text.text != null && text.text.Length > 0; }

        public Parameters ()
        {
            Defaults();
        }
    }
}
