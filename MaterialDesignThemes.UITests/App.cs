using System.IO;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MaterialDesignThemes.UITests
{
    public static class App
    {
#if DEBUG
        private const string Configuration = "Debug";
#else
        private const string Configuration = "Release";
#endif

        public static string DemoAppPath => Path.GetFullPath(@$"..\..\..\..\MainDemo.Wpf\bin\{Configuration}\netcoreapp3.1\MaterialDesignDemo.exe");

        public static string UITestsAppPath => Path.GetFullPath(@$"..\..\..\..\UITestCases\bin\{Configuration}\netcoreapp3.1\UITestCases.exe");
    }
}
