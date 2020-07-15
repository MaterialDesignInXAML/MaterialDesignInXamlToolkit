using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace VTTests
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Application application = new CustomApplication
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown
            };
            application.Run();
        }

        private class CustomApplication : Application
        {
            public IService? Service { get; set; }

            protected override void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);
                Service = Server.Start(this);
            }

            protected override void OnExit(ExitEventArgs e)
            {
                Service?.Dispose();
                base.OnExit(e);
            }
        }
    }
}
