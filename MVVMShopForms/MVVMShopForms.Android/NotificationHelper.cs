using Android.App;
using Android.Content;
using Android.Media;
using AndroidX.Core.App;
using System;

namespace MVVMShopForms.Droid
{
    public class NotificationHelper
    {

        private Context mContext;
        private NotificationManager mNotificationManager;
        private NotificationCompat.Builder mBuilder;

        public NotificationHelper()
        {
            mContext = global::Android.App.Application.Context;
        }

        public void CreateNotification(string title, string message) 
        {
            try
            {
                var intent = new Intent(mContext, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra(title, message);
                var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.OneShot);


                /// crear las propiedades de la notificacion
                /// 
                var alarmAttributes = new AudioAttributes.Builder()
                    .SetContentType(AudioContentType.Sonification)
                    .SetUsage(AudioUsageKind.Notification).Build();

                mBuilder = new NotificationCompat.Builder(mContext);

                mBuilder.SetSmallIcon(Resource.Drawable.logo_aureapng);
               
                mBuilder.SetContentTitle(title)
                        .SetAutoCancel(true)
                        .SetContentText(message)
                        .SetPriority((int)NotificationPriority.High)
                        .SetVibrate(new long[3] { 10, 100, 10 })
                        .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                        .SetSmallIcon(Resource.Drawable.logo_aureapng)
                        .SetContentIntent(pendingIntent);

                NotificationManager notificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    NotificationImportance importance = global::Android.App.NotificationImportance.High;

                    NotificationChannel notificationChannel = new NotificationChannel("10023", title, importance);

                    notificationChannel.EnableLights(true);

                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId("10023");
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }  
                }
                notificationManager.Notify(0, mBuilder.Build());


            }
            catch (Exception)
            {

                throw;
            }
        
        }



    }
}