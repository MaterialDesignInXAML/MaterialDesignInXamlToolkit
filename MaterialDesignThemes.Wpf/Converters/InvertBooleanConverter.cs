namespace MaterialDesignThemes.Wpf.Converters
{
    internal class InvertBooleanConverter : BooleanConverter<bool>
    {
        public InvertBooleanConverter()
            : base(false, true)
        {
        }
    }
}
