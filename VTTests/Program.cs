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

            protected override async void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);
                Service = Test.StartService(this);

                //var app = Test.ConnectToApp();
                //await app.InitialzeWithMaterialDesign();
                //IWindow window = await app.CreateWindow(@"<Button Content=""Button"" />");
                //var bitmap = await window.GetBitmap();
                //using var file = File.OpenWrite("Test.bmp");
                //await bitmap.Save(file);
            }

            protected override void OnExit(ExitEventArgs e)
            {
                Service?.Dispose();
                base.OnExit(e);
            }
        }
    }
}
