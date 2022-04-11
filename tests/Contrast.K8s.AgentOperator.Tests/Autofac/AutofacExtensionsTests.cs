﻿using Autofac;
using Contrast.K8s.AgentOperator.Autofac;
using FluentAssertions;
using Xunit;

namespace Contrast.K8s.AgentOperator.Tests.Autofac
{
    public class AutofacExtensionsTests
    {
        [Fact]
        public void When_contrast_conventions_are_applied_then_Resolve_should_succeed_on_concrete_types()
        {
            var builder = new ContainerBuilder();

            // Act
            builder.ApplyContrastConventions(typeof(AutofacExtensionsTests).Assembly);

            // Assert
            var container = builder.Build();
            container.Resolve<ConcreteClassFixture>().Should().NotBeNull();
        }

        [Fact]
        public void When_contrast_conventions_are_applied_then_Resolve_should_succeed_on_default_interfaces()
        {
            var builder = new ContainerBuilder();

            // Act
            builder.ApplyContrastConventions(typeof(AutofacExtensionsTests).Assembly);

            // Assert
            var container = builder.Build();
            container.Resolve<IDefaultInterfaceFixture>().Should().NotBeNull();
        }

        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable ClassNeverInstantiated.Global
        // ReSharper disable UnusedType.Global
        public class ConcreteClassFixture
        {
        }

        public interface IDefaultInterfaceFixture
        {
        }

        public class DefaultInterfaceFixture : IDefaultInterfaceFixture
        {
        }
        // ReSharper restore ClassNeverInstantiated.Global
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper restore UnusedType.Global
    }
}
