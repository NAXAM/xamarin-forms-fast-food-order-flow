using System.ComponentModel;
using ffof.Models;
using ffof.Services;
using ffof.ViewModels;
using Xamarin.Forms;

namespace ffof
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private ISystemStyleManager systemStyleManager;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel(Navigation);
            systemStyleManager = DependencyService.Get<ISystemStyleManager>();
        }

        private void lstLogos_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            systemStyleManager?.SetStatusBarColor((e.CurrentItem as BrandModel).ColorCode);
        }
    }
}
