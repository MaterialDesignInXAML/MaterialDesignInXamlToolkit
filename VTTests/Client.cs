using System;
using System.Diagnostics;
using GrpcDotNetNamedPipes;

namespace VTTests
{
    public static class Client
    {
        public static IApp ConnectToApp() 
            => ConnectToApp(Process.GetCurrentProcess());

        public static IApp ConnectToApp(int processId) 
            => ConnectToApp(Process.GetProcessById(processId));

        public static IApp ConnectToApp(Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            var channel = new NamedPipeChannel(".", Server.PipePrefix + process.Id);
            var client = new Protocol.ProtocolClient(channel);

            return new Internal.App(client);
        }
    }
}
