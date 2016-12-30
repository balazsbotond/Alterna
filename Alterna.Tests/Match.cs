using System;
using Xunit;
using FluentAssertions;

namespace Alterna.Tests
{
    public class Match
    {
        [Fact]
        public void MatchThrowsIfBothArgumentsAreNull()
        {
            Optional<string>.None
                .Invoking(o => o.Match(null, null))
                .ShouldThrow<ArgumentNullException>();
        }

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
    }
}
