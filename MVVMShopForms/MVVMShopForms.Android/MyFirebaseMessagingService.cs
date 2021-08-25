using Android.App;
using Firebase.Messaging;

namespace MVVMShopForms.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {

        public override void OnMessageReceived(RemoteMessage message)
        {

            var data = message.Data;
            base.OnMessageReceived(message);
            new NotificationHelper().CreateNotification(message.GetNotification().Title, message.GetNotification().Body);

        }
    }
}