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
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;


namespace checkComputer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cpuName;
        string groupName;
        string username;
        List<String> aUsers = new List<string>();
        List<String> rUsers = new List<string>();
        bool online;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            lstAdministrators.Items.Clear();
            lstRemoteDesktop.Items.Clear();
            

            cpuName = txtCpuName.Text.ToString();
            groupName = "administrators";
            online = pingCpu.PingHost(cpuName);
            if (online == true)
            {
                lblHostStatus.Content = "Host Status: ONLINE";
            }
            else 
            {
                lblHostStatus.Content = "Host Status: OFFLINE";
            
            }

            aUsers = checkUser.groupCheck(cpuName, groupName);
            groupName = "Remote Desktop Users";
            rUsers = checkUser.groupCheck(cpuName, groupName);

            foreach (String user in aUsers)
            {
                lstAdministrators.Items.Add(user.ToString());

            }

            foreach (String user in rUsers)
            {
                lstRemoteDesktop.Items.Add(user.ToString());
            }











        }

        private void btnAddAdmin_Click(object sender, RoutedEventArgs e)
        {
            groupName = "administrators";
            username = txtUUN.Text.ToString();
            aUsers = checkUser.addUser(cpuName, groupName, username);
            lstAdministrators.Items.Clear();

            foreach (String user in aUsers)
            {
                lstAdministrators.Items.Add(user.ToString());

            }

        }

        private void btnAddRDU_Click(object sender, RoutedEventArgs e)
        {
            groupName = "Remote Desktop Users";
            username = txtUUN.Text.ToString();
            rUsers = checkUser.addUser(cpuName, groupName, username);
            lstRemoteDesktop.Items.Clear();

            foreach (String user in rUsers)
            {
                lstRemoteDesktop.Items.Add(user.ToString());

            }
        }

        private void btnRemoveAdmin_Click(object sender, RoutedEventArgs e)
        {
            groupName = "administrators";
            username = lstAdministrators.SelectedItem.ToString();
            aUsers = checkUser.removeUser(cpuName, groupName, username);
            lstAdministrators.Items.Clear();

            foreach (String user in aUsers)
            {
                lstAdministrators.Items.Add(user.ToString());

            }
        }

        private void btnRemoveRDU_Click(object sender, RoutedEventArgs e)
        {
            groupName = "remote desktop users";
            username = lstRemoteDesktop.SelectedItem.ToString();
            rUsers = checkUser.removeUser(cpuName, groupName, username);
            lstRemoteDesktop.Items.Clear();

            foreach (String user in rUsers)
            {
                lstRemoteDesktop.Items.Add(user.ToString());

            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtUUN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
    }

}

