using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Access.Controllers.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int numberOfObjects;
        public static int numberOfUsers;

        public static string User = String.Empty;
        public static string Object = String.Empty;

        public static List<Models.User> Users = new List<Models.User>();
        public static List<Models.Object> Objects = new List<Models.Object>();
        public static Dictionary<Models.Object, List<Models.User>> DAC = new Dictionary<Models.Object, List<Models.User>>();
        public static Dictionary<Models.Object, List<Models.User>> MAC = new Dictionary<Models.Object, List<Models.User>>();

        public static List<String> Perms = new List<String> { "read", "write", "grant" };

        public void DAC_Update()
        {
            DACOTextBox.Text = String.Empty;
            foreach (var entry in DAC)
            {
                DACOTextBox.Text += entry.Key.Name + Environment.NewLine;
                foreach (Models.User u in entry.Value)
                {
                    DACOTextBox.Text += u.DAC();
                }
                DACOTextBox.Text += Environment.NewLine;
            }
        }

        private void DAC_Generate(object sender, RoutedEventArgs e)
        {
            try
            {
                Users.Clear();
                Objects.Clear();
                DAC.Clear();

                numberOfObjects = Convert.ToInt16(DACNoOTextBox.Text);
                numberOfUsers = Convert.ToInt16(DACNoUTextBox.Text);

                for (int i = 0; i < numberOfObjects; i++) 
                {
                    Models.Object ob = new Models.Object();
                    Objects.Add(ob);
                }

                for (int i = 0; i < numberOfUsers; i++)
                {
                    Models.User us = new Models.User();
                    Users.Add(us);
                }

                for (int i = 0; i < Objects.Count; i++)
                {
                    List<Models.User> list = new List<Models.User>();
                    for (int j = 0; j < Users.Count; j++)
                    {
                        Models.User cu = new Models.User(Users[j]);
                        cu.SetPerm();
                        list.Add(cu);
                    }
                    DAC.Add(Objects[i], list);
                }

                foreach (Models.User us in Users)
                {
                    DACUComboBox.Items.Add(us.Name);
                    DACU2ComboBox.Items.Add(us.Name);
                }

                foreach (Models.Object ob in Objects)
                {
                    DACOComboBox.Items.Add(ob.Name);
                    DACO2ComboBox.Items.Add(ob.Name);
                }

                foreach (string s in Perms)
                {
                    DACPCombobox.Items.Add(s);
                }

                DAC_Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message");
            }
        }

        private void DAC_SignIn(object sender, RoutedEventArgs e)
        {
            User = Convert.ToString(DACUComboBox.SelectedItem);
            DACSULabel.Content = User;
            MessageBox.Show(this, "Success", "Message");
        }
        private void DAC_SignOut(object sender, RoutedEventArgs e)
        {
            User = null;
            Object = null;
            DACSULabel.Content = "Select User";
            DACSOLabel.Content = "Select object";
            MessageBox.Show(this, "Success", "Message");
        }
        private void DAC_Read(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Object = Convert.ToString(DACOComboBox.SelectedItem);
            foreach (var entry in DAC)
            {
                if (entry.Key.Name == Object)
                {
                    foreach (Models.User u in entry.Value)
                    {
                        if (u.isRead == true && u.Name == User)
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (flag)
            {
                DACSOLabel.Content = Object;
                MessageBox.Show(this, "Success", "Message");
            }
            else
            {
                MessageBox.Show(this, "Error", "Message");
            }
        }

        private void DAC_Write(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Object = Convert.ToString(DACOComboBox.SelectedItem);
            foreach (var entry in DAC)
            {
                if (entry.Key.Name == Object)
                {
                    foreach (Models.User u in entry.Value)
                    {
                        if (u.isWrite == true && u.Name == User)
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (flag)
            {
                DACSOLabel.Content = Object;
                MessageBox.Show(this, "Success", "Message");
            }
            else
            {
                MessageBox.Show(this, "Error", "Message");
            }
        }

        private void DAC_Set(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            string SU = Convert.ToString(DACU2ComboBox.SelectedItem);
            string SO = Convert.ToString(DACO2ComboBox.SelectedItem);
            string perm = Convert.ToString(DACPCombobox.SelectedItem);

            foreach (var entry in DAC)
            {
                if (entry.Key.Name == Object)
                {
                    foreach (Models.User u in entry.Value)
                    {
                        if (u.isWrite == true && u.Name == User)
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (flag)
            {
                foreach (var entry in DAC)
                {
                    if (entry.Key.Name == SO)
                    {
                        foreach (Models.User u in entry.Value)
                        {
                            if (u.Name == SU)
                            {
                                if (perm == "read")
                                {
                                    u.isRead = true;
                                }
                                if (perm == "write")
                                {
                                    u.isWrite = true;
                                }
                                if (perm == "grant")
                                {
                                    u.isGrant = true;
                                }
                            }
                        }
                    }
                }
                MessageBox.Show(this, "Success", "Message");
            }
            else
            {
                MessageBox.Show(this, "Error", "Message");
            }
            DAC_Update();
        }

        private void DAC_Unset(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            string SU = Convert.ToString(DACU2ComboBox.SelectedItem);
            string SO = Convert.ToString(DACO2ComboBox.SelectedItem);
            string perm = Convert.ToString(DACPCombobox.SelectedItem);

            foreach (var entry in DAC)
            {
                if (entry.Key.Name == Object)
                {
                    foreach (Models.User u in entry.Value)
                    {
                        if (u.isWrite == true && u.Name == User)
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (flag)
            {
                foreach (var entry in DAC)
                {
                    if (entry.Key.Name == SO)
                    {
                        foreach (Models.User u in entry.Value)
                        {
                            if (u.Name == SU)
                            {
                                if (perm == "read")
                                {
                                    u.isRead = false;
                                }
                                if (perm == "write")
                                {
                                    u.isWrite = false;
                                }
                                if (perm == "grant")
                                {
                                    u.isGrant = false;
                                }
                            }
                        }
                    }
                }
                MessageBox.Show(this, "Success", "Message");
            }
            else
            {
                MessageBox.Show(this, "Error", "Message");
            }
            DAC_Update();
        }

        public void MAC_Update()
        {
            MACOTextBox.Text = String.Empty;
            foreach (var entry in MAC)
            {
                MACOTextBox.Text += entry.Key.ToString() + Environment.NewLine;
                foreach (Models.User u in entry.Value)
                {
                    MACOTextBox.Text += u.MAC();
                }
                MACOTextBox.Text += Environment.NewLine;
            }
        }

        private void MAC_Generate(object sender, RoutedEventArgs e)
        {
            try
            {
                Users.Clear();
                Objects.Clear();
                MAC.Clear();

                numberOfObjects = Convert.ToInt16(MACNoOTextBox.Text);
                numberOfUsers = Convert.ToInt16(MACNoUTextBox.Text);

                for (int i = 0; i < numberOfObjects; i++)
                {
                    Models.Object ob = new Models.Object();
                    Objects.Add(ob);
                }

                for (int i = 0; i < numberOfUsers; i++)
                {
                    Models.User us = new Models.User();
                    Users.Add(us);
                }

                for (int i = 0; i < Objects.Count; i++)
                {
                    List<Models.User> list = new List<Models.User>();
                    for (int j = 0; j < Users.Count; j++)
                    {
                        Models.User cu = new Models.User(Users[j]);
                        cu.SetPerm();
                        list.Add(cu);
                    }
                    MAC.Add(Objects[i], list);
                }

                foreach (Models.User us in Users)
                {
                    MACUComboBox.Items.Add(us.Name);
                }

                foreach (Models.Object ob in Objects)
                {
                    MACOComboBox.Items.Add(ob.Name);
                }

                MAC_Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message");
            }
        }

        private void MAC_SignIn(object sender, RoutedEventArgs e)
        {
            User = Convert.ToString(MACUComboBox.SelectedItem);
            MACSULabel.Content = User;
            MessageBox.Show(this, "Success", "Message");
        }
        private void MAC_SignOut(object sender, RoutedEventArgs e)
        {
            User = null;
            Object = null;
            MACSULabel.Content = "Select User";
            MACSOLabel.Content = "Select object";
            MessageBox.Show(this, "Success", "Message");
        }

        private void MAC_Access(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Object = Convert.ToString(MACOComboBox.SelectedItem);
            foreach (var entry in MAC)
            {
                if (entry.Key.Name == Object)
                {
                    foreach (Models.User u in entry.Value)
                    {
                        if (u.Name == User)
                        {
                            if (u.isAccess == entry.Key.Right)
                            {
                                flag = true;
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                MACSOLabel.Content = Object;
                MessageBox.Show(this, "Success", "Message");
            }
            else
            {
                MessageBox.Show(this, "Error", "Message");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            DateMenuItem.Header = Controllers.UI.NavBar.Info.GetDate();
            OSMenuItem.Header = Controllers.UI.NavBar.Info.GetOS();
            LocationMenuItem.Header = Controllers.UI.NavBar.Info.GetLocation();
            WANIPMenuItem.Header = Controllers.UI.NavBar.Info.GetWANIP();
            LANIPMenuItem.Header = Controllers.UI.NavBar.Info.GetLANIP();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeMenuItem.Header = Controllers.UI.NavBar.Info.GetTime();
            StopwatchMenuItem.Header = Controllers.UI.NavBar.Info.GetStopwatch();
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.File.OpenFile();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.File.SaveFile();
        }

        private void UnblockMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Edit.Unblock(this);
        }

        private void BlockMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Edit.Block(this);
        }

        private void ClearMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Edit.Clear(this);
        }

        private void RestartMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Tools.Restart();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Controllers.UI.NavBar.Tools.Exit();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, Controllers.UI.NavBar.Help.About(), "Message");
        }
    }
}
