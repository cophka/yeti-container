using Shouldly;
using Xunit;

namespace YeTi.Tests
{
    public class yeti_container_tests
    {
        [Fact]
        public void resolves_registered_components()
        {
            var container = new YeTiContainer();
            container.Register<ITestInterface, TestImplementation>();

            var resolved_object = container.Resolve<ITestInterface>();

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