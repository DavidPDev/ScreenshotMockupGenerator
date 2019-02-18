using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ScreenshotMockupGenerator
{
    class ArgumentsParser
    {

        public static Parameters Parse (string str)
        {
            str = str.Replace('\r', ' ');
            str = str.Replace('\n', ' ');
            // https://stackoverflow.com/questions/4780728/regex-split-string-preserving-quotes/4780801#4780801
            string[] sl= Regex.Split(str, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            return Parse(sl, false);
        }

        public static Parameters Parse(string[] sl, bool excludeFirstArgument)
        {
            Parameters par = new Parameters();

            for (int i=0; i<sl.Length; i++)
            {
                string str0 = sl[i];
                if (str0.Length == 0 || (i==0 && excludeFirstArgument) ) continue;
                string str= str0.Replace("\"", "");

                int p = str.IndexOf('=');
                if (p == -1) throw new Exception("Invalid parameter. Not found '=' : \"" + str + "\"");

                string var = str.Substring(0, p);
                string val = str.Substring(p+1);

                switch (var.ToUpper())
                {
                    case "W"  : par.width = ToInt(val); break;
                    case "H"  : par.height = ToInt(val); break;
                    case "O"  : par.outputFile = val; break;
                    case "OF" : par.outputFormat = ToOutputFormat(val); break;

                    case "BF" : par.background.file = val; break;
                    case "BP" : par.background.pos = ToPosition(val); break;

                    case "DFD": par.device.fileDevice = val; break;
                    case "DFS": par.device.fileScreenshot = val; break;
                    case "DS" : par.device.style = ToDeviceStyle(val); break;
                    case "DP" : par.device.pos = ToPosition(val); break;
                    case "DC" : par.device.scale = ToFloat(val); break;

                    case "TT" : par.text.text = val; break;
                    case "TP" : par.text.pos = ToPosition(val); break;
                    case "TC1": par.text.color1 = val; break;
                    case "TC2": par.text.color2 = val; break;
                    case "TS" : par.text.style = ToTextStyle(val); break;
                    case "TST": par.text.styleThickness = ToFloat(val); break;
                    case "TF" : par.text.font = val; break;
                    case "TC" : par.text.scale = ToFloat(val); break;
                }
            }
            return par;
        }

        private static int ToInt(string s)
        {
            return int.Parse(s);
        }
        private static float ToFloat(string s)
        {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }

        private static Parameters.OutputFormat ToOutputFormat(string s)
        {
            return (Parameters.OutputFormat)Enum.Parse(typeof(Parameters.OutputFormat), s, true);
        }

        public static Parameters.DeviceStyle ToDeviceStyle(string s)
        {
            return (Parameters.DeviceStyle)Enum.Parse(typeof(Parameters.DeviceStyle), s, true);
        }

        public static Parameters.TextStyle ToTextStyle(string s)
        {
            return (Parameters.TextStyle)Enum.Parse(typeof(Parameters.TextStyle), s, true);
        }

        private static Parameters.Position ToPosition(string s)
        {
            Parameters.Position pos = new Parameters.Position();
            string[] sl = s.Split(',');
            if (sl.Length>=2)
            {
                pos.posx = ToFloat(sl[0]);
                pos.posy = ToFloat(sl[1]);
            }
            if (sl.Length>=4)
            {
                pos.alix = (Parameters.AlignmentX)Enum.Parse(typeof(Parameters.AlignmentX), sl[2], true);
                pos.aliy = (Parameters.AlignmentY)Enum.Parse(typeof(Parameters.AlignmentY), sl[3], true);
            }
            return pos;
        }
    }
}
