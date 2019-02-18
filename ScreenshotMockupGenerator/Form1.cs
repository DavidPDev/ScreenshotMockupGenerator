using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            string pars = textBox1.Text;
            Parameters par = ArgumentsParser.Parse(pars);
            Generator.Generate(par);

            pictureBox1.ImageLocation = par.outputFile;
            pictureBox2.ImageLocation = par.outputFile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
