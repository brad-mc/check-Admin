using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Runtime.InteropServices;
using System.Collections;


namespace checkComputer
{
    class checkUser
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
            group.Invoke("Add", new object[] { "WinNT://ED/" + username });
            group.CommitChanges();
            users = groupCheck(cpuName, groupName);
            return users;
        }

        public static List<String> removeUser(string cpuName, string groupName, string username)
        {

            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + cpuName);
            DirectoryEntry group = localMachine.Children.Find(groupName, "group");
            List<String> users = new List<string>();
            group.Invoke("Remove", new object[] { "WinNT://ED/" + username });
            group.CommitChanges();
            users = groupCheck(cpuName, groupName);
            return users;
        }
    }
}