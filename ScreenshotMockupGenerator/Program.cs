using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/*
 * Version 1.0  18.FEB.2019 by JDP
 */

namespace ScreenshotMockupGenerator
{
    static class Program
    {
        public static string VERSION = "1.0";

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        [STAThread]
        static void Main()
        {
            String[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length <= 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                AttachConsole(ATTACH_PARENT_PROCESS);
                Console.WriteLine();
                Console.WriteLine(">Console run "+VERSION);
                Console.WriteLine(">GetCommandLineArgs: "+ Environment.CommandLine);
                for (int i=0; i<arguments.Length; i++)
                {
                    Console.WriteLine(" "+(i)+" : {"+arguments[i]+"}");
                }

                Console.WriteLine("Start...");
                Parameters par = ArgumentsParser.Parse(arguments, true);
                Generator.Generate(par);
                Console.WriteLine("Finished OK");
            }
        }
    }
}
