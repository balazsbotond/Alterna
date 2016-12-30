using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class Conversion
    {
        [Fact]
        public void NullIsImplicitlyConvertedToNone()
        {
            Optional<string> o = (string)null;

            o.Should().Be(Optional<string>.None);
        }

        [Fact]
        public void NonNullValueIsImplicitlyConvertedToSome()
        {
            Optional<int> o = 42;

            o.Should().Be(Optional<int>.Some(42));
        }

        [Fact]
        public void ExplicitlyCastingNoneThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                int i = (int)Optional<int>.None;
            });
        }

        [Fact]
        public void SomeIsExplicitlyConvertedToNonNullValue()
        {
            int i = (int)Optional<int>.Some(42);

            i.Should().Be(42);
        }
    }
}
