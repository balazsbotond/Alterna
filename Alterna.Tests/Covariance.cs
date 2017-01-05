using FluentAssertions;
using Xunit;

namespace Alterna.Tests
{
    public class Covariance
    {
        [Fact]
        public void CovariantAssignment()
        {
            IOptional<object> oo = Optional<string>.Some("a");
            oo.Value.Should().Be("a");
        } 

        [Fact]
        public void CovariantConversion()
        {
            var oo = Optional<string>.Some("a") as IOptional<object>;
            oo.Value.Should().Be("a");
        }
    }
}
