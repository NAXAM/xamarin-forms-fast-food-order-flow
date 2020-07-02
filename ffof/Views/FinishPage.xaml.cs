using Xamarin.Forms;
using ffof.ViewModels;
using ffof.Services;

namespace ffof.Views
{
    public partial class FinishPage : ContentPage
    {
        private ISystemStyleManager systemStyleManager;
        public FinishPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            systemStyleManager = DependencyService.Get<ISystemStyleManager>();
            systemStyleManager?.SetStatusBarColor(Color.Orange.ToHex(),false);
            BindingContext = new FinishViewModel(Navigation);
            Device.BeginInvokeOnMainThread(async() => { await (BindingContext as FinishViewModel).Process(); });
        }
    }
}
