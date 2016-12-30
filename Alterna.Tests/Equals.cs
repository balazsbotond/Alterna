using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class Equals
    {
        [Fact]
        public void NoneEqualsNull()
        {
            Optional<string>.None.Equals(null).Should().BeTrue();
        }

        [Fact]
        public void NoneEqualsNoneOfTheSameType()
        {
            Optional<string>.None.Equals(Optional<string>.From(null))
                .Should().BeTrue();
        }

        [Fact]
        public void NoneDoesNotEqualNoneOfAnotherType()
        {
            Optional<string>.None.Equals(Optional<object>.From(null))
                .Should().BeFalse();
        }

        [Fact]
        public void NoneDoesNotEqualSomeOfTheSameType()
        {
            Optional<string>.None.Equals(Optional<string>.Some("a"))
                .Should().BeFalse();
        }

        [Fact]
        public void NoneDoesNotEqualSomeOfAnotherType()
        {
            Optional<string>.None.Equals(Optional<int>.Some(42))
                .Should().BeFalse();
        }

        [Fact]
        public void NoneDoesNotEqualValueOfTheSameType()
        {
            Optional<string>.None.Equals("a").Should().BeFalse();
        }

        [Fact]
        public void NoneDoesNotEqualValueOfAnotherType()
        {
            Optional<string>.None.Equals(42).Should().BeFalse();
        }

        [Fact]
        public void SomeDoesNotEqualNull()
        {
            Optional<string>.Some("a").Equals(null).Should().BeFalse();
        }

        [Fact]
        public void SomeDoesNotEqualNoneOfTheSameType()
        {
            Optional<string>.Some("a").Equals(Optional<string>.None)
                .Should().BeFalse();
        }

        [Fact]
        public void SomeDoesNotEqualNoneOfAnotherType()
        {
            Optional<string>.Some("a").Equals(Optional<object>.None)
                .Should().BeFalse();
        }

        [Fact]
        public void SomeDoesNotEqualSomeOfAnotherType()
        {
            Optional<string>.Some("42").Equals(Optional<int>.Some(42))
                .Should().BeFalse();
        }

        [Fact]
        public void SomeDoesNotEqualSomeOfTheSameTypeWithDifferentValue()
        {
            Optional<string>.Some("a").Equals(Optional<string>.Some("b"))
                .Should().BeFalse();
        }

        [Fact]
        public void SomeEqualsSomeOfTheSameTypeWithTheSameValue()
        {
            Optional<int>.Some(42).Equals(Optional<int>.Some(42))
                .Should().BeTrue();
        }

        [Fact]
        public void SomeDoesNotEqualValueOfAnotherType()
        {
            Optional<int>.Some(42).Equals("42").Should().BeFalse();
        }

        [Fact]
        public void SomeDoesNotEqualDifferentValueOfTheSameType()
        {
            Optional<int>.Some(42).Equals(1).Should().BeFalse();
        }

        [Fact]
        public void SomeEqualsTheSameValueOfTheSameType()
        {
            Optional<int>.Some(42).Equals(42).Should().BeTrue();
        }
    }
}
