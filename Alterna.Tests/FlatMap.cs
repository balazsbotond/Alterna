using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class FlatMap
    {
        [Fact]
        public void FlatMapThrowsExceptionIfMapperIsNull()
        {
            Optional<int>.None
                .Invoking(o => o.FlatMap<string>(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void FlatMapReturnsNoneIfMapperReturnsSomeButOptionalHasNoValue()
        {
            Optional<int>.None.FlatMap(v => Optional<string>.Some("a"))
                .Should().Be(Optional<string>.None);
        }

        [Fact]
        public void FlatMapReturnsNoneIfMapperReturnsNoneAndOptionalHasNoValue()
        {
            Optional<int>.None.FlatMap(v => Optional<string>.None)
                .Should().Be(Optional<string>.None);
        }

        [Fact]
        public void FlatMapReturnsNoneIfMapperReturnsNoneAndOptionalHasValue()
        {
            Optional<int>.Some(42).FlatMap(v => Optional<string>.None)
                .Should().Be(Optional<string>.None);
        }

        [Fact]
        public void FlatMapReturnsMappedValueIfMapperReturnsSomeAndOptionalHasValue()
        {
            Optional<int>.Some(42).FlatMap(v => Optional<string>.Some("a"))
                .Should().Be(Optional<string>.Some("a"));
        }

        [Fact]
        public void FlatMapPassesTheValueToTheMapper()
        {
            Optional<int>.Some(42)
                .FlatMap(v =>
                    v == 42
                    ? Optional<string>.Some("42")
                    : Optional<string>.None)
                .Should().Be(Optional<string>.Some("42"));
        }
    }
}
