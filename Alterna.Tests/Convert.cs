using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class Convert
    {
        [Fact]
        public void ConvertThrowsIfFirstArgumentIsNull()
        {
            Optional<string>.None
                .Invoking(o => o.Convert(null, 0))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ConvertDoesNotThrowIfSecondArgumentIsNull()
        {
            Optional<string>.None.Convert(x => x, null)
                .Should().BeNull();
        }

        [Fact]
        public void ConvertInvokesIfNoneIfOptionalHasNoValue()
        {
            Optional<int>.None.Convert(
                ifSome: x => { throw new Exception(); },
                ifNone: 42).Should().Be(42);
        }

        [Fact]
        public void ConvertInvokesIfSomeIfOptionalHasValue()
        {
            Optional<int>.Some(42).Convert(
                ifSome: x => (x / 2).ToString(),
                ifNone: string.Empty).Should().Be("21");
        }
    }
}
