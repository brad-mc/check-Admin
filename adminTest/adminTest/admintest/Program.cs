using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net;
using System.Management;

namespace adminTest
{
    class Program
    {
        string LoggedinUser;
        static void Main(string[] args)
        {

            /*     //searchGroup.addUser("is-usd-0284", "Administrators", "bmcleish");
               string host = "is-usd-0284";
               string message;
               message = searchGroup.PingHost(host);
               IPHostEntry ip = Dns.GetHostEntry(host);
                string cpuName;
                 string groupName;

                 Console.WriteLine("Enter cpu Name");
                 cpuName = Console.ReadLine();
                 groupName = Console.ReadLine();
                 List<String> users = new List<string>();
                 users = searchGroup.groupCheck(cpuName, groupName);
                 foreach (string user in users)
                 {
                     Console.WriteLine(user);
                 }

                 Console.ReadLine();*/
            try
            {
                ManagementScope scope = new ManagementScope("\\\\is-usd-0289\\root\\cimv2");
                scope.Connect();

                /* ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

                 ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                 ManagementObjectCollection queryCollection = searcher.Get();
                 foreach (ManagementObject m in queryCollection)
                 {
                     Console.WriteLine("Computer Name: {0}", m["csname"]);
                     Console.WriteLine("Windows Directory : {0}", m["WindowsDirectory"]);
                     Console.WriteLine("Operating System: {0}", m["Caption"]);
                     Console.Write("Version: {0}", m["Version"]);
                     Console.WriteLine("Manufacturer : {0}", m["Manufacturer"]);
                     Console.ReadLine();*/

                ObjectQuery Query = new ObjectQuery("SELECT LogonId  FROM Win32_LogonSession Where LogonType=2");
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    Console.WriteLine("{0,-35} {1,-40}", "LogonId", WmiObject["LogonId"]);// String
                    ObjectQuery LQuery = new ObjectQuery("Associators of {Win32_LogonSession.LogonId=" + WmiObject["LogonId"] + "} Where AssocClass=Win32_LoggedOnUser Role=Dependent");
                    ManagementObjectSearcher LSearcher = new ManagementObjectSearcher(scope, LQuery);
                    foreach (ManagementObject LWmiObject in LSearcher.Get())
                    {
                        Console.WriteLine("{0,-35} {1,-40}", "Name", LWmiObject["Name"]);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(String.Format("Exception {0} Trace {1}", e.Message, e.StackTrace));
            }
        Console.WriteLine("Press Enter to exit");
        Console.Read();
            }
           
        }



            
    



        }
    





