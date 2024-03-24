using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScreenshotMockupGenerator.Properties;

namespace ScreenshotMockupGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadSettings ()
        {
            textBox1.Text = Settings.Default.myparams;
        }
        private void SaveSettings()
        {
            Settings.Default.myparams = textBox1.Text;
            Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();

            if ((ModifierKeys & Keys.Shift) == 0)
            {
                string initLocation = Properties.Settings.Default.InitialLocation;
                Point il = new Point(0, 0);
                Size sz = Size;
                if (!string.IsNullOrWhiteSpace(initLocation))
                {
                    string[] parts = initLocation.Split(',');
                    if (parts.Length >= 2)
                    {
                        il = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
                    }
                    if (parts.Length >= 4)
                    {
                        sz = new Size(int.Parse(parts[2]), int.Parse(parts[3]));
                    }
                }
                Size = sz;
                Location = il;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == 0)
            {
                Point location = Location;
                Size size = Size;
                if (WindowState != FormWindowState.Normal)
                {
                    location = RestoreBounds.Location;
                    size = RestoreBounds.Size;
                }
                string initLocation = string.Join(",", location.X, location.Y, size.Width, size.Height);
                Properties.Settings.Default.InitialLocation = initLocation;
                Properties.Settings.Default.Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            string pars = textBox1.Text;
            Parameters par = ArgumentsParser.Parse(pars);
            Generator.Generate(par);

            pictureBox1.ImageLocation = par.outputFile;
            //pictureBox2.ImageLocation = par.outputFile;
            string exactPath = Path.GetFullPath(par.outputFile);
            String url = "file:///"+exactPath;
            webBrowser1.Url = new System.Uri(url, System.UriKind.Absolute); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
