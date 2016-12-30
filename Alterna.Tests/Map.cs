using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class Map
    {
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

        [Fact]
        public void MapperIsNotInvokedIfOptionalHasNoValue()
        {
            Optional<string>.None.Map<int>(v => { throw new Exception(); });
        }

        [Fact]
        public void MapCanConvertToOptionOfAnotherType()
        {
            Optional<string>.Some("42").Map(v => int.Parse(v)).Value
                .Should().Be(42);
        }
    }
}
