using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using GrpcDotNetNamedPipes;
using VTTests.Internal;

namespace VTTests
{
    public static class App
    {
        public static IApp StartRemote(string? path = null)
        {
            path ??= Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, ".exe");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                throw new Exception($"Could not find test app '{path}'");
            }

            var startInfo = new ProcessStartInfo(path)
            {
                WorkingDirectory = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar,
            };
            Process process = Process.Start(startInfo);
            var channel = new NamedPipeChannel(".", Server.PipePrefix + process.Id);
            var client = new Protocol.ProtocolClient(channel);

            return new ManagedApp(process, client);
        }
    }
}
