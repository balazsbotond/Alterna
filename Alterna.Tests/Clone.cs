using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alterna.Tests
{
    public class Clone
    {
        [Fact]
        public void CloneOfNoneIsNone()
        {
            var none = Optional<Test>.None;
            var clone = none.Clone();

            clone.Should().Be(Optional<Test>.None);
        }

        [Fact]
        public void CloneOfSomeIsSome()
        {
            var value = Test.Create();
            var some = Optional<Test>.Some(value);
            var clone = (Optional<Test>)some.Clone();

            clone.Should().Be(some);
            clone.Value.Should().BeSameAs(value);
        }

        [Fact]
        public void ValueIsClonedIfItIsCloneable()
        {
            var value = TestCloneable.Create();
            var some = Optional<TestCloneable>.Some(value);
            var clone = (Optional<TestCloneable>)some.Clone();

            clone.Value.Should().NotBeSameAs(value);
        }

        private class Test
        {
            public int Value { get; set; }

            public static Test Create() => new Test { Value = 42 };
        }

        private class TestCloneable : ICloneable
        {
            public int Value { get; set; }

            public static TestCloneable Create()
                => new TestCloneable { Value = 24 };

            public object Clone() => new TestCloneable { Value = Value };
        }
    }
}
