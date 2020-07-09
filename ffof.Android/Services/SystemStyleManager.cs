using Android.OS;
using Android.Views;
using ffof.Droid.Services;
using ffof.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SystemStyleManager))]
namespace ffof.Droid.Services
{
    public class SystemStyleManager : ISystemStyleManager
    {
        public SystemStyleManager()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = 0;
                });
            }
        }

        public void SetStatusBarColor(string hexColor, bool isAnimated = false)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var currentWindow = GetCurrentWindow();
                currentWindow?.SetStatusBarColor(global::Android.Graphics.Color.ParseColor(hexColor));
            }
        }

        Window GetCurrentWindow()
        {
            var window = CrossCurrentActivity.Current.Activity.Window;

            // clear FLAG_TRANSLUCENT_STATUS flag:
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }
    }
}
