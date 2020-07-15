using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VTTests
{
    public static class VTMixins
    {
        public static async Task Save(this IImage image, string filePath)
        {
            await using var file = File.OpenWrite(filePath);
            await image.Save(file);
        }
    }
}
