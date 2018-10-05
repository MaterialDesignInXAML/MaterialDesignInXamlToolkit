using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignDemo
{
    class ColorToolViewModel
    {
        public IEnumerable<ISwatch> Swatches { get; } =  SwatchHelper.Swatches;
    }
}
