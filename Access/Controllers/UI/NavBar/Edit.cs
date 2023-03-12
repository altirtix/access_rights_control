using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Access.Controllers.UI.NavBar
{
    public static class Edit
    {
        public static void Clear(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                {
                    ((TextBox)obj).Text = null;
                }
                if (obj is CheckBox)
                {
                    ((CheckBox)obj).IsChecked = false;
                }
                if (obj is RadioButton)
                {
                    ((RadioButton)obj).IsChecked = false;
                }
                Clear(VisualTreeHelper.GetChild(obj, i));
            }
        }
        public static void Block(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                {
                    ((TextBox)obj).IsEnabled = false;
                }
                if (obj is CheckBox)
                {
                    ((CheckBox)obj).IsEnabled = false;
                }
                if (obj is RadioButton)
                {
                    ((RadioButton)obj).IsEnabled = false;
                }
                Block(VisualTreeHelper.GetChild(obj, i));
            }
        }
        public static void Unblock(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                {
                    ((TextBox)obj).IsEnabled = true;
                }
                if (obj is CheckBox)
                {
                    ((CheckBox)obj).IsEnabled = true;
                }
                if (obj is RadioButton)
                {
                    ((RadioButton)obj).IsEnabled = true;
                }
                Unblock(VisualTreeHelper.GetChild(obj, i));
            }
        }
    }
}
