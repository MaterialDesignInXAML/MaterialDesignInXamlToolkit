namespace MaterialDesignDemo.Domain
{
    public class SlidersViewModel : ViewModelBase
    {
        public SliderViewModel DiscreteHorizontal { get; } = new();
        public SliderViewModel DiscreteVertical { get; } = new() { Maximum = 100000, TickFrequency = 10000, Value = 50000 };
    }

    public class SliderViewModel : ViewModelBase
    {
        private double _minimum;
        private double _maximum = 100.0;
        private double _tickFrequency = 10.0;
        private double _value = 50.0;

        public double Minimum
        {
            get => _minimum;
            set => SetProperty(ref _minimum, value);
        }

        public double Maximum
        {
            get => _maximum;
            set => SetProperty(ref _maximum, value);
        }

        public double TickFrequency
        {
            get => _tickFrequency;
            set => SetProperty(ref _tickFrequency, value);
        }

        public double Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
