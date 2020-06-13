using System.ComponentModel;
using ffof.ViewModels;
using Xamarin.Forms;

namespace ffof
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel(Navigation);
        }
    }
}
