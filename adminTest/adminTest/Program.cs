﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Collections;
namespace adminTest
{
    class Program
    {
        static void Main(string[] args)
        {
             
            //searchGroup.addUser("is-usd-0284", "Administrators", "bmcleish");
            
            
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

            Console.ReadLine();
            
            
            
           
            
        }
    }
}


