using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pierwszeWiazanie
{
    public partial class NewPage1 : ContentPage
    {
        public NewPage1()
        {
            BindingContext = new BmiCalculatorViewModel();
            InitializeComponent();
        }
    }
    public class BmiCalculatorViewModel : INotifyPropertyChanged
    {
        private double Height { get; set; }
        private double Weight { get; set; }
        private double Bmi { get; }
        private string Category { get; }

        public double height
        {
            get => Height;
            set
            {
                Height = Math.Round(value, 2);
                OnPropertyChanged();
                OnPropertyChanged(nameof(bmi));
                OnPropertyChanged(nameof(category));
            }
        }
        public double weight
        {
            get => Weight;
            set
            {
                Weight = Math.Round(value, 2);
                OnPropertyChanged();
                OnPropertyChanged(nameof(bmi));
                OnPropertyChanged(nameof(category));
            }
        }
        public double bmi
        {
            get
            {
                if (height <= 0) return 0;
                return Math.Round(weight / (height * height), 2);
            }
        }
        public string category
        {
            get
            {
                if (bmi < 18.5) return "Niedowaga";
                else if (bmi < 24.9) return "Norma";
                else if (bmi < 29.9) return "Nadwaga";
                else return "Otyloœæ";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}