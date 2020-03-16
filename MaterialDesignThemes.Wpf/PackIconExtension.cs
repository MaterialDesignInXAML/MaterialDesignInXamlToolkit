using System;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf
{
    [MarkupExtensionReturnType(typeof(PackIcon))]
    public class PackIconExtension : MarkupExtension
    {
        public PackIconExtension()
        { }

        public PackIconExtension(PackIconKind kind)
        {
            Kind = kind;
        }

        public PackIconExtension(PackIconKind kind, double size)
        {
            Kind = kind;
            Size = size;
        }

        [ConstructorArgument("kind")]
        public PackIconKind Kind { get; set; }

        [ConstructorArgument("size")]
        public double? Size { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var result = new PackIcon { Kind = Kind };

            if (Size.HasValue)
            {
                result.Height = Size.Value;
                result.Width = Size.Value;
            }

            return result;
        }
    }
}