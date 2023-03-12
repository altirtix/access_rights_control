using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace Access.Controllers.UI.NavBar
{
    public static class Info
    {
        public static string GetDate()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            return "Date: " + date.ToShortDateString();
        }

        public static string GetTime()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            return "Time: " + date.ToLongTimeString();
        }

        public static DateTime StartTime = DateTime.Now;

        public static string GetStopwatch()
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
            string stopWatch = string.Empty;
            stopWatch +=
            elapsed.Hours.ToString("00") + ":" +
            elapsed.Minutes.ToString("00") + ":" +
            elapsed.Seconds.ToString("00");
            return "Stopwatch: " + stopWatch;
        }

        public static string GetOS()
        {
            OperatingSystem os = Environment.OSVersion;
            return "OS: " + Convert.ToString(os);
        }
        public static string GetLocation()
        {
            return "No Location";
        }

        public static string GetLANIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return "LAN IP: " + ip.ToString();
                }
            }
            return "No adapters";
        }
        public static string GetWANIP()
        {
            using (WebClient client = new WebClient())
            {
                if (NetworkInterface.GetIsNetworkAvailable() &&
                     new Ping().Send(new IPAddress(new byte[] { 8, 8, 8, 8 }), 2000).Status == IPStatus.Success)
                {
                    var ip = client.DownloadString("https://ipinfo.io/ip");

                    return "WAN IP: " + ip.ToString().Replace("\n", "");
                }
            }
            return "No internet";
        }
    }
}
