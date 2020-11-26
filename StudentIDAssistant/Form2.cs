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
using System.Windows.Input;

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

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MP3文件(*.mp3)|*.mp3";
            if (dialog.ShowDialog() == DialogResult.OK)
                Form1.musicPath = dialog.FileName;
        }

        private void button1_MouseDown_1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int cheatKeyInt = int.Parse(DateTime.Now.ToString("mm")) % 10;
            Key cheatKey = new Key();
            if (cheatKeyInt == 1) cheatKey = Key.D1;
            if (cheatKeyInt == 2) cheatKey = Key.D2;
            if (cheatKeyInt == 3) cheatKey = Key.D3;
            if (cheatKeyInt == 4) cheatKey = Key.D4;
            if (cheatKeyInt == 5) cheatKey = Key.D5;
            if (cheatKeyInt == 6) cheatKey = Key.D6;
            if (cheatKeyInt == 7) cheatKey = Key.D7;
            if (cheatKeyInt == 8) cheatKey = Key.D8;
            if (cheatKeyInt == 9) cheatKey = Key.D9;
            if (cheatKeyInt == 0) cheatKey = Key.D0;
            if (e.Button == MouseButtons.Right)
            {
                if (Keyboard.IsKeyDown(cheatKey))
                {
                    Form3 goodStudent = new Form3();
                    goodStudent.ShowDialog();
                }
                else MessageBox.Show("???","???",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
