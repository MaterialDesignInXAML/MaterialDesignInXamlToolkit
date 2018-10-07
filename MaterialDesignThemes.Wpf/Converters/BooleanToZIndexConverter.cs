namespace MaterialDesignThemes.Wpf.Converters
{
    public sealed class BooleanToZIndexConverter : BooleanConverter<int>
    {
        public BooleanToZIndexConverter() : base(2, 0) { }
    }
}
