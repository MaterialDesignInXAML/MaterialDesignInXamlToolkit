using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTTests.Internal
{

    internal class App : IApp
    {
        public App(Protocol.ProtocolClient client) 
            => Client = client ?? throw new ArgumentNullException(nameof(client));

        private Protocol.ProtocolClient Client { get; }

        public virtual void Dispose()
        { }

        public async Task Initialize(string applicationResourceXaml, params string[] assemblies)
        {
            var request = new ApplicationConfiguration
            {
                ApplicationResourceXaml = applicationResourceXaml
            };
            request.AssembliesToLoad.AddRange(assemblies);
            if (await Client.InitializeApplicationAsync(request) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                return;
            }
            throw new Exception("Failed to get a reply");
        }

        public async Task<IWindow> CreateWindow(string windowXaml)
        {
            var request = new WindowConfiguration()
            {
                Xaml = windowXaml
            };
            if (await Client.CreateWindowAsync(request) is { } reply)
            {
                if (reply.ErrorMessages.Any())
                {
                    throw new Exception(string.Join(Environment.NewLine, reply.ErrorMessages));
                }
                return new Window(Client, reply.WindowsId);
            }
            throw new Exception("Failed to get a reply");
        }

        public async Task<IWindow?> GetMainWindow()
        {
            if (await Client.GetMainWindowAsync(new GetWindowsQuery()) is { } reply &&
                reply.WindowIds.Count == 1)
            {
                return new Window(Client, reply.WindowIds[0]);
            }
            return null;
        }

        public async Task<IResource> GetResource(string key)
        {
            var query = new ResourceQuery
            {
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

            throw new Exception("Failed ot receive a reply");
        }

        public async Task<IReadOnlyList<IWindow>> GetWindows()
        {
            if (await Client.GetWindowsAsync(new GetWindowsQuery()) is { } reply)
            {
                return reply.WindowIds.Select(x => new Window(Client, x)).ToList();
            }
            return Array.Empty<IWindow>();
        }
    }
}
