using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Collections;
using System.DirectoryServices.AccountManagement;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Net;
using System.Net.Sockets;


namespace adminTest
{
    class searchGroup
    {
        
        public static List<String> groupCheck(string cpuName, string groupName)
        {
            int i = -1;

            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + cpuName);
            DirectoryEntry group = localMachine.Children.Find(groupName, "group");
            List<String> users = new List<string>();
            object members = group.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                i = i + 1;
                DirectoryEntry member = new DirectoryEntry(groupMember);
                users.Add((member.Name).ToString());
                
            }
            return users;
            
        }

        public static List<String> addUser(string cpuName, string groupName, string username)
        {
            
            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + cpuName);
            DirectoryEntry group = localMachine.Children.Find(groupName, "group");
            List<String> users = new List<string>();
            group.Invoke("Add", new object[] {"WinNT://ED/" + username});
            group.CommitChanges();
            users = groupCheck(cpuName, groupName);
            return users;


           
        }

        public static string PingHost(string host)
        {
            string returnMessage = string.Empty;
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
                    PingReply pingReply = ping.Send(ipa, 1000, buffer, pingOptions);
                    if (!(pingReply == null))
                    {
                        switch (pingReply.Status)
                        {

                            case IPStatus.Success:
                                returnMessage = string.Format("Reply from {0}: bytes={1} time={2}ms TTL={3}", pingReply.Address, pingReply.Buffer.Length, pingReply.RoundtripTime, pingReply.Options.Ttl);
                                break;
                            case IPStatus.TimedOut:
                                returnMessage = "Connection has timed out....";
                                break;
                            default:
                                returnMessage = string.Format("Ping failed: {0}", pingReply.Status.ToString());
                                break;
                        }

                    }
                    else
                        returnMessage = "Connection failed for an unknown reason....";

                }
                catch (PingException ex)
                {
                    returnMessage = string.Format("Connection Error: {0}", ex.Message);
                }
                catch (SocketException ex)
                {
                    returnMessage = string.Format("Connection Error: {0}", ex.Message);
                }
            }


            return returnMessage;



        }

       
    }
}
