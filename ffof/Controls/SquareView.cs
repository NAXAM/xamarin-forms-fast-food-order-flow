using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace ffof.Controls
{
    public partial class SquareView : PancakeView
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Device.BeginInvokeOnMainThread(() => { HeightRequest = Width; });
        }
    }
}
