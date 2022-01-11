using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LeaveOnlineMobile.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ProjectSerial))]
namespace LeaveOnlineMobile.Droid
{
    public class ProjectSerial : InterfaceSerial
    {
        public string GetSerial()
        {
            var serial = Android.Provider.Settings.Secure.GetString(Forms.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            return serial;
        }
    }

}