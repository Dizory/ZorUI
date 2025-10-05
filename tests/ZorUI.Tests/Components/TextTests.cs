using FluentAssertions;
using Xunit;
using ZorUI.Components.Primitives;
using ZorUI.Core.Properties;

namespace ZorUI.Tests.Components;

public class TextTests
{
    [Fact]
    public void Text_Constructor_SetsContent()
    {
        // Arrange & Act
        var text = new Text("Hello World");

        // Assert
        text.Content.Should().Be("Hello World");
    }

    [Fact]
    public void Text_Bold_SetsFontWeight()
    {
        // Arrange
        var text = new Text("Test");

        // Act
        var result = text.Bold();

        // Assert
        result.FontWeight.Should().Be(FontWeight.Bold);
        result.Should().BeSameAs(text);
    }

    [Fact]
    public void Text_WithFontSize_SetsSize()
    {
        // Arrange
        var text = new Text("Test");

        // Act
        var result = text.WithFontSize(24);

        // Assert
        result.FontSize.Should().Be(24);
    }

    [Fact]
    public void Text_WithColor_SetsColor()
    {
        // Arrange
        var text = new Text("Test");
        var color = Color.Blue;

        // Act
        var result = text.WithColor(color);

        // Assert
        result.TextColor.Should().Be(color);
    }
}
