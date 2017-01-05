using Android.App;
using Android.Views;
using Android.OS;
using Com.OneSignal;

namespace push_onesignal.Droid
{
	[Activity (MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            OneSignal.NotificationOpened NotificationOpenedDelegate = delegate (OSNotificationOpenedResult result)
            {
                try
                {
                    var builder = new AlertDialog.Builder(Application.Context);

                    AlertDialog alertDialog = builder.Create();

                    alertDialog.Window.SetType(WindowManagerTypes.SystemAlert);
                    alertDialog.SetTitle(result.notification.payload.title);
                    alertDialog.SetMessage(result.notification.payload.body);
                    alertDialog.SetButton(-1, "Ok", delegate { });
                    alertDialog.SetCancelable(true);
                    alertDialog.Show();
                }
                catch
                {
                    // Catch the exception
                }
            };

            OneSignal.StartInit("ONESIGNAL_APP_ID", "GOOGLE_PROJECT_NUMBER")
                .InFocusDisplaying(OneSignal.OSInFocusDisplayOption.Notification)
                .HandleNotificationOpened(NotificationOpenedDelegate)
                .EndInit();
        }
    }
}


