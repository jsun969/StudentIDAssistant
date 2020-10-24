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
using System.Runtime.Remoting.Messaging;
using WMPLib;

namespace StudentIDAssistant
{
    public partial class Form1 : Form
    {
        int startNum;
        int endNum;
        bool ifMusic;
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        private void goodStudent()
        {
            StreamReader reader = File.OpenText("cache.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            int goodStuCou = int.Parse(jsonObject["goodStuCnt"].ToString());
            for (int i = 0; i < goodStuCou; i++)
            {
                if (jsonObject["goodStu"][i].ToString() == label2.Text)
                {
                    reader.Close();
                    Random rand = new Random();
                    int RandNum = rand.Next(startNum, endNum);
                    label2.Text = RandNum.ToString();
                    goodStudent();
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader reader = File.OpenText("cache.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            startNum = int.Parse(jsonObject["startNum"].ToString());
            endNum = int.Parse(jsonObject["endNum"].ToString());
            string musicPath= jsonObject["musicPath"].ToString();
            ifMusic = bool.Parse(jsonObject["ifMusic"].ToString());
            reader.Close();

            timer1.Start();

            if (ifMusic==true)
            {
                wplayer.URL = musicPath;
                wplayer.controls.play();
            }

            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int RandNum = rand.Next(startNum, endNum);
            label2.Text = RandNum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ifMusic == true)
                wplayer.controls.pause();

            timer1.Stop();
            if(endNum-startNum>=3)
                goodStudent();
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if(button1.Enabled==true)
            {
                Form2 settings = new Form2();
                settings.ShowDialog();
            }
        }
    }
}
