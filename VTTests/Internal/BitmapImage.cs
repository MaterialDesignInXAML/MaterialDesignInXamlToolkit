using System.IO;
using System.Threading.Tasks;
using Google.Protobuf;

namespace VTTests.Internal
{
    internal class BitmapImage : IImage
    {
        private ByteString Data { get; }

        public BitmapImage(ByteString data) => Data = data ?? throw new System.ArgumentNullException(nameof(data));

        public Task Save(Stream stream)
        {
            Data.WriteTo(stream);
            return Task.CompletedTask;
        }
    }
}
