using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class GetHashCode
    {
        [Fact]
        public void TwoNoneInstancesOfTheSameTypeHaveTheSameHashCode()
        {
            Optional<string>.None.GetHashCode()
                .Should().Be(Optional<string>.None.GetHashCode());
        }

        [Fact]
        public void NoneAndSomeHaveDifferentHashCodes()
        {
            Optional<string>.None.GetHashCode()
                .Should().NotBe(Optional<string>.Some("a").GetHashCode());
        }

        [Fact]
        public void TwoSomeInstancesWithDifferentValuesHaveDifferentHashCodes()
        {
            Optional<string>.Some("a").GetHashCode()
                .Should().NotBe(Optional<string>.Some("b").GetHashCode());
        }

        [Fact]
        public void TwoSomeInstancesWithTheSameValueHaveTheSameHashCode()
        {
            Optional<string>.Some("a").GetHashCode()
                .Should().Be(Optional<string>.Some("a").GetHashCode());
        }

    }
}
