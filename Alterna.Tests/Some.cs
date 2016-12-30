using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class Some
    {
        [Fact]
        public void SomeHasValue()
        {
            Optional<string>.Some("a").HasValue.Should().BeTrue();
        }

        [Fact]
        public void SomeHasCorrectValue()
        {
            Optional<string>.Some("a").Value.Should().Be("a");
        }
    }
}
