using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static CSimple.SDK.Memory;
using System.Threading;
using static CSimple.SDK.Offsets;
using CSimple.SDK;
using static CSimple.SDK.Structs.GlowObject;
using CSimple.SDK.Structs;
using System.Globalization;
using static CSimple.Hack_Stuff.hsTrigger;
using static CSimple.Hack_Stuff.hsGlow;
using static CSimple.Hack_Stuff.hsNoFlash;
using static CSimple.Hack_Stuff.hsBhopFOV;
using static CSimple.Hack_Stuff.hsHotKeys;
using static CSimple.Hack_Stuff.hsAimbot;
using static CSimple.Hack_Stuff.hsValues;


namespace CSimple
{

    public partial class Form1 : Form
    {
        public static string version = "0.0.1";
        int currentOpen = 0;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetAsyncKeyState(int vKey);

        public Form1()
        {
            InitializeComponent();
            Init();


        }
        public void Init()
        {
            var CSGO = Process.GetProcessesByName("csgo");
            if (CSGO.Length != 0)
            {
                g_pProcess = CSGO[0];
                g_pProcessHandle = OpenProcess(0x0008 | 0x0010 | 0x0020, false, g_pProcess.Id);
                foreach (ProcessModule Module in g_pProcess.Modules)
                {
                    if ((Module.ModuleName == "client_panorama.dll"))
                        g_pClient = Module.BaseAddress;

                    if ((Module.ModuleName == "engine.dll"))
                        g_pEngine = Module.BaseAddress;
                }
                Thread BhopFOV = new Thread(Bunnyhop_Fov);
                Thread HotKey = new Thread(hotKeys);
                Thread Trigger1 = new Thread(Trigger_Loop);
                Thread Trigger2 = new Thread(Trigger_Loop);
                Thread Esp = new Thread(glow);
                Thread NoFlash = new Thread(noFlash);
                Thread Aim = new Thread(Aimware);

                BhopFOV.Start();
                HotKey.Start();
                Trigger1.Start();
                Trigger2.Start();
                Esp.Start();
                NoFlash.Start();
                Aim.Start();
            }
            else
            {
                MessageBox.Show("Start csgo.exe!", "CSimple", MessageBoxButtons.OK);
                Environment.Exit(1);
            }

        }
        
        public static bool IsVisible(int local, int entity)
        {
            int mask = ReadMemory<int>(entity + m_bSpottedByMask);
            int PBASE = ReadMemory<int>(local + 0x64) - 1;
            return (mask & (1 << PBASE)) > 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Application.StartupPath + @"\config.ini"))
            {
                System.IO.File.WriteAllText(Application.StartupPath + @"\config.ini", Config.defaultConfig());
            }
            else
            {
                List<string> b = new List<String>(Microsoft.VisualBasic.Strings.Split(System.IO.File.ReadAllText(Application.StartupPath + @"\config.ini"), Environment.NewLine));
                List<string> a = new List<String>(Microsoft.VisualBasic.Strings.Split(Config.defaultConfig(), Environment.NewLine));

                if (a.Count != b.Count)
                {
                    System.IO.File.WriteAllText(Application.StartupPath + @"\config.ini", Config.defaultConfig());
                }
            }
            LoadScreen LoadingScreen = new LoadScreen();
            LoadingScreen.ShowDialog();
            tabControl1.SelectedIndex = 0;
            if (Config.Get("bot-enabled", true))
            {
                chkBotActive.Checked = true;
            }
            else
            {
                chkBotActive.Checked = false;
            }
            

   
        }

        private void GlowBox_CheckedChanged(object sender, EventArgs e)
        {
            Globals.bGlow = !Globals.bGlow;
        }

        private void RadarBox_CheckedChanged(object sender, EventArgs e)
        {
            Globals.bRadar = !Globals.bRadar;
        }

        private void GlowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NoFlashCheck_CheckedChanged(object sender, EventArgs e)
        {
            Globals.bNoflash = !Globals.bNoflash;
        }

        private void HeaderClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void FovCheck_CheckedChanged(object sender, EventArgs e)
        {
            Globals.bFov = !Globals.bFov;
            MessageBox.Show(Config.Get("version", "str") + "");
        }

        private void TriggerCheck_CheckedChanged(object sender, EventArgs e)
        {
            Globals.bTrigger = !Globals.bTrigger;
        }

        private void TriggerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.iTriggerKey = TriggerCombo.SelectedIndex;
        }

        private void Header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void HeaderTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {


        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {



        }

        private void panel4_Click(object sender, EventArgs e)
        {

        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {




        }

        private void label4_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_MouseClick(object sender, MouseEventArgs e)
        {



        }

        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {




        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_MouseClick(object sender, MouseEventArgs e)
        {



        }

        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {


    
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


        }
        public void wait(int interval)
        {
            Stopwatch stopw = new Stopwatch();
            stopw.Start();
            while (stopw.ElapsedMilliseconds < interval)
            {
                Application.DoEvents();
            }
            stopw.Stop();

        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int playerAddress = Offsets.dwLocalPlayer;
            int playerHealthOffset = Offsets.m_iHealth;
            VAMemory vam = new VAMemory("csgo");

            int baseAddress = (int)Process.GetProcessesByName("csgo")[0].MainModule.BaseAddress;

            int playerBaseAddress = vam.ReadInt32((IntPtr)baseAddress + playerAddress);

            int playerHealthAddress = playerBaseAddress + playerHealthOffset;


            var playerHealth = vam.ReadFloat((IntPtr)playerHealthAddress);


        }

        private void bunifuGradientPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuGradientPanel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuGradientPanel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuGradientPanel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuGradientPanel5_MouseHover(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuGradientPanel5_MouseLeave(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel5_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 0;




        }

        private void bunifuGradientPanel6_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 1;

        }

        private void bunifuGradientPanel8_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 2;

        }

        private void bunifuGradientPanel7_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 3;

        }

        private void bunifuGradientPanel9_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 4;

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 0;

        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 1;

        }

        private void label3_MouseClick_1(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 2;

        }

        private void label4_MouseClick_1(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 3;

        }

        private void label5_MouseClick_1(object sender, MouseEventArgs e)
        {
            tabControl1.SelectedIndex = 4;

        }

        private void label6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Bot_Page_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCheckbox2_OnChange(object sender, EventArgs e)
        {
            if(bunifuCheckbox2.Checked == true)
            {
                Globals.bTrigger = !Globals.bTrigger;
            }
            else
            {
                if (bunifuCheckbox2.Checked == false)
                {
                    Globals.bTrigger = false;
                }
            }
            
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if(chkBotActive.Checked == true)
            {
                Globals.BotEnable = true;
            }
            else
            {
                if(chkBotActive.Checked == false)
                {
                    Globals.BotEnable = false;
                }
            }
        }

        private void bunifuCheckbox3_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox3.Checked == true)
            {
                Globals.autoFire = true;
            }
            else
            {
                if (bunifuCheckbox3.Checked == false)
                {
                    Globals.autoFire = false;
                }
            }
        }

        private void panel8_DragOver(object sender, DragEventArgs e)
        {
            


        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
 
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void macTrackBar1_ValueChanged(object sender, decimal value)
        {
            int current = macTrackBar1.Value;
            decimal d = (decimal)current / 100;
            label18.Text = d.ToString();
            Globals.Delay = current;
            
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void bunifuCheckbox5_OnChange(object sender, EventArgs e)
        {
            Globals.iGlowMode = 1;
        }

        private void bunifuCheckbox6_OnChange(object sender, EventArgs e)
        {
            if (chkVis.Checked)
            {
                Globals.iGlowMode = 2;
                chkEnemyVis.Checked = true;
                chkTeamVis.Checked = true;
                chkEnemyVis.Enabled = true;
                chkTeamVis.Enabled = true;
                Hack_Stuff.hsValues.chkEnemyVis = true;
                Hack_Stuff.hsValues.chkTeamVis = true;
                Hack_Stuff.hsValues.chkVis = true;
            }
            else
            {
                Globals.iGlowMode = 2;
                chkEnemyVis.Checked = false;
                chkTeamVis.Checked = false;
                chkEnemyVis.Enabled = false;
                chkTeamVis.Enabled = false;
                Hack_Stuff.hsValues.chkEnemyVis = false;
                Hack_Stuff.hsValues.chkTeamVis = false;
                Hack_Stuff.hsValues.chkVis = false;
            }
            
        }

        private void bunifuCheckbox7_OnChange(object sender, EventArgs e)
        {
            Globals.iGlowMode = 0;
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Visuals_Page_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCheckbox8_OnChange(object sender, EventArgs e)
        {
            if(bunifuCheckbox8.Checked == true)
            {
                bunifuCheckbox5.Checked = true;
                Globals.bGlow = true;
            }
            else
            {
                if(bunifuCheckbox8.Checked == false)
                {
                    Globals.bGlow = false;
                }
            }
        }

        private void bunifuCheckbox4_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox4.Checked == true)
            {
                Globals.VisualsEnable = true;
            }
            else if (bunifuCheckbox4.Checked == false)
            {
                Globals.VisualsEnable = false;
            }
        }

        private void bunifuCheckbox9_OnChange(object sender, EventArgs e)
        {
            if(bunifuCheckbox9.Checked == true)
            {
                Globals.bNoflash = true;

            }
            else
            {
                if(bunifuCheckbox9.Checked == false)
                {
                    Globals.bNoflash = false;
                }
            }
        }

        private void bunifuCheckbox9_OnChange_1(object sender, EventArgs e)
        {
            if(bunifuCheckbox9.Checked == true)
            {
                Globals.bRadar = true;
            }
            else
            {
                if(bunifuCheckbox9.Checked == false)
                {
                    Globals.bRadar = false;
                }
            }
        }
        class XEHHID8RD2MRQ
        {
            void A1S0JNSL5EBR()
            {
                int D4KDWF9IRCXAM = 251367151;
                if (D4KDWF9IRCXAM > 251367182)
                    D4KDWF9IRCXAM = 251367110;
                else if (D4KDWF9IRCXAM <= 251367123)
                    D4KDWF9IRCXAM++;
                else
                    D4KDWF9IRCXAM = (251367126 / 251367136);
                int DGXYC5H2IXDM4 = 251367169;
                if (DGXYC5H2IXDM4 > 251367109)
                    DGXYC5H2IXDM4 = 251367118;
                else if (DGXYC5H2IXDM4 <= 251367109)
                    DGXYC5H2IXDM4++;
                else
                    DGXYC5H2IXDM4 = (251367132 / 251367171);
                int DNB03EDOMCAEX = 251367176;
                if (DNB03EDOMCAEX > 251367172)
                    DNB03EDOMCAEX = 251367112;
                else if (DNB03EDOMCAEX <= 251367135)
                    DNB03EDOMCAEX++;
                else
                    DNB03EDOMCAEX = (251367125 / 251367187);
                bool DOGXX5N25HAMW = false;
                if (!DOGXX5N25HAMW)
                    DOGXX5N25HAMW = true;
                else if (DOGXX5N25HAMW = true)
                    DOGXX5N25HAMW = true;
                else
                    DOGXX5N25HAMW = true;
                int DKNDRB03BYJKO = 251367171;
                if (DKNDRB03BYJKO > 251367151)
                    DKNDRB03BYJKO = 251367134;
                else if (DKNDRB03BYJKO <= 251367115)
                    DKNDRB03BYJKO++;
                else
                    DKNDRB03BYJKO = (251367122 / 251367130);
                int D77RWYZE4WZAO = 251367183;
                if (D77RWYZE4WZAO > 251367157)
                    D77RWYZE4WZAO = 251367185;
                else if (D77RWYZE4WZAO <= 251367130)
                    D77RWYZE4WZAO++;
                else
                    D77RWYZE4WZAO = (251367184 / 251367182);
                bool D44CL1DMOJO4R = true;
                if (!D44CL1DMOJO4R)
                    D44CL1DMOJO4R = false;
                else if (D44CL1DMOJO4R = false)
                    D44CL1DMOJO4R = true;
                else
                    D44CL1DMOJO4R = false;
                int DJBNQZWLKA2LQ = 251367184;
                if (DJBNQZWLKA2LQ > 251367182)
                    DJBNQZWLKA2LQ = 251367192;
                else if (DJBNQZWLKA2LQ <= 251367182)
                    DJBNQZWLKA2LQ++;
                else
                    DJBNQZWLKA2LQ = (251367141 / 251367192);
                bool D0DRQFNAWBMYD = true;
                if (!D0DRQFNAWBMYD)
                    D0DRQFNAWBMYD = true;
                else if (D0DRQFNAWBMYD = true)
                    D0DRQFNAWBMYD = true;
                else
                    D0DRQFNAWBMYD = true;
                bool DWYGJWREBD4G4 = true;
                if (!DWYGJWREBD4G4)
                    DWYGJWREBD4G4 = false;
                else if (DWYGJWREBD4G4 = true)
                    DWYGJWREBD4G4 = false;
                else
                    DWYGJWREBD4G4 = true;
                int D4RG33ICC3HAW = 251367174;
                if (D4RG33ICC3HAW > 251367190)
                    D4RG33ICC3HAW = 251367108;
                else if (D4RG33ICC3HAW <= 251367150)
                    D4RG33ICC3HAW++;
                else
                    D4RG33ICC3HAW = (251367152 / 251367135);
                int DY5CL79CY2G7X = 251367180;
                if (DY5CL79CY2G7X > 251367149)
                    DY5CL79CY2G7X = 251367114;
                else if (DY5CL79CY2G7X <= 251367136)
                    DY5CL79CY2G7X++;
                else
                    DY5CL79CY2G7X = (251367113 / 251367111);
                bool DC37P4LEAH9YA = true;
                if (!DC37P4LEAH9YA)
                    DC37P4LEAH9YA = true;
                else if (DC37P4LEAH9YA = false)
                    DC37P4LEAH9YA = false;
                else
                    DC37P4LEAH9YA = false;
                bool DE2GQ0Q6XERBI = true;
                if (!DE2GQ0Q6XERBI)
                    DE2GQ0Q6XERBI = true;
                else if (DE2GQ0Q6XERBI = false)
                    DE2GQ0Q6XERBI = false;
                else
                    DE2GQ0Q6XERBI = true;
                bool D0XINXEFNCOIB = false;
                if (!D0XINXEFNCOIB)
                    D0XINXEFNCOIB = false;
                else if (D0XINXEFNCOIB = true)
                    D0XINXEFNCOIB = false;
                else
                    D0XINXEFNCOIB = false;
                bool D760QRSYQSMFM = false;
                if (!D760QRSYQSMFM)
                    D760QRSYQSMFM = true;
                else if (D760QRSYQSMFM = false)
                    D760QRSYQSMFM = false;
                else
                    D760QRSYQSMFM = false;
                int D7EN725RHI5YC = 251367130;
                if (D7EN725RHI5YC > 251367159)
                    D7EN725RHI5YC = 251367164;
                else if (D7EN725RHI5YC <= 251367133)
                    D7EN725RHI5YC++;
                else
                    D7EN725RHI5YC = (251367154 / 251367151);
                int DKY4HL56GCHIP = 251367185;
                if (DKY4HL56GCHIP > 251367188)
                    DKY4HL56GCHIP = 251367168;
                else if (DKY4HL56GCHIP <= 251367105)
                    DKY4HL56GCHIP++;
                else
                    DKY4HL56GCHIP = (251367137 / 251367103);
                int D69XDZKIB2DER = 251367186;
                if (D69XDZKIB2DER > 251367166)
                    D69XDZKIB2DER = 251367120;
                else if (D69XDZKIB2DER <= 251367115)
                    D69XDZKIB2DER++;
                else
                    D69XDZKIB2DER = (251367192 / 251367141);
                int D0L3OA4RPF8BF = 251367154;
                if (D0L3OA4RPF8BF > 251367173)
                    D0L3OA4RPF8BF = 251367121;
                else if (D0L3OA4RPF8BF <= 251367118)
                    D0L3OA4RPF8BF++;
                else
                    D0L3OA4RPF8BF = (251367182 / 251367135);
                int D33M3ZOB7GYPZ = 251367149;
                if (D33M3ZOB7GYPZ > 251367171)
                    D33M3ZOB7GYPZ = 251367118;
                else if (D33M3ZOB7GYPZ <= 251367171)
                    D33M3ZOB7GYPZ++;
                else
                    D33M3ZOB7GYPZ = (251367187 / 251367177);
                bool D2H7QAC9IANK4 = true;
                if (!D2H7QAC9IANK4)
                    D2H7QAC9IANK4 = true;
                else if (D2H7QAC9IANK4 = false)
                    D2H7QAC9IANK4 = true;
                else
                    D2H7QAC9IANK4 = true;
                bool D6C4DL4FHI85E = true;
                if (!D6C4DL4FHI85E)
                    D6C4DL4FHI85E = true;
                else if (D6C4DL4FHI85E = true)
                    D6C4DL4FHI85E = true;
                else
                    D6C4DL4FHI85E = true;
                bool D0CM0F6JCROPW = true;
                if (!D0CM0F6JCROPW)
                    D0CM0F6JCROPW = true;
                else if (D0CM0F6JCROPW = false)
                    D0CM0F6JCROPW = true;
                else
                    D0CM0F6JCROPW = true;
                int D6JBG2WLE0NSX = 251367165;
                if (D6JBG2WLE0NSX > 251367188)
                    D6JBG2WLE0NSX = 251367177;
                else if (D6JBG2WLE0NSX <= 251367186)
                    D6JBG2WLE0NSX++;
                else
                    D6JBG2WLE0NSX = (251367115 / 251367106);
                int DKPIN2Y0CD9HI = 251367131;
                if (DKPIN2Y0CD9HI > 251367170)
                    DKPIN2Y0CD9HI = 251367115;
                else if (DKPIN2Y0CD9HI <= 251367172)
                    DKPIN2Y0CD9HI++;
                else
                    DKPIN2Y0CD9HI = (251367120 / 251367167);
                int D7PY8XQN8LZGB = 251367155;
                if (D7PY8XQN8LZGB > 251367118)
                    D7PY8XQN8LZGB = 251367144;
                else if (D7PY8XQN8LZGB <= 251367155)
                    D7PY8XQN8LZGB++;
                else
                    D7PY8XQN8LZGB = (251367171 / 251367196);
                int DX8FB93JOX1BO = 251367182;
                if (DX8FB93JOX1BO > 251367117)
                    DX8FB93JOX1BO = 251367137;
                else if (DX8FB93JOX1BO <= 251367197)
                    DX8FB93JOX1BO++;
                else
                    DX8FB93JOX1BO = (251367178 / 251367190);
                int D2PGD65102KAX = 251367109;
                if (D2PGD65102KAX > 251367121)
                    D2PGD65102KAX = 251367193;
                else if (D2PGD65102KAX <= 251367103)
                    D2PGD65102KAX++;
                else
                    D2PGD65102KAX = (251367131 / 251367150);
                int D74SMLXBKEKLS = 251367152;
                if (D74SMLXBKEKLS > 251367149)
                    D74SMLXBKEKLS = 251367102;
                else if (D74SMLXBKEKLS <= 251367120)
                    D74SMLXBKEKLS++;
                else
                    D74SMLXBKEKLS = (251367102 / 251367164);
                int DKZ5PQZBOIQ9N = 251367120;
                if (DKZ5PQZBOIQ9N > 251367188)
                    DKZ5PQZBOIQ9N = 251367165;
                else if (DKZ5PQZBOIQ9N <= 251367180)
                    DKZ5PQZBOIQ9N++;
                else
                    DKZ5PQZBOIQ9N = (251367171 / 251367132);
                bool D96LE1Q017WRP = true;
                if (!D96LE1Q017WRP)
                    D96LE1Q017WRP = true;
                else if (D96LE1Q017WRP = false)
                    D96LE1Q017WRP = true;
                else
                    D96LE1Q017WRP = false;
            }
        }
    private void bunifuCheckbox12_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox12.Checked == true)
            {
                Globals.MiscEnable = true;
            }
            else
            {
                Globals.MiscEnable = false;
            }
    
        }

        private void bunifuCheckbox16_OnChange(object sender, EventArgs e)
        {
            if(bunifuCheckbox16.Checked == true)
            {
                Globals.BunnyhopEnable = true;
            }
            else
            {
                Globals.BunnyhopEnable = false;
            }

        }

        private void bunifuCheckbox15_OnChange(object sender, EventArgs e)
        {
            if(bunifuCheckbox15.Checked == true)
            {
                Globals.BunnyhopAlwaysOn = true;
            }
            else
            {
                Globals.BunnyhopAlwaysOn = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.BunnyKey = comboBox3.SelectedIndex;
        }

        private void macTrackBar4_ValueChanged(object sender, decimal value)
        {
            decimal divideBy = Convert.ToDecimal(10);
            decimal macR = Convert.ToDecimal(macTrackBar4.Value);
            decimal macG = Convert.ToDecimal(macTrackBar5.Value);
            decimal macB = Convert.ToDecimal(macTrackBar6.Value);
            terrorR = float.Parse((macR / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            terrorG = float.Parse((macG / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            terrorB = float.Parse((macB / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            System.Drawing.Color myRgbColor = new System.Drawing.Color();
            myRgbColor = System.Drawing.Color.FromArgb(macTrackBar4.Value * 25, macTrackBar5.Value * 25, macTrackBar6.Value * 25);
            label48.BackColor = myRgbColor;
        }

        private void macTrackBar5_ValueChanged(object sender, decimal value)
        {
            decimal divideBy = Convert.ToDecimal(10);
            decimal macR = Convert.ToDecimal(macTrackBar4.Value);
            decimal macG = Convert.ToDecimal(macTrackBar5.Value);
            decimal macB = Convert.ToDecimal(macTrackBar6.Value);
            terrorR = float.Parse((macR / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            terrorG = float.Parse((macG / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            terrorB = float.Parse((macB / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            System.Drawing.Color myRgbColor = new System.Drawing.Color();
            myRgbColor = System.Drawing.Color.FromArgb(macTrackBar4.Value * 25, macTrackBar5.Value * 25, macTrackBar6.Value * 25);
            label48.BackColor = myRgbColor;
        }

        private void macTrackBar6_ValueChanged(object sender, decimal value)
        {
            decimal divideBy = Convert.ToDecimal(10);
            decimal macR = Convert.ToDecimal(macTrackBar4.Value);
            decimal macG = Convert.ToDecimal(macTrackBar5.Value);
            decimal macB = Convert.ToDecimal(macTrackBar6.Value);
            terrorR = float.Parse((macR / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            terrorG = float.Parse((macG / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            terrorB = float.Parse((macB / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            System.Drawing.Color myRgbColor = new System.Drawing.Color();
            myRgbColor = System.Drawing.Color.FromArgb(macTrackBar4.Value * 25, macTrackBar5.Value * 25, macTrackBar6.Value * 25);
            label48.BackColor = myRgbColor;
        }

        private void macTrackBar9_ValueChanged(object sender, decimal value)
        {
            decimal divideBy = Convert.ToDecimal(10);
            decimal macR = Convert.ToDecimal(macTrackBar9.Value);
            decimal macG = Convert.ToDecimal(macTrackBar8.Value);
            decimal macB = Convert.ToDecimal(macTrackBar7.Value);
            CterrorR = float.Parse((macR / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            CterrorG = float.Parse((macG / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            CterrorB = float.Parse((macB / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            System.Drawing.Color myRgbColor = new System.Drawing.Color();
            myRgbColor = System.Drawing.Color.FromArgb(macTrackBar9.Value * 25, macTrackBar8.Value * 25, macTrackBar7.Value * 25);
            label45.BackColor = myRgbColor;
        }

        private void macTrackBar8_ValueChanged(object sender, decimal value)
        {
            decimal divideBy = Convert.ToDecimal(10);
            decimal macR = Convert.ToDecimal(macTrackBar9.Value);
            decimal macG = Convert.ToDecimal(macTrackBar8.Value);
            decimal macB = Convert.ToDecimal(macTrackBar7.Value);
            CterrorR = float.Parse((macR / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            CterrorG = float.Parse((macG / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            CterrorB = float.Parse((macB / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            System.Drawing.Color myRgbColor = new System.Drawing.Color();
            myRgbColor = System.Drawing.Color.FromArgb(macTrackBar9.Value * 25, macTrackBar8.Value * 25, macTrackBar7.Value * 25);
            label45.BackColor = myRgbColor;
        }

        private void macTrackBar7_ValueChanged(object sender, decimal value)
        {
            decimal divideBy = Convert.ToDecimal(10);
            decimal macR = Convert.ToDecimal(macTrackBar9.Value);
            decimal macG = Convert.ToDecimal(macTrackBar8.Value);
            decimal macB = Convert.ToDecimal(macTrackBar7.Value);
            CterrorR = float.Parse((macR / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            CterrorG = float.Parse((macG / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            CterrorB = float.Parse((macB / divideBy) + "", CultureInfo.InvariantCulture.NumberFormat);
            System.Drawing.Color myRgbColor = new System.Drawing.Color();
            myRgbColor = System.Drawing.Color.FromArgb(macTrackBar9.Value * 25, macTrackBar8.Value * 25, macTrackBar7.Value * 25);
            label45.BackColor = myRgbColor;
        }

        private void cbGlow_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.iGlowEspKey = cbGlow.SelectedIndex;
        }

        private void cbTigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.iTriggerToggleKey = cbTigger.SelectedIndex;
        }

        private void cbRadar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.iRadarKey = cbRadar.SelectedIndex;
        }

        private void cbNoFlash_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.iNoFlashKey = cbNoFlash.SelectedIndex;
        }

        private void chkEnemyVis_OnChange(object sender, EventArgs e)
        {
            if (chkEnemyVis.Checked)
            {
                Globals.bEnemyVis = true;
                Hack_Stuff.hsValues.chkEnemyVis = true;
            } 
            else
            {
                Globals.bEnemyVis = false;
                Hack_Stuff.hsValues.chkEnemyVis = false;
            }
            if (!chkTeamVis.Checked && !chkEnemyVis.Checked)
            {
                chkVis.Checked = false;
                Hack_Stuff.hsValues.chkVis = false;
            } 
            else
            {
                chkVis.Checked = true;
                Hack_Stuff.hsValues.chkVis = true;
            }
            

        }

        private void chkTeamVis_OnChange(object sender, EventArgs e)
        {
            if (chkTeamVis.Checked)
            {
                Globals.bTeammatesVis = true;
                Hack_Stuff.hsValues.chkTeamVis = true;
            }
            else
            {
                Globals.bTeammatesVis = false;
                Hack_Stuff.hsValues.chkTeamVis = false;
            }
            MessageBox.Show(Hack_Stuff.hsValues.chkTeamVis + "");
        }

        private void bunifuCheckbox10_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox10.Checked)
            {
                Globals.bNoflash = true;
            }
            else
            {
                Globals.bNoflash = false;
            }
        }
    }

    
}
