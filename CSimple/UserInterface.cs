using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CSimple.Options.Startup.Startup;
using System.Threading;
namespace CSimple
{
    public partial class UserInterface : Form
    {
        //----------Checked Values. 0 = False, 1 = True----------

        //Bot Tab
            //----------TriggerBot>
        int TriggerbotCheckValue = 0;
        int Triggerbot_AutoFire = 0;
        //End Bot Tab

        //Visuals Tab
            //----------GlowESP>
        int GlowESPCheckValue = 0; 
        int GlowESP_Enemy = 0;
        int GlowESP_Team = 0;

            //----------Other>
        int OtherCheckValue = 0;
        int Other_NoFlash = 0;
        int Other_Radar = 0;
        //End Visuals Tab

        //Misc Tab
            //----------Bunnyhop>
        int BunnyhopCheckValue = 0;
        //End Misc Tab

        //End Check Mark Values
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetAsyncKeyState(int vKey);
        public UserInterface()
        {
            InitializeComponent();
        }
        public void startup()
        {
            visuals_panel.Visible = true;
            bot_panel.Visible = true;
            colors_panel.Visible = true;
            misc_panel.Visible = true;
            other_panel.Visible = true;
            settings_panel.Visible = true;

            
            
            bot_settings_panel.Visible = true;
            colors_settings_panel.Visible = true;
            misc_settings_panel.Visible = true;
            settings_settings_panel.Visible = true;
            other_settings_panel.Visible = true;
            visuals_settings_panel.Visible = true;

            visuals_panel.Dock = DockStyle.Fill;
            bot_panel.Dock = DockStyle.Fill;
            colors_panel.Dock = DockStyle.Fill;
            misc_panel.Dock = DockStyle.Fill;
            updates_panel.Dock = DockStyle.Fill;
            other_panel.Dock = DockStyle.Fill;


            visuals_settings_panel.Dock = DockStyle.Fill;
            bot_settings_panel.Dock = DockStyle.Fill;
            colors_settings_panel.Dock = DockStyle.Fill;
            misc_settings_panel.Dock = DockStyle.Fill;
            settings_settings_panel.Dock = DockStyle.Fill;
            other_settings_panel.Dock = DockStyle.Fill;


            visuals_panel.Visible = false;
            bot_panel.Visible = false;
            colors_panel.Visible = false;
            misc_panel.Visible = false;

            other_panel.Visible = false;
            settings_panel.Visible = false;


            visuals_settings_panel.Visible = false;
            bot_settings_panel.Visible = false;
            colors_settings_panel.Visible = false;
            misc_settings_panel.Visible = false;
            settings_settings_panel.Visible = false;
            other_settings_panel.Visible = false;

            bunifuFlatButton1.selected = true;

        }
        public void last()
        {
            Thread.Sleep(1000);
            updates_panel.Visible = false;
            updates_settings_panel.Visible = false;
            updates_panel.Dock = DockStyle.Fill;
            updates_settings_panel.Dock = DockStyle.Fill;
            updates_panel.Visible = true;
            updates_settings_panel.Visible = true;
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void btnMnu_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void btnMnu_Click(object sender, EventArgs e)
        {
            if (sidemenu.Width == 60)
            {
                //EXPAND
                //1 EXPAND the panel
                //2 Show Logo
                sidemenu.Visible = false;
                sidemenu.Width = 278;
                PanelAnimator.ShowSync(sidemenu);
                LogoAnimator.ShowSync(logo);
            }
            else
            {
                //minimize
                //1 hide logo
                //2 slide the panel
                //btnMnu.Location = new Point(15, 21);
                LogoAnimator.HideSync(logo);
                sidemenu.Visible = false;
                sidemenu.Width = 60;
                PanelAnimator.ShowSync(sidemenu);

            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sidemenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserInterface_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch
            {
                int a = 0;
                a += 1;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //Options Column [Updates Section]
            visuals_panel.Visible = false;
            bot_panel.Visible = false;
            colors_panel.Visible = false;
            misc_panel.Visible = false;
            settings_panel.Visible = false;
            other_panel.Visible = false;

            updates_panel.Dock = DockStyle.Fill;
            //updates_panel.Visible = true;

            ItemPanelAnimator.Show(updates_panel);

            //Settings Column [Updates Section]
            visuals_settings_panel.Visible = false;
            bot_settings_panel.Visible = false;
            colors_settings_panel.Visible = false;
            misc_settings_panel.Visible = false;
            settings_settings_panel.Visible = false;
            other_settings_panel.Visible = false;

            updates_settings_panel.Dock = DockStyle.Fill;
            //updates_settings_panel.Visible = true;

            settings_ItemPanelAnimator.Show(updates_settings_panel);



        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //Options Column [Bot Section]
            visuals_panel.Visible = false;
            updates_panel.Visible = false;
            colors_panel.Visible = false;
            misc_panel.Visible = false;
            settings_panel.Visible = false;
            other_panel.Visible = false;

            bot_panel.Dock = DockStyle.Fill;
            //bot_panel.Visible = true;

            ItemPanelAnimator.Show(bot_panel);

            //Settings Column [Bot Section]
            visuals_settings_panel.Visible = false;
            updates_settings_panel.Visible = false;
            colors_settings_panel.Visible = false;
            misc_settings_panel.Visible = false;
            settings_settings_panel.Visible = false;
            other_settings_panel.Visible = false;

            bot_settings_panel.Dock = DockStyle.Fill;
            //bot_settings_panel.Visible = true;
            settings_ItemPanelAnimator.Show(bot_settings_panel);
        }

        private void panel_holder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void visuals_panel_Click(object sender, EventArgs e)
        {
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //Options Column [Visuals Section]
            updates_panel.Visible = false;
            bot_panel.Visible = false;
            colors_panel.Visible = false;
            misc_panel.Visible = false;
            settings_panel.Visible = false;
            other_panel.Visible = false;

            visuals_panel.Dock = DockStyle.Fill;
            //visuals_panel.Visible = true;

            ItemPanelAnimator.Show(visuals_panel);

            //Settings Column [Visuals Section]
            bot_settings_panel.Visible = false;
            updates_settings_panel.Visible = false;
            colors_settings_panel.Visible = false;
            misc_settings_panel.Visible = false;
            settings_settings_panel.Visible = false;
            other_settings_panel.Visible = false;

            visuals_settings_panel.Dock = DockStyle.Fill;
            //visuals_settings_panel.Visible = true;

            settings_ItemPanelAnimator.Show(visuals_settings_panel);


        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            //Options Column [Colors Section]
            updates_panel.Visible = false;
            bot_panel.Visible = false;
            visuals_panel.Visible = false;
            misc_panel.Visible = false;
            settings_panel.Visible = false;
            other_panel.Visible = false;

            colors_panel.Dock = DockStyle.Fill;
            //colors_panel.Visible = true;

            ItemPanelAnimator.Show(colors_panel);

            //Settings Column [Colors Section]
            bot_settings_panel.Visible = false;
            updates_settings_panel.Visible = false;
            visuals_settings_panel.Visible = false;
            misc_settings_panel.Visible = false;
            settings_settings_panel.Visible = false;
            other_settings_panel.Visible = false;

            colors_settings_panel.Dock = DockStyle.Fill;
            //colors_settings_panel.Visible = true;

            settings_ItemPanelAnimator.Show(colors_settings_panel);
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            //Options Column [Misc Section]
            updates_panel.Visible = false;
            bot_panel.Visible = false;
            visuals_panel.Visible = false;
            colors_panel.Visible = false;
            settings_panel.Visible = false;
            other_panel.Visible = false;

            misc_panel.Dock = DockStyle.Fill;
            //misc_panel.Visible = true;

            ItemPanelAnimator.Show(misc_panel);

            //Settings Column [Misc Section]
            bot_settings_panel.Visible = false;
            updates_settings_panel.Visible = false;
            visuals_settings_panel.Visible = false;
            colors_settings_panel.Visible = false;
            settings_settings_panel.Visible = false;
            other_settings_panel.Visible = false;

            misc_settings_panel.Dock = DockStyle.Fill;
            //misc_settings_panel.Visible = true;

            settings_ItemPanelAnimator.Show(misc_settings_panel);
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            //Options Column [Settings Section]
            updates_panel.Visible = false;
            bot_panel.Visible = false;
            visuals_panel.Visible = false;
            colors_panel.Visible = false;
            misc_panel.Visible = false;
            other_panel.Visible = false;

            settings_panel.Dock = DockStyle.Fill;
            //settings_panel.Visible = true;

            ItemPanelAnimator.Show(settings_panel);

            //Settings Column [Settings Section]
            bot_settings_panel.Visible = false;
            updates_settings_panel.Visible = false;
            visuals_settings_panel.Visible = false;
            colors_settings_panel.Visible = false;
            misc_settings_panel.Visible = false;
            other_settings_panel.Visible = false;

            settings_settings_panel.Dock = DockStyle.Fill;
            //settings_settings_panel.Visible = true;

            settings_ItemPanelAnimator.Show(settings_settings_panel);
        }

        private void other_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void other_panel_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            //Options Column [Other Section]
            updates_panel.Visible = false;
            bot_panel.Visible = false;
            visuals_panel.Visible = false;
            colors_panel.Visible = false;
            misc_panel.Visible = false;
            settings_panel.Visible = false;

            other_panel.Dock = DockStyle.Fill;
            //other_panel.Visible = true;

            ItemPanelAnimator.Show(other_panel);

            //Settings Column [Other Section]
            bot_settings_panel.Visible = false;
            updates_settings_panel.Visible = false;
            visuals_settings_panel.Visible = false;
            colors_settings_panel.Visible = false;
            misc_settings_panel.Visible = false;
            settings_settings_panel.Visible = false;

            other_settings_panel.Dock = DockStyle.Fill;
            //other_settings_panel.Visible = true;

            settings_ItemPanelAnimator.Show(other_settings_panel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadScreen Load = new LoadScreen();
            Load.ShowDialog();
            this.Close();
            
        }

        private void panel11_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel9_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        
        
        private void panel4_Click(object sender, EventArgs e)
        {
            if(TriggerbotCheckValue == 0)
            {
                //Checked On
                TriggerbotCheckValue = 1;
                panel4.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel4.BackColor = Color.FromArgb(255, 253, 232);
                panel5.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if(TriggerbotCheckValue == 1)
            {
                //Checked Off
                TriggerbotCheckValue = 0;
                panel4.BackgroundImage = base.BackgroundImage;
                panel4.BackColor = Color.WhiteSmoke;
                panel5.BackColor = Color.WhiteSmoke;

            }


        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void alphaGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void panel16_Click(object sender, EventArgs e)
        {
            if (Triggerbot_AutoFire == 0 )
            {
                //Checked On
                Triggerbot_AutoFire = 1;
                panel16.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel16.BackColor = Color.FromArgb(255, 253, 232);
                panel15.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (Triggerbot_AutoFire == 1)
            {
                //Checked Off
                Triggerbot_AutoFire = 0;
                panel16.BackgroundImage = base.BackgroundImage;
                panel16.BackColor = Color.WhiteSmoke;
                panel15.BackColor = Color.WhiteSmoke;

            }
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_MouseHover(object sender, EventArgs e)
        {

        }

        private void bot_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel29_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel30_Click(object sender, EventArgs e)
        {

        }

        private void panel27_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void panel28_Click(object sender, EventArgs e)
        {
            if (GlowESPCheckValue == 0)
            {
                //Checked On
                GlowESPCheckValue = 1;
                panel28.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel28.BackColor = Color.FromArgb(255, 253, 232);
                panel27.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (GlowESPCheckValue == 1)
            {
                //Checked Off
                GlowESPCheckValue = 0;
                panel28.BackgroundImage = base.BackgroundImage;
                panel28.BackColor = Color.WhiteSmoke;
                panel27.BackColor = Color.WhiteSmoke;

            }
        }

        private void panel28_Paint(object sender, PaintEventArgs e)
        {

        }

        private void alphaGradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }
        
        private void panel26_Click(object sender, EventArgs e)
        {
            if (GlowESP_Enemy == 0)
            {
                //Checked On
                GlowESP_Enemy = 1;
                panel26.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel26.BackColor = Color.FromArgb(255, 253, 232);
                panel3.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (GlowESP_Enemy == 1)
            {
                //Checked Off
                GlowESP_Enemy = 0;
                panel26.BackgroundImage = base.BackgroundImage;
                panel26.BackColor = Color.WhiteSmoke;
                panel3.BackColor = Color.WhiteSmoke;

            }
        }

        private void panel26_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void panel6_Click(object sender, EventArgs e)
        {
            if (GlowESP_Team == 0)
            {
                //Checked On
                GlowESP_Team = 1;
                panel6.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel6.BackColor = Color.FromArgb(255, 253, 232);
                panel2.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (GlowESP_Team == 1)
            {
                //Checked Off
                GlowESP_Team = 0;
                panel6.BackgroundImage = base.BackgroundImage;
                panel6.BackColor = Color.WhiteSmoke;
                panel2.BackColor = Color.WhiteSmoke;

            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void panel36_Click(object sender, EventArgs e)
        {
            if (OtherCheckValue == 0)
            {
                //Checked On
                OtherCheckValue = 1;
                panel36.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel36.BackColor = Color.FromArgb(255, 253, 232);
                panel35.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (OtherCheckValue == 1)
            {
                //Checked Off
                OtherCheckValue = 0;
                panel36.BackgroundImage = base.BackgroundImage;
                panel36.BackColor = Color.WhiteSmoke;
                panel35.BackColor = Color.WhiteSmoke;

            }
        }
        
        private void panel34_Click(object sender, EventArgs e)
        {
            if (Other_NoFlash == 0)
            {
                //Checked On
                Other_NoFlash = 1;
                panel34.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel34.BackColor = Color.FromArgb(255, 253, 232);
                panel33.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (Other_NoFlash == 1)
            {
                //Checked Off
                Other_NoFlash = 0;
                panel34.BackgroundImage = base.BackgroundImage;
                panel34.BackColor = Color.WhiteSmoke;
                panel33.BackColor = Color.WhiteSmoke;

            }
        }

        private void panel34_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void panel32_Click(object sender, EventArgs e)
        {
            if (Other_Radar == 0)
            {
                //Checked On
                Other_Radar = 1;
                panel32.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel32.BackColor = Color.FromArgb(255, 253, 232);
                panel31.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (Other_Radar == 1)
            {
                //Checked Off
                Other_Radar = 0;
                panel32.BackgroundImage = base.BackgroundImage;
                panel32.BackColor = Color.WhiteSmoke;
                panel31.BackColor = Color.WhiteSmoke;

            }
        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void panel22_Click(object sender, EventArgs e)
        {
            if (BunnyhopCheckValue == 0)
            {
                //Checked On
                BunnyhopCheckValue = 1;
                panel22.BackgroundImage = CSimple.Properties.Resources.baseline_done_black_48dp;
                panel22.BackColor = Color.FromArgb(255, 253, 232);
                panel20.BackColor = Color.FromArgb(255, 253, 232);
            }
            else if (BunnyhopCheckValue == 1)
            {
                //Checked Off
                BunnyhopCheckValue = 0;
                panel22.BackgroundImage = base.BackgroundImage;
                panel22.BackColor = Color.WhiteSmoke;
                panel20.BackColor = Color.WhiteSmoke;

            }
        }

        private void bunifuCustomLabel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
