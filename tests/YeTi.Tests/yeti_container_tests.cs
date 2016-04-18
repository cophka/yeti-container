using Shouldly;
using Xunit;

namespace YeTi.Tests
{
    public class yeti_container_tests
    {
        ITestInterface act()
        {
            return _container.Resolve<ITestInterface>();
        }

        [Fact]
        public void resolves_registered_components()
        {
            _container.Register<ITestInterface, TestImplementation>();

            var result = act();

            result.ShouldBeOfType<TestImplementation>();
        }

        [Fact]
        public void resolves_components_with_ctor_with_dependencies()
        {
            _container.Register<Dependency, Dependency>();
            _container.Register<ITestInterface, TestImplementationWithDependency>();

            var result = act();

            result.ShouldBeOfType<TestImplementationWithDependency>();
        }

        [Fact]
        public void throws_when_component_has_multiple_ctors()
        {
            _container.Register<Dependency, Dependency>();
            _container.Register<ITestInterface, TestImplementationWithMultipleCtors>();

            var exc = Record.Exception(
                () => act()
            );

            exc.ShouldNotBe(null);
            exc.ShouldBeOfType<ComponentHasMultipleConstructorsException>();
        }

        readonly YeTiContainer _container;

        public yeti_container_tests()
        {
            _container = new YeTiContainer();
        }

        public interface ITestInterface
        {
             
        }

        public class TestImplementation : ITestInterface
        {
        }

        public class Dependency
        {
             
        }

        public class TestImplementationWithDependency : ITestInterface
        {
            public TestImplementationWithDependency(Dependency dependency)
            {
                
            }
        }

        public class TestImplementationWithMultipleCtors : ITestInterface
        {
            public TestImplementationWithMultipleCtors()
            {
                
            }

            public TestImplementationWithMultipleCtors(Dependency dependency)
            {
                
            }
        }
    }
}