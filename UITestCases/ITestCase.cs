using System;
using System.Threading.Tasks;

namespace UITestCases
{
    public interface ITestCase
    {
        Uri Link { get; }
        Task Execute();
    }
}
