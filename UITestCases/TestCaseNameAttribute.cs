using System;

namespace UITestCases
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestCaseNameAttribute : Attribute
    {
        public string Name { get; }

        public TestCaseNameAttribute(string name) => Name = name;
    }
}
