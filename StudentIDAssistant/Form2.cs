using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace StudentIDAssistant
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text,out int text1);
            int.TryParse(textBox2.Text,out int text2);
            if (textBox1.Text == "" || text1>=text2)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out int text1);
            int.TryParse(textBox2.Text, out int text2);
            if (textBox2.Text == "" || text1 >= text2)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.startNum = int.Parse(textBox1.Text);
            Form1.endNum = int.Parse(textBox2.Text);
            Form1.ifMusic = checkBox1.Checked;
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.startNum.ToString();
            textBox2.Text = Form1.endNum.ToString();
            checkBox1.Checked= Form1.ifMusic;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Form3 goodStudent = new Form3();
                goodStudent.ShowDialog();
            }
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MP3文件(*.mp3)|*.mp3";
            if (dialog.ShowDialog() == DialogResult.OK)
                Form1.musicPath = dialog.FileName;
        }
    }
}
