using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using HockeyApp.Android;

namespace Spent.Droid
{
    [Activity(Label = "Spent", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			         
			Xamarin.Forms.Forms.Init(this, bundle);

            MobileCenter.Start(AppSettings.MobileCenterId,
                   typeof(Analytics), typeof(Crashes));

            LoadApplication(new App());
		}

        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, AppSettings.HockeyAppId);
        }
    }
}
