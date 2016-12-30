using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class None
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
    }
}
