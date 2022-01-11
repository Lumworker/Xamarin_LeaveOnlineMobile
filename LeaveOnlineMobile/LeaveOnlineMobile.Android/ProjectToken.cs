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

[assembly: Xamarin.Forms.Dependency(typeof(ProjectToken))]
namespace LeaveOnlineMobile.Droid
{
    class ProjectToken : InterfaceToken
    {
        public string AndroidToken = "";

        public void setToken(string token)
        {
            AndroidToken = token;
        }

        public string getToken()
        {
            string value = MainActivity.GetToken();

            return value;
        }
    }
}