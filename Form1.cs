using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace RDP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void timer_Tick(object sender, System.EventArgs e)
        {
            button1.Enabled = true;
            timer.Stop();
        }

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private void button1_Click(object sender, EventArgs e)
        {

            timer.Interval = 5000;
            timer.Tick += timer_Tick;
            timer.Start();
            button1.Enabled = false;

            List<string> list = new List<string>() { "192.168.0.202", "192.168.0.176" };
            int l = list.Count;
            Random r = new Random();
            int num = r.Next(l);
            var randomStringFromList = list[num];

            var rdcProcess = new Process
            {
                StartInfo =
                {
                    FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe"),
                    Arguments = String.Format(@"/generic:TERMSRV/{0} /user:{1} /pass:{2}", randomStringFromList,
                                (String.IsNullOrEmpty("E-Wise")) ? inputUsername.Text : "E-Wise" + "\\" + inputUsername.Text, inputPassword.Text),
                                WindowStyle = ProcessWindowStyle.Hidden
                }
            };
            rdcProcess.Start();
            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
            rdcProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            rdcProcess.StartInfo.Arguments = String.Format("/f /v {0}", randomStringFromList);
            rdcProcess.Start();
        }
    }
}