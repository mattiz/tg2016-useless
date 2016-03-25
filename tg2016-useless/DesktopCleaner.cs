using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace tg2016_useless
{
    class DesktopCleaner
    {
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        public DesktopCleaner()
        {
            var t = new Thread(() =>
            {

                while (true)
                {
                    Thread.Sleep(5000);

                    IntPtr fw = GetForegroundWindow();

                    String windowTitle = getWindowTitle(fw);

                    Console.WriteLine("Foreground Window: " + windowTitle);


                    if (windowTitle.Length > 0)
                    {
                        RECT rct = new RECT();
                        GetWindowRect(fw, ref rct);

                        var p1 = new Point(Cursor.Position.X, Cursor.Position.Y);
                        var p2 = new Point(rct.Right - 90, rct.Top + 5);

                        animateMouse(p1, p2);

                        Thread.Sleep(500);

                        ShowWindow(fw, SW_MINIMIZE);
                    }
                }
            });


            t.Start();
        }


        private String getWindowTitle( IntPtr handle )
        {
            int capacity = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder stringBuilder = new StringBuilder(capacity);
            GetWindowText(new HandleRef(this, handle), stringBuilder, stringBuilder.Capacity);

            return stringBuilder.ToString();
        }

        private void animateMouse( Point p1, Point p2 )
        {
            for (double i = 0; i < 100; i++)
            {
                double blend = i / 100;

                double x = p1.X + blend * (p2.X - p1.X);
                double y = p1.Y + blend * (p2.Y - p1.Y);

                int ix = (int)x;
                int iy = (int)y;

                Cursor.Position = new Point(ix, iy);
                Thread.Sleep(10);
            }
        }
    }
}
