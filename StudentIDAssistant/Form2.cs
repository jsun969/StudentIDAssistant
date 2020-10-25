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
using System.Threading;

namespace StudentIDAssistant
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string musicPath;

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
            if (textBox1.Text == "")
                button1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text)>=int.Parse(textBox2.Text))
            {
                MessageBox.Show("请正确输入范围","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                Thread.Sleep(100);
                StreamReader reader = File.OpenText("cache.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject["startNum"] = int.Parse(textBox1.Text);
                jsonObject["endNum"] = int.Parse(textBox2.Text);
                jsonObject["musicPath"] = musicPath;
                jsonObject["ifMusic"] = checkBox1.Checked;
                reader.Close();
                Thread.Sleep(100);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("cache.json", output);
                Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            StreamReader reader = File.OpenText("cache.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            textBox1.Text = jsonObject["startNum"].ToString();
            textBox2.Text = jsonObject["endNum"].ToString();
            musicPath=jsonObject["musicPath"].ToString();
            checkBox1.Checked= bool.Parse(jsonObject["ifMusic"].ToString());
            reader.Close();
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
                musicPath = dialog.FileName;
        }
    }
}
