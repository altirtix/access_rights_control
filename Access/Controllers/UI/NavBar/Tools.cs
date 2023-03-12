using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Access.Controllers.UI.NavBar
{
    public static class Tools
    {
        public static void Restart() 
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
