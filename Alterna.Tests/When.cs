using FluentAssertions;
using System;
using Xunit;

namespace Alterna.Tests
{
    public class When
    {
        [Fact]
        public void WhenThrowsExceptionIfConditionIsNull()
        {
            Optional<string>.None
                .Invoking(o => o.When(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void WhenFalseReturnsNoneIfOptionalHasNoValue()
        {
            Optional<string>.None.When(v => false)
                .Should().Be(Optional<string>.None);
        }

        [Fact]
        public void WhenFalseReturnsNoneIfOptionalHasValue()
        {
            Optional<string>.Some("a").When(v => false)
                .Should().Be(Optional<string>.None);
        }

        [Fact]
        public void WhenTrueReturnsNoneIfOptionalHasNoValue()
        {
            Optional<string>.None.When(v => true)
                .Should().Be(Optional<string>.None);
        }

        [Fact]
        public void WhenTrueReturnsCopyIfOptionalHasValue()
        {
            Optional<string>.Some("a").When(v => true)
                .Should().Be(Optional<string>.Some("a"));
        }

        [Fact]
        public void ValueIsPassedToThePredicate()
        {
            Optional<string>.Some("a").When(v => v == "a")
                .Should().Be(Optional<string>.Some("a"));
        }
    }
}
