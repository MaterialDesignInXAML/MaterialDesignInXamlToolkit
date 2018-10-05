using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialDesignColors
{
    public interface ISwatch
    {
        string Name { get; }
        IEnumerable<CodeHue> Hues { get; }
    }
}
