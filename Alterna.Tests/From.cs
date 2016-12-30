using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class From
    {
        [Fact]
        public void FromReturnsNoneIfGivenNull()
        {
            Optional<string>.From(null).HasValue.Should().BeFalse();
        }

        [Fact]
        public void FromReturnsSomeIfGivenNonNull()
        {
            Optional<string>.From("a").HasValue.Should().BeTrue();
        }
    }
}
