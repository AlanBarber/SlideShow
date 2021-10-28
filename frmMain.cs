using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SlideShow
{
    public partial class frmMain : Form
    {
        private bool formFullscreen = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadRandomPicture();
            timer.Enabled = true;
            timer.Interval = 60000;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            pictureBox.Top = 0;
            pictureBox.Left = 0;
            pictureBox.Width = this.Width;
            pictureBox.Height = this.Height;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            loadRandomPicture();
        }

        private void loadRandomPicture()
        {
            var extensions = new string[] { ".bmp", ".png", ".jpg", ".jpeg", ".gif" };
            var di = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower())).ToList();
            if (rgFiles.Any())
            {
                Random R = new Random();
                var file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                pictureBox.Load(file);
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!formFullscreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;
            }

            this.formFullscreen = !this.formFullscreen;
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Space:
                    timer.Enabled = !timer.Enabled;
                    this.Text = timer.Enabled ? "SlideShow" : "SlideShow - Paused";
                    break;
                case Keys.Right:
                    loadRandomPicture();
                    this.timer.Stop();
                    this.timer.Start();
                    break;
            }
        }
    }
}
