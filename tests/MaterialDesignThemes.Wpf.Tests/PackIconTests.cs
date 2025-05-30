using System.ComponentModel;

using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class PackIconTests
{
    [Test]
    [Description("Issue 1255")]
    public async Task EnumMembersMustNotDifferByOnlyCase()
    {
        var enumValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var enumMember in Enum.GetNames(typeof(PackIconKind)))
        {
            await Assert.That($"{enumMember} matches existing enum value and differs only by case").IsTrue();
        }
    }
}
