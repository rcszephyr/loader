using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Net;
using ManualMapInjection.Injection;
using System.Diagnostics;
using Authed;
using Newtonsoft.Json;
using Jose.jwe;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;

namespace eternity.us
{
    public partial class Login : Form
    {

        #region Ignore this shit
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Win32.ReleaseCapture();
                Win32.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public static class Win32
        {
            public const int CS_DropSHADOW = 0x20000;
            public const int GCL_STYLE = -26;
            public const int WM_NCLBUTTONDOWN = 0xA1;
            public const int HT_CAPTION = 0x2;

            [DllImport("User32.dll")]
            public static extern short GetAsyncKeyState(Keys vKey);
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int GetClassLong(IntPtr hwnd, int nIndex);
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
            [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,     // x-coordinate of upper-left corner
                int nTopRect,      // y-coordinate of upper-left corner
                int nRightRect,    // x-coordinate of lower-right corner
                int nBottomRect,   // y-coordinate of lower-right corner
                int nWidthEllipse, // height of ellipse
                int nHeightEllipse // width of ellipse
            );

            public static bool GetKeyState(Keys key) => (GetAsyncKeyState(key) == short.MinValue + 1);
        }
        #endregion

        Auth auth = new Auth();
        Checker checker = new Checker();

        public Login()
        {
            #region AuthedStuff
            bool isLegit = checker.CheckFiles();
            if (!isLegit)
            {
                MessageBox.Show("You don't have permission to access the tool due wrong/modified files!", "eternity.us");
                Application.Exit();
            }
            bool authed = auth.Authenticate(Stuff.key);
            if (authed != true)
            {
                MessageBox.Show("Please contact the Administration", "eternity.us");
                Environment.Exit(0);
            }
            if (File.Exists(names.path))
            {
                File.Delete(names.path);
            }
            if (File.Exists(names.path2))
            {
                File.Delete(names.path2);
            }

            string str = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
            ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + str + ":\"");
            managementObject.Get();
            string text = managementObject["VolumeSerialNumber"].ToString();
            user_hwid = Crypt(text);
            #endregion

            InitializeComponent();
            this.Opacity = 0;
            this.TopMost = true;
            timer1.Start();
            LabelInvite.Visible = false;
            TextInvite.Visible = false;
            ButtonRegister.Visible = false;

            TextUser.Text = eternity.us.Properties.Settings.Default.username;
            TextPassword.Text = eternity.us.Properties.Settings.Default.password;
            checkBox1.Checked = eternity.us.Properties.Settings.Default.check;

            timer3.Start();
        }

        private void ButtonLogin2_Click(object sender, EventArgs e)
        {
            LabelInvite.Visible = false;
            TextInvite.Visible = false;
            ButtonRegister.Visible = false;
            ButtonLogin.Visible = true;
        }

        private string Crypt(string text)
        {
            string text2 = string.Empty;
            foreach (char c in text)
            {
                text2 += ((int)(c ^ '\u0001')).ToString();
            }
            return text2;
        }

        private static string user_hwid;

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            bool authed = auth.Authenticate(Stuff.key);
            string username1 = TextUser.Text;
            string password = TextPassword.Text + user_hwid; //2000iq hwid lock for authed hh
            string email = TextPassword.Text;
            string token = TextInvite.Text;
            bool register = auth.Register(username1, password, email, token);

            if (authed != true)
            {
                MessageBox.Show("Error. Please restart the Application", "eternity.us");

            }

            if (register == true)
            {
                MessageBox.Show(username1 + " successfully registered!", "eternity.us");
                eternity.us.Properties.Settings.Default.username = TextUser.Text;
                eternity.us.Properties.Settings.Default.password = TextPassword.Text;
                eternity.us.Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Invalid or used invitation code.", "eternity.us");
                return;
            }

        } 

        private void ButtonRegister2_Click(object sender, EventArgs e)
        {
            LabelInvite.Visible = true;
            TextInvite.Visible = true;
            ButtonRegister.Visible = true;
            ButtonLogin2.Visible = true;
            ButtonLogin.Visible = false;
        }

        private void Auth_OnInvalidUser(object sender, OnUserInvalidEvent e)
        {
            throw new NotImplementedException();
        }

        private void Auth_OnBannedUser(object sender, OnUserBannedEvent e)
        {
            throw new NotImplementedException();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            Stuff.username = TextUser.Text;
            bool right = false;

            string password = TextPassword.Text + user_hwid; //pHwid
            if (!(this.Opacity == 1.0))//dont delete this shit, loader will broke
            {
                MessageBox.Show("Wait until the loader loads completely.", "eternity.us");
            }
            else
            {
                if (TextUser.Text == "" || TextPassword.Text == "")
                {
                    MessageBox.Show("Username or Password empty!", "eternity.us");
                }
                else
                {
                    bool login = auth.Login(Stuff.username, password);
                    if (login == true)
                    {

                            Stuff.user_id = auth.user.id;//useless cuz authed is bugged as fuck :)
                            Stuff.specialint = auth.user.special;
                            Stuff.programid = auth.user.program_id;//useless cuz authed is bugged as fuck :)
                            Stuff.expires = auth.user.expires;
                            eternity.us.Properties.Settings.Default.username = TextUser.Text;
                            eternity.us.Properties.Settings.Default.password = TextPassword.Text;
                            eternity.us.Properties.Settings.Default.Save();
                            timer2.Start();
                    }
                    else
                    {
                        MessageBox.Show("Wrong username/password or already registered!");
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.050;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.050;
            }
            else
            {
                timer2.Stop();
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
        }

        private void TextUser_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (key == Keys.Space)
            {
                MessageBox.Show("We dont accept spaces in the username.", "eternity.us");
                TextUser.Clear();
                e.Handled = true;
            }

            if (key == Keys.Enter)
            {
                MessageBox.Show("If you want to sumbit just click in the button, dont press enter.", "eternity.us");
                TextUser.Clear();
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        private void TextPassword_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (key == Keys.Space)
            {
                MessageBox.Show("We dont accept spaces in the password.", "eternity.us");
                TextPassword.Clear();
                e.Handled = true;
            }
            if (key == Keys.Enter)
            {
                MessageBox.Show("If you want to sumbit just click in the button, dont press enter.", "eternity.us");
                TextPassword.Clear();
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        private void TextInvite_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (key == Keys.Space)
            {
                MessageBox.Show("We dont accept spaces in the invitation code.", "eternity.us");
                TextInvite.Clear();
                e.Handled = true;
            }
            if (key == Keys.Enter)
            {
                MessageBox.Show("If you want to sumbit just click in the button, dont press enter.", "eternity.us");
                TextInvite.Clear();
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        private void ClearUser_Click(object sender, EventArgs e)
        {
            ClearUser.ForeColor = ColorTranslator.FromHtml("#cecece");
            TextUser.Clear();
        }

        private void ClearPass_Click(object sender, EventArgs e)
        {
            ClearPass.ForeColor = ColorTranslator.FromHtml("#cecece");
            TextPassword.Clear();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Process[] runningProcesses = Process.GetProcesses();

            #region AntiDump

            //Smart bypass: change .exe name like: ProcessHacker to notProcessHacker and done :)
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
    }
}
