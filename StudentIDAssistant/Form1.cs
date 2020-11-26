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
        public static int startNum;
        public static int endNum;
        public static int[] goodStuID = new int[1005];
        public static int goodStuCnt;
        public static bool ifMusic;
        public static string musicPath;
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        private void goodStudent()
        {
            for (int i = 0; i < goodStuCnt; i++)
            {
                if(goodStuID[i].ToString()==label2.Text)
                {
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
            if(endNum-startNum>=10)
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
                if (ifMusic)
                    label3.Text = "音乐 : " + Path.GetFileNameWithoutExtension(musicPath);
                else
                    label3.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("cache.json") == false)
                File.WriteAllText("cache.json", "{\n  \"startNum\": 1,\n  \"endNum\": 30,\n  \"goodStuCnt\": 0,\n  \"goodStu\": [],\n  \"musicPath\": null,\n  \"ifMusic\": false\n}");
            StreamReader reader = File.OpenText("cache.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            startNum = int.Parse(jsonObject["startNum"].ToString());
            endNum = int.Parse(jsonObject["endNum"].ToString());
            musicPath = jsonObject["musicPath"].ToString();
            ifMusic = bool.Parse(jsonObject["ifMusic"].ToString());
            goodStuCnt = int.Parse(jsonObject["goodStuCnt"].ToString());
            for (int i = 0; i < goodStuCnt; i++)
                goodStuID[i] = int.Parse(jsonObject["goodStu"][i].ToString());
            if (ifMusic)
                label3.Text = "音乐 : " + Path.GetFileNameWithoutExtension(musicPath);
            else
                label3.Text = "";
            reader.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamReader reader = File.OpenText("cache.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            JArray jsonArrayObj = new JArray();
            jsonObject["startNum"] = startNum;
            jsonObject["endNum"] = endNum;
            jsonObject["goodStuCnt"] = goodStuCnt;
            for(int i=0;i<goodStuCnt;i++)
                jsonArrayObj.Add(goodStuID[i]);
            jsonObject["goodStu"] = jsonArrayObj;
            jsonObject["musicPath"] = musicPath;
            jsonObject["ifMusic"] = ifMusic;
            reader.Close();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("cache.json", output);
        }
    }
}
