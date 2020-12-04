using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace ClickOperator
{
    public partial class form : Form
    {
        ClickOperator ope = new ClickOperator();

        public form()
        {
            InitializeComponent();
            t_ip.Text = "127.0.0.1";
            t_port1.Text = "1000";
            t_port2.Text = "2000";
        }

        private void b_listen_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress buf;
                if (IPAddress.TryParse(t_ip.Text, out buf)) ope.ip = t_ip.Text;
                else
                {
                    MessageBox.Show("Invalid IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int port = int.Parse(t_port1.Text);
                if (port > 0 && port < 65536 && !ope.CheckPortUsing(port)) ope.cmdPort = port;
                else
                {
                    MessageBox.Show("Invalid Cmd Port.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                port = int.Parse(t_port2.Text);
                if (port > 0 && port < 65536 && !ope.CheckPortUsing(port)) ope.dataPort = port;
                else
                {
                    MessageBox.Show("Invalid Data Port.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ope.CmdListen();
                ope.DataListen();

            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        public void Error(Exception e)
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

        private void b_xy_Click(object sender, EventArgs e)
        {
            try
            {
                Cmd.EV_TYPE eventType = Cmd.EV_TYPE.C;
                ope.type = eventType;
                ope.CmdSend(ope.GenCmd(eventType));
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        private void b_text_Click(object sender, EventArgs e)
        {
            try
            {
                Cmd.EV_TYPE eventType = Cmd.EV_TYPE.I;
                ope.type = eventType;
                ope.text = t_text.Text;
                ope.CmdSend(ope.GenCmd(eventType));
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        private void p_ss_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (ope.cmdProc != null && !ope.cmdProc.HasExited) l_port1.ForeColor = Color.GreenYellow;
                else l_port1.ForeColor = Color.Black;
                if (ope.dataProc != null && !ope.dataProc.HasExited) l_port2.ForeColor = Color.GreenYellow;
                else l_port2.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                Error(ex);
            }

            if (File.Exists(ClickOperator.SS_NAME) && ope.received)
            {
                FileInfo file = new FileInfo(ClickOperator.SS_NAME);
                if (file.Length > 0)
                {
                    try
                    {
                        string displayFile = $"{DateTime.Now.ToString("yyyyMMdd-HHmmss")}_{ClickOperator.SS_NAME}";
                        if (File.Exists(ClickOperator.SS_NAME)) File.Move(ClickOperator.SS_NAME, displayFile);
                        p_ss.ImageLocation = displayFile;
                        ope.received = false;
                    }
                    catch (Exception ex)
                    {
                        Error(ex);
                    }
                }
            }
            ope.DataListen();
            GetMouseClick(e, 1);
        }

        private void p_ss_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GetMouseClick(e, 2);
        }

        private void GetMouseClick(MouseEventArgs e, int count)
        {
            try
            {
                ope.x = e.Location.X;
                ope.y = e.Location.Y;
                if (e.Button == MouseButtons.Right) ope.click = Cmd.EV_CLICK.R;
                else if (e.Button == MouseButtons.Left)
                {
                    if (count == 1) ope.click = Cmd.EV_CLICK.L;
                    else ope.click = Cmd.EV_CLICK.L2;
                }
                l_xy.Text = $"{ope.x}x{ope.y} {ope.click.ToString()}";
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ope.cmdProc != null)
                {
                    ope.cmdWriter.Close();
                    ope.cmdProc.Kill();
                }
            }
            catch (Exception ex)
            {
                Error(ex);
            }
            try
            {
                if (ope.dataProc != null) ope.dataProc.Kill();
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

    }

    public class ClickOperator
    {
        public const string CLIENT_FILENAME = "clicker.exe";
        public const string SS_NAME = "ss.jpg";

        public enum OS_TYPE { Windows, Linux };
        public Cmd.EV_TYPE type { set; get; }
        public Cmd.EV_CLICK click { set; get; }
        public bool received { set; get; } = false;
        public StreamWriter cmdWriter { set; get; } = null;
        public string os { set; get; } = string.Empty;
        public string nc { set; get; } = string.Empty;
        public string ip { set; get; } = string.Empty;
        public int cmdPort { set; get; }
        public int dataPort { set; get; }
        public Process cmdProc { set; get; } = null;
        public Process dataProc { set; get; } = null;
        public int x { set; get; }
        public int y { set; get; }
        public string text { set; get; } = string.Empty;

        public ClickOperator()
        {
            string osVer = Environment.OSVersion.ToString();
            if (!osVer.Contains("Windows") && osVer.Contains("Unix"))
            {
                os = OS_TYPE.Linux.ToString();
                nc = "nc";
            }
            else
            {
                os = OS_TYPE.Windows.ToString();
                nc = "nc64.exe";
            }
        }

        public void CmdListen()
        {
            try
            {
                this.cmdProc = new Process();
                if (this.cmdPort == 0 || CheckPortUsing(this.cmdPort)) return;

                //ProcessStartInfo proc = new ProcessStartInfo(this.nc, $"-nvlp {this.cmdPort}");
                ProcessStartInfo proc = new ProcessStartInfo("cmd.exe", $"{this.nc} -nvlp {this.cmdPort}");
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                proc.UseShellExecute = false;
                proc.RedirectStandardInput = true;
                proc.CreateNoWindow = true;
                this.cmdProc.StartInfo = proc;
                this.cmdProc.Start();
                this.cmdWriter = this.cmdProc.StandardInput;
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        public void DataListen()
        {
            try
            {
                this.dataProc = new Process();
                if (this.dataPort == 0 || CheckPortUsing(this.dataPort)) return;

                ProcessStartInfo proc = new ProcessStartInfo("cmd.exe", $"/c {this.nc} -nvlp {this.dataPort} > {SS_NAME}");
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                proc.UseShellExecute = false;
                proc.CreateNoWindow = true;
                this.dataProc.StartInfo = proc;
                this.dataProc.Start();
            }
            catch(Exception e)
            {
                Error(e);
            }
        }

        public bool CmdSend(string cmd)
        {
            try
            {
                if (this.cmdWriter == null) return false;
                if (this.cmdWriter.BaseStream.CanWrite) this.cmdWriter.WriteLine(cmd);
            }
            catch(Exception e)
            {
                Error(e);
                return false;
            }
            return true;
        }

        //true: the port is being used
        public bool CheckPortUsing(int port)
        {
            bool result = false;
            try
            {
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();

                foreach (IPEndPoint endpoint in tcpConnInfoArray)
                {
                    if (endpoint.Port == port)
                    {
                        result = true;
                        break;
                    }
                }
                this.received = true;
            }
            catch(Exception e)
            {
                Error(e);
            }
            return result;
        }

        public string GenCmd(Cmd.EV_TYPE eventType)
        {
            string result = $"{CLIENT_FILENAME} -e {this.ip} {this.dataPort}";
            try
            {
                Cmd cmd = new Cmd();
                cmd.type = eventType;
                string eventCmd = string.Empty;
                if (eventType == Cmd.EV_TYPE.C)
                {
                    cmd.x = this.x;
                    cmd.y = this.y;
                    cmd.click = this.click;
                    eventCmd = $"{cmd.type.ToString()}:{cmd.x},{cmd.y},{cmd.click.ToString()}";
                }
                else if (eventType == Cmd.EV_TYPE.I)
                {
                    cmd.text = this.text;
                    eventCmd = $"{cmd.type.ToString()}:{cmd.text}";
                }
                result = $"{result} {eventCmd}";
            }
            catch (Exception e)
            {
                Error(e);
            }
            return result;
        }

        public void Error(Exception e)
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

    public class Cmd
    {
        public enum EV_TYPE { C, I }    //C=Click, I=Input
        public enum EV_CLICK { L, R, L2 }   //L=Left, R=Right, L2=Left Double Click
        public EV_TYPE type { set; get; }
        public EV_CLICK click { set; get; }
        public int x { set; get; }
        public int y { set; get; }
        public string text { set; get; }
        public string Gen(Cmd cmd)
        {
            string result = string.Empty;
            try
            {
                if (cmd.type.Equals(Cmd.EV_TYPE.C.ToString())) result = $"{cmd.type.ToString()}:{cmd.x},{cmd.y},{cmd.click}";
                else if (cmd.type.Equals(Cmd.EV_TYPE.I.ToString())) result = $"{cmd.type.ToString()}:{cmd.text}";
            }
            catch (Exception){}
            return result;
        }
    }
}
