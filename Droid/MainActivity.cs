using Android.App;
using Android.Content.PM;
using Android.OS;

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
			LoadApplication(new App());
		}
	}
}
