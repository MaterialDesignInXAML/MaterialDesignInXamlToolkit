using MaterialDesignThemes.UITests;
using TUnit.Core.Executors;
using TUnit.Core.Interfaces;

[assembly: TestExecutor<XamlTestExecutor>]

namespace MaterialDesignThemes.UITests;

public class XamlTestExecutor : ITestExecutor
{
    public async ValueTask ExecuteTest(TestContext context, Func<ValueTask> action)
    {
        if (context.TestDetails.ClassInstance is TestBase testBase)
        {
            await using var app = await testBase.StartApp();

            await using var recorder = new TestRecorder(app,
                callerFilePath: context.TestDetails.TestFilePath,
                unitTestMethod: context.TestDetails.TestMethod.Name);
            testBase.App = app;
            testBase.Recorder = recorder;

            await action();

            recorder.Success();
        }
        else
        {
            await action();
        }
    }
}
