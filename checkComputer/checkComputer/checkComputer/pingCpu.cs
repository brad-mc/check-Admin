using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Net;
using System.Net.Sockets;


namespace checkComputer
{
    class pingCpu
    {
        public static bool PingHost (string host)
        {
            string returnMessage = string.Empty;
            bool online = false; 
            IPAddress ipa;
            IPHostEntry ip = Dns.GetHostEntry(host);
            ipa = ip.AddressList[0];
            PingOptions pingOptions = new PingOptions(128, true);

            Ping ping = new Ping();

            byte[] buffer = new byte[32];

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    PingReply pingReply = ping.Send(ipa,1000,buffer,pingOptions);
                    if (!(pingReply == null))
                    {
                        switch (pingReply.Status)
                        {

                            case IPStatus.Success:
                                returnMessage = string.Format("Reply from {0}: bytes={1} time={2}ms TTL={3}", pingReply.Address, pingReply.Buffer.Length, pingReply.RoundtripTime, pingReply.Options.Ttl);
                                online = true;
                                break;
                            case IPStatus.TimedOut:
                                returnMessage = "Connection has timed out....";
                                online = false;
                                break;
                            default:
                                returnMessage = string.Format("Ping failed: {0}", pingReply.Status.ToString());
                                online = false;
                                break;
                        }

                    }
                    

                }
                catch (PingException ex)
                {
                    returnMessage = string.Format("Connection Error: {0}", ex.Message);
                    online = false;
                }
                catch (SocketException ex)
                {
                    returnMessage = string.Format("Connection Error: {0}", ex.Message);
                    online = false;
                }
            }


            return online;
 


        }
    }
}
