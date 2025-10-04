using FluentAssertions;
using Xunit;
using ZorUI.Components.Primitives;
using ZorUI.Core.Properties;

namespace ZorUI.Tests.Components;

/// <summary>
/// Tests for Button component.
/// </summary>
public class ButtonTests
{
    [Fact]
    public void Button_Constructor_SetsText()
    {
        // Arrange & Act
        var button = new Button("Click me");

        // Assert
        button.Text.Should().Be("Click me");
    }

    [Fact]
    public void Button_OnClick_IsInvoked()
    {
        // Arrange
        var clicked = false;
        var button = new Button("Test", () => clicked = true);

        // Act
        button.OnClick?.Invoke();

        // Assert
        clicked.Should().BeTrue();
    }

    [Fact]
    public void Button_Primary_SetsVariant()
    {
        // Arrange
        var button = new Button("Test");

        // Act
        var result = button.Primary();

        // Assert
        result.Variant.Should().Be(ButtonVariant.Primary);
        result.Should().BeSameAs(button); // Fluent API
    }

    [Fact]
    public void Button_Disabled_SetsProperty()
    {
        // Arrange
        var button = new Button("Test");

        // Act
        var result = button.Disabled(true);

        // Assert
        result.IsDisabled.Should().BeTrue();
    }

    [Fact]
    public void Button_WithFullWidth_SetsProperty()
    {
        // Arrange
        var button = new Button("Test");

        // Act
        var result = button.WithFullWidth();

        // Assert
        result.FullWidth.Should().BeTrue();
    }

    [Fact]
    public void Button_Small_SetsSize()
    {
        // Arrange
        var button = new Button("Test");

        // Act
        var result = button.Small();

        // Assert
        result.Size.Should().Be(ButtonSize.Small);
    }
}
