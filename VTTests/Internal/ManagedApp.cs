using System;
using System.Diagnostics;

namespace VTTests.Internal
{
    internal class ManagedApp : App
    {
        public ManagedApp(Process managedProcess, Protocol.ProtocolClient client)
            : base(client) => ManagedProcess = managedProcess ?? throw new ArgumentNullException(nameof(managedProcess));

        public Process ManagedProcess { get; }

        public override void Dispose()
        {
            base.Dispose();
            ManagedProcess.Kill();
            ManagedProcess.WaitForExit();
        }
    }
}
