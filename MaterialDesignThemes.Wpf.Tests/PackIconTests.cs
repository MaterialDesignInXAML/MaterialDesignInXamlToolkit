using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class PackIconTests
    {
        [Fact]
        [Description("Issue 1255")]
        public void EnumMembersMustNotDifferByOnlyCase()
        {
            var enumValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var enumMember in Enum.GetNames(typeof(PackIconKind)))
            {
                Assert.True(enumValues.Add(enumMember), $"{enumMember} matches existing enum value and differs only by case");
            }
        }
    }
}