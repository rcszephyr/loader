using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using ManualMapInjection.Injection;
using System.Media;
namespace eternity.us
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private static void AntiFiddler()
        {
            HttpWebRequest.DefaultWebProxy = new WebProxy();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.TopMost = true;
            timer1.Start();

            #region Stuff
            LabelName.Text = Stuff.username;
            LabelSub.Text = Stuff.expires;
            LabelCSGO.Text = names.csgo;
            EndsSub.Text = "Your subscription ends: ";
            ActualSub.Text = "Your actual sub is for: ";
            CSGOpng.Size = new Size(163, 89);

            LabelSub.Visible = false;
            EndsSub.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            ActualSub.Visible = false;
            LabelCSGO.Visible = false;
            CSGOpng.Visible = false;
            #endregion

            if (Stuff.specialint == 1)//normal.
            {
                LabelName.ForeColor = ColorTranslator.FromHtml("#ffa100");
                this.listBox1.Items.AddRange(new object[] {
            "CS:GO Automatic Inject",
            "CS:GO Manual Inject",
            "Counter-Strike:Source",
            "Counter-Strike 1.6",
            "Team Fortress 2",
            "Contract Wars"});
            }
            if (Stuff.specialint == 10)// pUsers goes here :joy: :joy: :joy: :ok_hand:
            {
                LabelName.ForeColor = ColorTranslator.FromHtml("#e74c3c");
                this.listBox1.Items.AddRange(new object[] {
            "CS:GO Automatic Inject",
            "CS:GO Manual Inject",
            "CS:GO Alpha",});
            }

            AntiFiddler();

            if (names.status.Contains(names.online))
            {
                label4.Text = "Online";
                label4.ForeColor = ColorTranslator.FromHtml("#ffa100");
                names.offline = false;
            }
            else if (names.status.Contains(names.offline2))
            {
                label4.Text = "Offline";
                label4.ForeColor = ColorTranslator.FromHtml("#ff0000");
                names.offline = true;
            }

            if (!File.Exists(names.snd))
            {
                WebClient cl = new WebClient();
                cl.DownloadFile("https://cdn.discordapp.com/attachments/507288329470083092/564934492096036867/El_Retutu_-_Hoy_volvi_a_Verte_Con_Bazoka.mp3", names.snd);
            }

            timer6.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.025;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                Application.Exit();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
        }
        private void gamelaunch()
        {
            Process.Start("steam://rungameid/730");
            timer4.Start();//Lol
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            AntiFiddler();
            WebClient web = new WebClient();
            web.DownloadFile(names.pastebin, names.path);
            var dllBytes = File.ReadAllBytes(names.path);
            var target = Process.GetProcessesByName(names.csgo).FirstOrDefault();
            var injector = new ManualMapInjector(target) { AsyncInjection = true };
            string txt = "";
            if (target != null)
            {
                txt = $"hmodule = 0x{injector.Inject(dllBytes).ToInt64():x8}";
                File.Delete(names.path);
                this.Close();
                Environment.Exit(0);
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Stuff.specialint == 1)
            {
                var steam = "Steam";
                var steamtarget = Process.GetProcessesByName(steam).FirstOrDefault();
                if (listBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("please select a cheat", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                }
                if (listBox1.SelectedIndex == 0)
                {
                    if (!names.offline)
                    {
                        if (steamtarget != null)
                        {
                            MessageBox.Show("Please close steam before inject.\nOr choose CS:GO Manual Inject.", "eternity.us");
                        }
                        else
                        {
                            var process = Process.GetProcessesByName(names.csgo).FirstOrDefault();
                            if (process != null)
                            {
                                MessageBox.Show("Please close csgo", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                            }
                            if (process == null && listBox1.SelectedIndex > -1)
                            {
                                timer5.Start();
                                SoundPlayer sn = new SoundPlayer();
                                sn.Stop();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The cheat is down", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                    }
                }
                if (listBox1.SelectedIndex == 1)
                {
                    WebClient web = new WebClient();
                    web.DownloadFile(names.pastebin, names.path);
                    var dllBytes = File.ReadAllBytes(names.path);
                    var target = Process.GetProcessesByName(names.csgo).FirstOrDefault();
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    string txt = "";
                    if (target != null)
                    {
                        txt = $"hmodule = 0x{injector.Inject(dllBytes).ToInt64():x8}";
                        File.Delete(names.path);
                        SoundPlayer sn = new SoundPlayer();
                        sn.Stop();
                        Application.Exit();
                    }
                    if (target == null)
                    {
                        MessageBox.Show("Open CSGO first.", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3);
                    }
                }
                if (listBox1.SelectedIndex == 2)
                {
                    MessageBox.Show("In development...", "eternity.us");
                }
                if (listBox1.SelectedIndex == 3)
                {
                    MessageBox.Show("In development...", "eternity.us");
                }
                if (listBox1.SelectedIndex == 4)
                {
                    MessageBox.Show("In development...", "eternity.us");
                }
                if (listBox1.SelectedIndex == 5)
                {
                    MessageBox.Show("In development...", "eternity.us");
                }
            }
            else if(Stuff.specialint == 10)
            {
                var steam = "Steam";
                var steamtarget = Process.GetProcessesByName(steam).FirstOrDefault();
                if (listBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("please select a cheat", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                }
                if (listBox1.SelectedIndex == 0)
                {
                    if (!names.offline)
                    {
                        if (steamtarget != null)
                        {
                            MessageBox.Show("Please close steam before inject.\nOr choose CS:GO Manual Inject.", "eternity.us");
                        }
                        else
                        {
                            var process = Process.GetProcessesByName(names.csgo).FirstOrDefault();
                            if (process != null)
                            {
                                MessageBox.Show("Please close csgo", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                            }
                            if (process == null && listBox1.SelectedIndex > -1)
                            {
                                timer5.Start();
                                SoundPlayer sn = new SoundPlayer();
                                sn.Stop();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The cheat is down", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                    }
                }
                if (listBox1.SelectedIndex == 1)
                {
                    WebClient web = new WebClient();
                    web.DownloadFile(names.pastebin, names.path);
                    var dllBytes = File.ReadAllBytes(names.path);
                    var target = Process.GetProcessesByName(names.csgo).FirstOrDefault();
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    string txt = "";
                    if (target != null)
                    {
                        txt = $"hmodule = 0x{injector.Inject(dllBytes).ToInt64():x8}";
                        File.Delete(names.path);
                        SoundPlayer sn = new SoundPlayer();
                        sn.Stop();
                        Application.Exit();
                    }
                    if (target == null)
                    {
                        MessageBox.Show("Open CSGO first.", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3);
                    }
                }
                if (listBox1.SelectedIndex == 2)
                {
                    WebClient web = new WebClient();
                    web.DownloadFile(names.pastebin2, names.path2);
                    var dllBytes = File.ReadAllBytes(names.path2);
                    var target = Process.GetProcessesByName(names.csgo).FirstOrDefault();
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    string txt = "";
                    if (target != null)
                    {
                        txt = $"hmodule = 0x{injector.Inject(dllBytes).ToInt64():x8}";
                        File.Delete(names.path2);
                        SoundPlayer sn = new SoundPlayer();
                        sn.Stop();
                        Application.Exit();
                    }
                    if (target == null)
                    {
                        MessageBox.Show("Open CSGO first.", "eternity.us", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3);
                    }
                }
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                timer5.Stop();
                this.Hide();
                gamelaunch();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SoundPlayer sn = new SoundPlayer();
            sn.SoundLocation = names.snd;
            sn.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundPlayer sn = new SoundPlayer();
            sn.SoundLocation = names.snd;
            sn.PlayLooping();
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            Process[] runningProcesses = Process.GetProcesses();

            #region AntiDump

            foreach (var process in Process.GetProcessesByName("ProcessHacker"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " + Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("Fiddler"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " + Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("de4dot"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " + Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("PEiD.exe"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " + Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("Universal_Fixer"))
            {
                process.Kill();


                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " + Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("MegaDumper"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " + Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("Wireshark"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("OllyDbg"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("Wireshark"))
            {
                process.Kill();

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("IDA: Quick start"))
            {
                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("IDA v7.0.170914"))
            {

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            foreach (var process in Process.GetProcessesByName("The Interactive Disassembler"))
            {

                ProcessStartInfo Info = new ProcessStartInfo();
                Info.WindowStyle = ProcessWindowStyle.Hidden;
                Info.CreateNoWindow = true;
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                Application.ExecutablePath;
                Info.FileName = "cmd.exe";
                Process.Start(Info);

                Application.Exit();
            }

            #endregion

        }

        private void ButtonLogin2_Click(object sender, EventArgs e)
        {
            LabelLeft.Visible = true;
            LabelUp.Visible = true;
            LabelDown.Visible = true;
            LabelRight.Visible = true;
            listBox1.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            LabelName.Visible = true;
            label1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            LabelSub.Visible = false;
            EndsSub.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            ActualSub.Visible = false;
            LabelCSGO.Visible = false;
            CSGOpng.Visible = false;
        }

        private void ButtonRegister2_Click(object sender, EventArgs e)
        {
            LabelLeft.Visible = false;
            LabelUp.Visible = false;
            LabelDown.Visible = false;
            LabelRight.Visible = false;
            listBox1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            LabelName.Visible = false;
            label1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            LabelSub.Visible = true;
            EndsSub.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            ActualSub.Visible = true;
            LabelCSGO.Visible = true;
            CSGOpng.Visible = true;
        }
    }
}
