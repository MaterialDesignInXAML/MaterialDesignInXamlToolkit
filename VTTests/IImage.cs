using System.IO;
using System.Threading.Tasks;

namespace VTTests
{
    public interface IImage
    {
        Task Save(Stream stream);
    }
}
