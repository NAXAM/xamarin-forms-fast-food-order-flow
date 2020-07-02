using Xamarin.Forms;
using PropertyChanged;
using System.Windows.Input;

namespace ffof.Controls
{
    [AddINotifyPropertyChangedInterface]
    public class StepperViewModel
    {
        public int CurrentValue { get; set; }
        public ICommand MinusCommand { get; set; }
        public ICommand PlusCommand { get; set; }
        public StepperViewModel(int value)
        {
            CurrentValue = value;

            MinusCommand = new Command<string>(param =>
            {
                CurrentValue = int.Parse(param) - 1;
            });

            PlusCommand = new Command<string>(param =>
            {
                CurrentValue = int.Parse(param) + 1;
            });
        }
    }

    public partial class Stepper : ContentView
    {
        public Stepper()
        {
            this.BindingContext = new StepperViewModel(1);

            InitializeComponent();
        }
    }
}
