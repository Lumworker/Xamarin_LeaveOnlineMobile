using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Firebase.Iid;
using Firebase.Messaging;
using Xamarin.Forms;
using Android.Util;
using Android.Content;
using Android.Support.V4.App;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeaveOnlineMobile;

namespace LeaveOnlineMobile.Droid
{
    [Activity(Label = "Leave Online", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static string TokenDevice = "";
        public static MainActivity MainActivityInstance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //DependencyService.Register<InterfaceToken>();
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            IsPlayServicesAvaliable();
            MainActivityInstance = this;

            //Console.WriteLine($"Token :===================================================================");
            //#if DEBUG
            //            Task.Run(() =>
            //            {
            //                FirebaseInstanceId.Instance.DeleteInstanceId();
            //                Console.WriteLine("Force token: "+FirebaseInstanceId.Instance.Token);
            //            });


            //#endif
        }

        public static void SetToken(string token)
        {
            TokenDevice = token;
        }

        public static string GetToken()
        {
            return TokenDevice;
        }

        public bool IsPlayServicesAvaliable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode == ConnectionResult.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Service]
        [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
        public class MyFirebaseIIDService : FirebaseInstanceIdService
        {
            public override void OnTokenRefresh()
            {
                var refreshedToken = FirebaseInstanceId.Instance.Token;
                Console.WriteLine($"Token : {refreshedToken}");
                SendRegistrationToServerAsync(refreshedToken);
            }
            void SendRegistrationToServerAsync(string Token)
            {
               MainActivity.SetToken(Token);
            }
        }


        [Service]
        [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
        public class MyMessagingService : FirebaseMessagingService
        {
            private readonly string NOTIFICATION_CHANNEL_ID = "com.companyname.LeaveOnlineMobile";

            public override void OnMessageReceived(RemoteMessage message)
            {
                base.OnMessageReceived(message);

                if (!message.Data.GetEnumerator().MoveNext())
                {
                    SendNoti(message.GetNotification().Title, message.GetNotification().Body);
                }
                else
                {
                    SendNoti(message.Data);
                }
                //new NotificationHelper().CreateNotification(message.GetNotification().Title, message.GetNotification().Body);

            }

            private void SendNoti(IDictionary<string, string> data)
            {
                string title, body;

                data.TryGetValue("title", out title);
                data.TryGetValue("body", out body);

                SendNoti(title, body);
            }

            private void SendNoti(string title, string body)
            {
                NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Notification Channel",
                    Android.App.NotificationImportance.Default);
                    notificationChannel.Description = "Leave Online";
                    notificationChannel.EnableLights(true);
                    //notificationChannel.LightColor = Color.Blue;
                    notificationChannel.SetVibrationPattern(new long[] { 0, 1000, 500, 1000 });

                    notificationManager.CreateNotificationChannel(notificationChannel);
                }

                NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);

                notificationBuilder.SetAutoCancel(true)
                    .SetDefaults(-1)
                    .SetWhen(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                    .SetContentTitle(title)
                    .SetContentText(body)
                    .SetPriority((int)NotificationPriority.High)
                    .SetVisibility((int)NotificationVisibility.Public)
                    .SetSmallIcon(Resource.Drawable.icon)
                    .SetContentInfo("info");
                notificationManager.Notify(new Random().Next(), notificationBuilder.Build());
            }


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {


            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }



}