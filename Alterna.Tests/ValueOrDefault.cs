using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class ValueOrDefault
    {
        [Fact]
        public void ValueOrDefaultReturnsValueIfOptionalHasValue()
        {
            Optional<string>.Some("a").ValueOrDefault().Should().Be("a");
        }

        [Fact]
        public void ValueOrDefaultReturnsTypeDefaultValueIfOptionalHasNoValueAndNoCustomDefaultSupplied()
        {
            Optional<int>.None.ValueOrDefault().Should().Be(0);
        }

        [Fact]
        public void ValueOrDefaultReturnsCustomDefaultValueIfSpecifiedAndOptionalHasNoValue()
        {
            Optional<int>.None.ValueOrDefault(42).Should().Be(42);
        }
    }
}
