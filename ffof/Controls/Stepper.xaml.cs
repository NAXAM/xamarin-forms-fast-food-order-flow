using Xamarin.Forms;
using PropertyChanged;
using System.Windows.Input;

namespace ffof.Controls
{
    [AddINotifyPropertyChangedInterface]
    public class StepperViewModel
    {
        private const int MaxVolume = 99;
        private const int MinVolume = 1;
        public ICommand MinusCommand { get; set; }
        public ICommand PlusCommand { get; set; }
        public StepperViewModel()
        {
            MinusCommand = new Command<Label>(param =>
            {
                
                int currentVolume = int.Parse((param as Label).Text);
                if (currentVolume == MinVolume)
                {
                    return;
                }
                (param as Label).Text = (currentVolume - 1).ToString();
            });

            PlusCommand = new Command<Label>(param =>
            {
                int currentVolume = int.Parse((param as Label).Text);
                if (currentVolume == MaxVolume)
                {
                    return;
                }
                (param as Label).Text = (currentVolume + 1).ToString();
            });
        }
    }

    public partial class Stepper : ContentView
    {
        public Stepper()
        {
            this.BindingContext = new StepperViewModel();

            InitializeComponent();
        }
    }
}
