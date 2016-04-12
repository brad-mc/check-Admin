using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Reflection;
using System.IO;

namespace checkComputer
{
    class Poweshell
    {
        public static Collection<PSObject> checkPermissions(string cpuName, string type) 
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                string result;
                // This code reads the file in the recources. 
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "checkComputer.Resources.checkAdmin.ps1";
                var test = assembly.GetManifestResourceNames();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                PowerShellInstance.AddScript(result);
                PowerShellInstance.AddParameter("ComputerName", cpuName);
                PowerShellInstance.AddParameter("Type", type);
                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
                return PSOutput;
            }
        }
        public static Collection<PSObject> addPermissions(string cpuName, string type, string user)
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript("param([string] $ComputerName, [string] $Type, [string] $User );" + "$group = [ADSI]('WinNT://'+$ComputerName+'/'+$Type+',group');" + "@($group.Invoke('members'))|foreach {$_.GetType().InvokeMember('Name','GetProperty',$null,$_,$null);"+"if ($admins.Contains($User)){'This user is already an admin'}else {Try {$group.add('WinNT://$env:USERDNSDOMAIN/$User,user')'User successfully added'}Catch{'An error has occured, please make sure the user exsists'}}");
                PowerShellInstance.AddParameter("ComputerName", cpuName);
                PowerShellInstance.AddParameter("Type", type);
                PowerShellInstance.AddParameter("User", user);
                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
                return PSOutput;
            }
        }
    }
}
