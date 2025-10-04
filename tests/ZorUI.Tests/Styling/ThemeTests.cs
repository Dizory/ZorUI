using FluentAssertions;
using Xunit;
using ZorUI.Styling;

namespace ZorUI.Tests.Styling;

public class ThemeTests
{
    [Fact]
    public void Theme_Light_HasLightColors()
    {
        // Act
        var theme = Theme.Light();

        // Assert
        theme.Should().NotBeNull();
        theme.Colors.Should().NotBeNull();
        theme.Colors.Background.Should().NotBeNull();
    }

    [Fact]
    public void Theme_Dark_HasDarkColors()
    {
        // Act
        var theme = Theme.Dark();

        // Assert
        theme.Should().NotBeNull();
        theme.Colors.Should().NotBeNull();
        theme.Colors.Background.Should().NotBeNull();
    }

    [Fact]
    public void Theme_HasTypography()
    {
        // Act
        var theme = Theme.Light();

        // Assert
        theme.Typography.Should().NotBeNull();
        theme.Typography.FontSizeBase.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Theme_HasSpacing()
    {
        // Act
        var theme = Theme.Light();

        // Assert
        theme.Spacing.Should().NotBeNull();
        theme.Spacing.Space4.Should().BeGreaterThan(0);
    }
}
