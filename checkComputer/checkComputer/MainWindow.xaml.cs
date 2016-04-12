using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace checkComputer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cpuName;
        string type;
        string user;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            lstAdministrators.Items.Clear();
            lstRemoteDesktop.Items.Clear();
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                
                cpuName = txtCpuName.Text.ToString();
                type = "administrators";
                Collection<PSObject> PSOutput = Poweshell.checkPermissions(cpuName, type);

                



                

                foreach (PSObject outputitem in PSOutput)
                {
                    if (outputitem != null)
                    {
                        lstAdministrators.Items.Add(outputitem.ToString());
                    }
                }

                type = "remote desktop users";
                Collection<PSObject> PSOutput1 = Poweshell.checkPermissions(cpuName, type);
                foreach (PSObject outputitem in PSOutput1)
                {
                    if (outputitem != null)
                    {
                        lstRemoteDesktop.Items.Add(outputitem.ToString());
                    }
                }
                

            }
        }

        private void btnAddAdmin_Click(object sender, RoutedEventArgs e)
        {
            user = txtUUN.Text;
            type = "administrators";
            Collection<PSObject> PSOutput1 = Poweshell.addPermissions(cpuName, type, user);
        }
    }
}
