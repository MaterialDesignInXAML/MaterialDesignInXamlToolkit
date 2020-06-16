using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests
{
    public abstract class UITestCasesBase : TestBase
    {
        protected UITestCasesBase(ITestOutputHelper output) 
            : base(output, App.UITestsAppPath)
        {
        }
    }
}
