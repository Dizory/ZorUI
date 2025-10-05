using FluentAssertions;
using Xunit;
using ZorUI.Core;
using ZorUI.Components.Primitives;

namespace ZorUI.Tests.Core;

/// <summary>
/// Tests for ZorComponent base class.
/// </summary>
public class ZorComponentTests
{
    private class TestComponent : ZorComponent
    {
        public int BuildCount { get; private set; }
        
        public override ZorElement Build()
        {
            BuildCount++;
            return new Text("Test");
        }
    }

    [Fact]
    public void Component_Build_ReturnsElement()
    {
        // Arrange
        var component = new TestComponent();

        // Act
        var result = component.Build();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Text>();
    }

    [Fact]
    public void Component_SetState_TriggersRebuild()
    {
        // Arrange
        var component = new TestComponent();
        var initialBuildCount = component.BuildCount;

        // Act
        component.SetState("test", "value");

        // Assert
        component.NeedsRebuild.Should().BeTrue();
    }

    [Fact]
    public void Component_GetState_ReturnsStoredValue()
    {
        // Arrange
        var component = new TestComponent();
        component.SetState("key", 42);

        // Act
        var result = component.GetState<int>("key");

        // Assert
        result.Should().Be(42);
    }

    [Fact]
    public void Component_GetState_ReturnsDefaultForMissingKey()
    {
        // Arrange
        var component = new TestComponent();

        // Act
        var result = component.GetState<string>("missing", "default");

        // Assert
        result.Should().Be("default");
    }

    [Fact]
    public void Component_OnMount_IsCalled()
    {
        // Arrange
        var component = new TestComponent();
        var mounted = false;
        component.OnMount += () => mounted = true;

        // Act
        component.OnMount();

        // Assert
        mounted.Should().BeTrue();
    }

    [Fact]
    public void Component_IsMounted_ReflectsState()
    {
        // Arrange
        var component = new TestComponent();

        // Act & Assert
        component.IsMounted.Should().BeFalse();
        
        component.OnMount();
        component.IsMounted.Should().BeTrue();
        
        component.OnUnmount();
        component.IsMounted.Should().BeFalse();
    }
}
