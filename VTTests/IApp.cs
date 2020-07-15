using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VTTests
{
    public interface IApp : IDisposable
    {
        Task Initialize(string applicationResourceXaml, params string[] assemblies);
        Task<IWindow> CreateWindow(string xaml);
        Task<IWindow?> GetMainWindow();
        Task<IReadOnlyList<IWindow>> GetWindows();

        Task<IResource> GetResource(string key);
    }
}
