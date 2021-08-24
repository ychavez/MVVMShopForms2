using Android.App;
using Firebase.Messaging;

namespace MVVMShopForms.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {

        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);
        }
    }
}