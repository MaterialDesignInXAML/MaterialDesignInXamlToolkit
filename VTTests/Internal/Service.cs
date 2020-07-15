using System;
using System.Windows;
using GrpcDotNetNamedPipes;

namespace VTTests.Internal
{
    internal class Service : IService
    {
        private NamedPipeServer Server { get; }
        private bool IsDisposed { get; set; }

        public Service(string id, Application application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            Server = new NamedPipeServer(VTTests.Server.PipePrefix + id);

            Protocol.BindService(Server.ServiceBinder, new VisualTreeService(application));
            Server.Start();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Server.Dispose();
                }

                IsDisposed = true;
            }
        }

        public void Dispose() => Dispose(true);
    }
}
