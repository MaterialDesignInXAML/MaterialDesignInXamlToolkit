using MaterialDesignThemes.UITests;
using TUnit.Core.Executors;
using TUnit.Core.Interfaces;

[assembly: TestExecutor<XamlTestExecutor>]

namespace MaterialDesignThemes.UITests;

public class XamlTestExecutor : ITestExecutor
{
    public async ValueTask ExecuteTest(TestContext context, Func<ValueTask> action)
    {
        if (context is ITestMetadata { TestDetails.ClassInstance: TestBase testBase } testMetadata)
        {
            await using var app = await testBase.StartApp();

            await using var recorder = new TestRecorder(app,
                callerFilePath: testMetadata.TestDetails.TestFilePath,
                unitTestMethod: testMetadata.TestDetails.TestName);
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
