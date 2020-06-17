using System;
using FFImageLoading.Forms;
using ffof.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ffof
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            CachedImage.FixedOnMeasureBehavior = true;
            CachedImage.FixedAndroidMotionEventHandler = true;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
