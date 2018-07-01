using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Cloud.Vision.V1;
using System.Windows.Forms;

namespace FormVisionAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = "";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;

            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text) == true)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(textBox1.Text);

                var client = ImageAnnotatorClient.Create();
                var image = Google.Cloud.Vision.V1.Image.FromFile(textBox1.Text);
                var response = client.DetectLabels(image);

                foreach (var label in response.Select((v, i) => new { v, i }))
                {
                    richTextBox1.Text += (label.i + 1).ToString() + ". " + label.v.Description + "\n";
                }
            }
        }
    }
}