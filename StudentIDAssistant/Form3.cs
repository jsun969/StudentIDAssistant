using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;

namespace StudentIDAssistant
{
    public partial class Form3 : Form
    {
        public Form3()
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

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            StreamReader reader = File.OpenText("cache.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            int Cnt = 0;
            JArray jsonArrayObj = new JArray();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                bool ifRe = false;
                for (int j = 0; j < i; j++)
                {
                    if(jsonObject["goodStu"][j].ToString() == listBox1.Items[i].ToString())
                    {
                        Cnt++;
                        ifRe = true;
                        break;
                    }
                }
                if(ifRe==false)
                    jsonArrayObj.Add(int.Parse(listBox1.Items[i].ToString()));
            }
            jsonObject["goodStu"] = jsonArrayObj;
            jsonObject["goodStuCnt"] = listBox1.Items.Count-Cnt;
            reader.Close();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("cache.json", output);
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            StreamReader reader = File.OpenText("cache.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            int itemsCnt = int.Parse(jsonObject["goodStuCnt"].ToString());
            for (int i = 0; i < itemsCnt; i++)
            {
                listBox1.Items.Add(jsonObject["goodStu"][i].ToString());
            }
            reader.Close();
        }
    }
}
