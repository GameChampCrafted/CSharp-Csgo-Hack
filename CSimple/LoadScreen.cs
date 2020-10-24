using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSimple
{
    public partial class LoadScreen : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        //Classes Include
        CSimple.Options.Startup.Startup Process = new CSimple.Options.Startup.Startup();
        //

        public LoadScreen()
        {

            InitializeComponent();
            Thread.Sleep(1000);
        }

        public void startup()
        {




        }

        private void LoadScreen_Load(object sender, EventArgs e)
        {
            
        }
        UserInterface UI = new UserInterface();

        private void button1_Click(object sender, EventArgs e)
        {
 
        }

        int Loaded = 0;
        
        private void Counter_Tick(object sender, EventArgs e)
        {
            //Load Items
            UI.startup();
            Process.LOAD();
            //
            if (bunifuCircleProgressbar1.Value == 99)
            {
                UI.last();
                Counter.Enabled = false;
                this.Hide();
                UI.ShowDialog();
                bunifuCircleProgressbar1.Value = 0;
                this.Close();
                
            }

            Loaded += 1;
            bunifuCircleProgressbar1.Value = Loaded;


        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
