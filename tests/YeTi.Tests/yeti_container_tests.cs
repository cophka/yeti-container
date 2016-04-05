using Shouldly;
using Xunit;

namespace YeTi.Tests
{
    public class yeti_container_tests
    {
        [Fact]
        public void resolves_registered_components()
        {
            // Arrange
            var container = new YeTiContainer();
            container.Register<ITestInterface, TestImplementation>();

            // Act
            var resolved_object = container.Resolve<ITestInterface>();

            // Arrange
            resolved_object.ShouldBeOfType<TestImplementation>();
        }

        public interface ITestInterface
        {
             
        }

        public class TestImplementation : ITestInterface
        {
        }
    }
}