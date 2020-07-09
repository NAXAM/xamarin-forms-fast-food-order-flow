using System;
using System.ComponentModel;
using System.Threading;
using Xamarin.Forms;
using PropertyChanged;
using System.Threading.Tasks;

namespace ffof.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class FinishViewModel
    {
        public FinishViewModel(INavigation navigation) { }
        public bool IsProcessing { get; private set; }

        public async Task Process()
        {
            IsProcessing = true;
            await Task.Delay(3000);
            IsProcessing = false;
        }
    }
}
