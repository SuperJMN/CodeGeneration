using CodeGen.Core;
using FluentAssertions;
using Xunit;

namespace CodeGen.Intermediate.Tests
{
    public class ReferenceSpecs
    {
        [Fact]
        public void ReferencesWithSameNameAreEqual()
        {
            var r1 = new Reference("test");
            var r2 = new Reference("test");

            r1.Should().Be(r2);
        }

        [Fact]
        public void UnknownReferences()
        {
            var r1 = new Reference();
            var r2 = new Reference();

            r1.Should().NotBe(r2);
        }
    }
}