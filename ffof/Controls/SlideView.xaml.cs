using ffof.Models;
using ffof.ViewModels;
using Xamarin.Forms;

namespace ffof.Controls
{
    public partial class SlideView : Grid
    {
        private bool _isActive;
        private bool _firstTime;
        private MainViewModel vm;
        public SlideView()
        {
            _firstTime = true;
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent is CarouselView carouselView)
            {
                vm = carouselView.BindingContext as MainViewModel;
                carouselView.Scrolled += CarouselViewControlOnScrolled;
                carouselView.PositionChanged += CarouselViewControlOnPositionSelected;
            }
        }

        private void CarouselViewControlOnScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (_firstTime)
            {
                _firstTime = false;
                return;
            }

            if (_isActive)
            {
                pcView.Margin = new Thickness(20, 0);
            }
            else
            {
                pcView.Margin = new Thickness(20);
            }
        }

        private void CarouselViewControlOnPositionSelected(object sender, PositionChangedEventArgs e)
        {
            _isActive = e.CurrentPosition == vm.Brands.IndexOf(BindingContext as BrandModel);
        }
    }
}
