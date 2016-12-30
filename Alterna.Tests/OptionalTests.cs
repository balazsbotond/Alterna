using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class OptionalTests
    {
        [Fact]
        public void NoneHasNoValue()
        {
            Optional<string>.None.HasValue.Should().BeFalse();
        }

        [Fact]
        public void NoneValueThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var v = Optional<string>.None.Value;
            });
        }

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

        [Fact]
        public void IfSomeNotExecutedIfNone()
        {
            Optional<string>.None.Match(ifSome: a => { throw new Exception(); });
        }

        [Fact]
        public void IfNoneNotExecutedIfSome()
        {
            Optional<string>.Some("a").Match(ifNone: () => { throw new Exception(); });
        }

        [Fact]
        public void IfSomeExecutedIfSome()
        {
            var e = false;
            Optional<string>.Some("a").Match(
                ifSome: a => e = true,
                ifNone: () => { throw new Exception(); });

            e.Should().BeTrue();
        }

        [Fact]
        public void IfNoneExecutedIfNone()
        {
            var e = false;
            Optional<string>.None.Match(
                ifSome: a => { throw new Exception(); },
                ifNone: () => e = true);

            e.Should().BeTrue();
        }

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
