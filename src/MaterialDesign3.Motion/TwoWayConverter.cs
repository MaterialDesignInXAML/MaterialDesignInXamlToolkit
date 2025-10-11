namespace MaterialDesignThemes.Motion;

public static class TwoWayConverter
{
    public static ITwoWayConverter<T, V> Create<T, V>(Func<T, V> convertToVector, Func<V, T> convertFromVector)
        where V : AnimationVector =>
        new TwoWayConverterImpl<T, V>(convertToVector ?? throw new ArgumentNullException(nameof(convertToVector)),
            convertFromVector ?? throw new ArgumentNullException(nameof(convertFromVector)));

    public static ITwoWayConverter<float, AnimationVector1D> Float { get; } =
        Create<float, AnimationVector1D>(value => new AnimationVector1D(value), vector => vector.Value);

    public static ITwoWayConverter<int, AnimationVector1D> Int { get; } =
        Create<int, AnimationVector1D>(value => new AnimationVector1D(value), vector => (int)Math.Round(vector.Value));

    private sealed class TwoWayConverterImpl<T, V> : ITwoWayConverter<T, V>
        where V : AnimationVector
    {
        public TwoWayConverterImpl(Func<T, V> convertToVector, Func<V, T> convertFromVector)
        {
            ConvertToVector = convertToVector;
            ConvertFromVector = convertFromVector;
        }

        public Func<T, V> ConvertToVector { get; }

        public Func<V, T> ConvertFromVector { get; }
    }
}
