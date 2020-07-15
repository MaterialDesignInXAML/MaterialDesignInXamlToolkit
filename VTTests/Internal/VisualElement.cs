using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VTTests.Internal
{

    internal class VisualElement : IVisualElement
    {
        public VisualElement(Protocol.ProtocolClient client, string id)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        private Protocol.ProtocolClient Client { get; }

        public string Id { get; }

        public async Task<IVisualElement> GetElement(string query)
        {
            ElementQuery elementQuery = GetFindElementQuery(query);

            if (await Client.GetElementAsync(elementQuery) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                if (reply.ElementIds.Count == 1)
                {
                    return new VisualElement(Client, reply.ElementIds[0]);
                }
                throw new Exception($"Found {reply.ElementIds.Count} elements");
            }

            throw new Exception("Failed to receive a reply");
        }

        public async Task<IValue> GetProperty(string name)
        {
            var propertyQuery = new PropertyQuery
            {
                ElementId = Id,
                Name = name
            };
            if (await Client.GetPropertyAsync(propertyQuery) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                if (reply.PropertyType is { } propertyType)
                {
                    return new Property(propertyType, reply.ValueType, reply.Value);
                }
                throw new Exception("Property does not have a type specified");
            }
            throw new Exception("Failed to receive a reply");
        }
        
        public async Task<IValue> SetProperty(string name, string value, string? valueType = null)
        {
            var query = new SetPropertyRequest
            {
                ElementId = Id,
                Name = name,
                Value = value,
                ValueType = valueType
            };
            if (await Client.SetPropertyAsync(query) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                if (reply.PropertyType is { } propertyType)
                {
                    return new Property(propertyType, reply.ValueType, reply.Value);
                }
                throw new Exception("Property reply does not have a type specified");
            }
            throw new Exception("Failed to receive a reply");
        }
        
        public async Task<IResource> GetResource(string key)
        {
            var query = new ResourceQuery
            {
                ElementId = Id,
                Key = key
            };

            if (await Client.GetResourceAsync(query) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                if (reply.ValueType is { } valueType)
                {
                    return new Resource(reply.Key, valueType, reply.Value);
                }
                throw new Exception($"Resource with key '{reply.Key}' not found");
            }

            throw new Exception("Failed to receive a reply");
        }

        public async Task<Color> GetEffectiveBackground()
        {
            var propertyQuery = new EffectiveBackgroundQuery
            {
                ElementId = Id
            };
            if (await Client.GetEffectiveBackgroundAsync(propertyQuery) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                return Color.FromArgb((byte)reply.Alpha, (byte)reply.Red, (byte)reply.Green, (byte)reply.Blue);
            }
            throw new Exception("Failed to receive a reply");
        }

        public async Task<Rect> GetCoordinates()
        {
            var query = new CoordinatesQuery
            {
                ElementId = Id
            };

            if (await Client.GetCoordinatesAsync(query) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                return new Rect(reply.Left, reply.Top, reply.Right - reply.Left, reply.Bottom - reply.Top);
            }

            throw new Exception("Failed to receive a reply");
        }

        protected virtual ElementQuery GetFindElementQuery(string query)
            => new ElementQuery
            {
                ParentId = Id,
                Query = query
            };

        public async Task<IImage> GetBitmap()
        {
            var imageQuery = new ImageQuery
            {
                ElementId = Id
            };
            if (await Client.GetImageAsync(imageQuery) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                return new BitmapImage(reply.Data);
            }
            throw new Exception("Failed to receive a reply");
        }
    }
}
