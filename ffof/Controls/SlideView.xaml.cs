using ffof.Models;
using ffof.ViewModels;
using Xamarin.Forms;

namespace ffof.Controls
{
    public partial class SlideView : Grid
    {
        private bool _isActive;
        private MainViewModel vm;
        public SlideView()
        {
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
            if (_isActive)
            {
                pcView.FadeTo((100 - e.HorizontalDelta) / 100);
                pcView.ScaleTo((100 - e.HorizontalDelta) / 100);
            }
            else
            {
                pcView.FadeTo(e.HorizontalDelta / 100);
                pcView.ScaleTo(e.HorizontalDelta / 100);
            }
        }

        private void CarouselViewControlOnPositionSelected(object sender, PositionChangedEventArgs e)
        {
            _isActive = e.CurrentPosition == vm.Brands.IndexOf(BindingContext as BrandModel);
        }
    }
}
