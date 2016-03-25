using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Media;

namespace tg2016_useless
{
    public class SysTrayApp : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        private DesktopCleaner desktopCleaner;


        [STAThread]
        static void Main()
        {
            Application.Run(new SysTrayApp());
        }


        public SysTrayApp()
        {
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("About", OnAbout);
            trayMenu.MenuItems.Add("Exit", OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Desktop Cleaner";
            trayIcon.Icon = tg2016_useless.Properties.Resources.star_black;

            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            desktopCleaner = new DesktopCleaner();
        }


        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            base.OnLoad(e);
        }


        private void OnAbout(object sender, EventArgs e)
        {
            MessageBox.Show("Desktop Cleaner - TG 2016 Useless contribution by #MooG");
        }


        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }


        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }
    }
}
