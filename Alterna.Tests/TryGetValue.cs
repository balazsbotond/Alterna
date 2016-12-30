using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class TryGetValue
    {
        [Fact]
        public void TryGetValueReturnsFalseIfNone()
        {
            int v;
            Optional<int>.None.TryGetValue(out v).Should().BeFalse();
        }

        [Fact]
        public void TryGetValueReturnsTrueIfSome()
        {
            int v;
            Optional<int>.Some(42).TryGetValue(out v).Should().BeTrue();
        }

        [Fact]
        public void TryGetValueAssignsValueToOutParameter()
        {
            int v;
            Optional<int>.Some(42).TryGetValue(out v);
            v.Should().Be(42);
        }
    }
}
