using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;

namespace networkidentifier
{
    public partial class NetworkTool : Form
    {

        string macson = "";
        string pcname = "";
        string ip = "";
        string publicip = " ";
        string mac = "";

        public NetworkTool()
        {
            InitializeComponent();
            
            pcname = Environment.MachineName;
            ip = GetIp();            
            mac = GetMACAddress();            
            
            for (int i = 0; i < mac.Length; i++)
            {
                macson += mac.Substring(i, 1);
                if ((i + 1) % 2 == 0)
                {
                    macson += "-";
                }
            }
            macson = macson.Remove(macson.Length - 1);
            richTextBox2.Text = pcname;
            richTextBox3.Text = ip;
            richTextBox1.Text = macson;

            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox3.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox4.SelectionAlignment = HorizontalAlignment.Center;                        

        }        

        private void button1_Click(object sender, EventArgs e)
        {                                    
            string publicip = new WebClient().DownloadString("http://icanhazip.com");                       
            richTextBox4.Text = publicip;                            
        }

        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        public string GetIp()

        {

            string HostAdi = System.Net.Dns.GetHostName();

            IPHostEntry ipGiris = System.Net.Dns.GetHostEntry(HostAdi);

            IPAddress[] ipAdresleri = ipGiris.AddressList;

            return ipAdresleri[ipAdresleri.Length - 2].ToString();

        }

        public static string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(macson);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(pcname);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ip);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
            string publicip = new WebClient().DownloadString("http://icanhazip.com");
            Clipboard.SetText(publicip);
        }
    }

}
