using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class OptionalTests
    {
        #region None

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

        #endregion

        #region Some

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

        #endregion

        #region Match

        [Fact]
        public void IfSomeNotExecutedIfNone()
        {
            Optional<string>.None.Match(
                ifSome: a => { throw new Exception(); });
        }

        [Fact]
        public void IfNoneNotExecutedIfSome()
        {
            Optional<string>.Some("a").Match(
                ifNone: () => { throw new Exception(); });
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

        #endregion

        #region From

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

        #endregion

        #region ValueOrDefault

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

        #endregion

        #region Map

        [Fact]
        public void MapReturnsOptionalWithConvertedValueIfOptionalHasValue()
        {
            var original = Optional<string>.Some("abc");
            var mapped = original.Map(s => s.ToUpper());

            mapped.HasValue.Should().BeTrue();
            mapped.Value.Should().Be("ABC");
        }

        [Fact]
        public void MapReturnsNoneIfOptionalHasNoValue()
        {
            var original = Optional<string>.None;
            var mapped = original.Map(s => s.ToUpper());

            mapped.HasValue.Should().BeFalse();
        }

        [Fact]
        public void MapThrowsExceptionIfItsArgumentIsNull()
        {
            Optional<string>.Some("a")
                .Invoking(o => o.Map<string>(null))
                .ShouldThrow<ArgumentNullException>();
        }

        #endregion

        #region Equals

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

        #endregion

        #region GetHashCode

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

        #endregion
    }
}
