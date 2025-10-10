namespace MaterialColorUtilities;

public interface Quantizer
{
    QuantizerResult Quantize(int[] pixels, int maxColors);
}