using FFImageLoading.Forms;
using Xamarin.Forms;

namespace ffof.Controls
{
    public partial class SquareImage : CachedImage
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Device.BeginInvokeOnMainThread(() => { HeightRequest = Width; });
        }
    }
}
