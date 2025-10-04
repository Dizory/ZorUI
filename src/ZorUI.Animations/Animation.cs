namespace ZorUI.Animations;

/// <summary>
/// Represents an animation that can be applied to UI elements.
/// </summary>
public class Animation
{
    /// <summary>
    /// Duration of the animation in milliseconds.
    /// </summary>
    public double Duration { get; set; } = 300;

    /// <summary>
    /// Delay before animation starts in milliseconds.
    /// </summary>
    public double Delay { get; set; } = 0;

    /// <summary>
    /// Easing function for the animation.
    /// </summary>
    public Easing Easing { get; set; } = Easing.EaseInOut;

    /// <summary>
    /// Animation properties to animate.
    /// </summary>
    public Dictionary<string, AnimationProperty> Properties { get; set; } = new();

    /// <summary>
    /// Whether to repeat the animation.
    /// </summary>
    public bool Repeat { get; set; } = false;

    /// <summary>
    /// Number of times to repeat (0 = infinite).
    /// </summary>
    public int RepeatCount { get; set; } = 0;

    /// <summary>
    /// Whether to reverse the animation on repeat.
    /// </summary>
    public bool AutoReverse { get; set; } = false;

    // Factory methods for common animations

    /// <summary>
    /// Creates a fade in animation.
    /// </summary>
    public static Animation FadeIn() => new Animation
    {
        Properties = new()
        {
            ["Opacity"] = new AnimationProperty { From = 0, To = 1 }
        }
    };

    /// <summary>
    /// Creates a fade out animation.
    /// </summary>
    public static Animation FadeOut() => new Animation
    {
        Properties = new()
        {
            ["Opacity"] = new AnimationProperty { From = 1, To = 0 }
        }
    };

    /// <summary>
    /// Creates a slide in animation from direction.
    /// </summary>
    public static Animation SlideIn(SlideDirection direction, double distance = 100) => new Animation
    {
        Properties = direction switch
        {
            SlideDirection.Left => new() { ["TranslateX"] = new AnimationProperty { From = -distance, To = 0 } },
            SlideDirection.Right => new() { ["TranslateX"] = new AnimationProperty { From = distance, To = 0 } },
            SlideDirection.Up => new() { ["TranslateY"] = new AnimationProperty { From = distance, To = 0 } },
            SlideDirection.Down => new() { ["TranslateY"] = new AnimationProperty { From = -distance, To = 0 } },
            _ => new()
        }
    };

    /// <summary>
    /// Creates a slide out animation to direction.
    /// </summary>
    public static Animation SlideOut(SlideDirection direction, double distance = 100) => new Animation
    {
        Properties = direction switch
        {
            SlideDirection.Left => new() { ["TranslateX"] = new AnimationProperty { From = 0, To = -distance } },
            SlideDirection.Right => new() { ["TranslateX"] = new AnimationProperty { From = 0, To = distance } },
            SlideDirection.Up => new() { ["TranslateY"] = new AnimationProperty { From = 0, To = -distance } },
            SlideDirection.Down => new() { ["TranslateY"] = new AnimationProperty { From = 0, To = distance } },
            _ => new()
        }
    };

    /// <summary>
    /// Creates a scale animation.
    /// </summary>
    public static Animation Scale(double from, double to) => new Animation
    {
        Properties = new()
        {
            ["ScaleX"] = new AnimationProperty { From = from, To = to },
            ["ScaleY"] = new AnimationProperty { From = from, To = to }
        }
    };

    /// <summary>
    /// Creates a rotation animation.
    /// </summary>
    public static Animation Rotate(double from, double to) => new Animation
    {
        Properties = new()
        {
            ["Rotation"] = new AnimationProperty { From = from, To = to }
        }
    };

    /// <summary>
    /// Creates a spring animation (bounce effect).
    /// </summary>
    public static Animation Spring() => new Animation
    {
        Easing = Easing.Spring,
        Duration = 500,
        Properties = new()
        {
            ["ScaleX"] = new AnimationProperty { From = 0.8, To = 1.0 },
            ["ScaleY"] = new AnimationProperty { From = 0.8, To = 1.0 }
        }
    };

    // Fluent API

    public Animation WithDuration(double ms)
    {
        Duration = ms;
        return this;
    }

    public Animation WithDelay(double ms)
    {
        Delay = ms;
        return this;
    }

    public Animation WithEasing(Easing easing)
    {
        Easing = easing;
        return this;
    }

    public Animation WithRepeat(int count = 0, bool autoReverse = false)
    {
        Repeat = true;
        RepeatCount = count;
        AutoReverse = autoReverse;
        return this;
    }
}

/// <summary>
/// Animation property with from/to values.
/// </summary>
public class AnimationProperty
{
    public double From { get; set; }
    public double To { get; set; }
}

/// <summary>
/// Easing functions for animations.
/// </summary>
public enum Easing
{
    Linear,
    EaseIn,
    EaseOut,
    EaseInOut,
    EaseInCubic,
    EaseOutCubic,
    EaseInOutCubic,
    EaseInQuad,
    EaseOutQuad,
    EaseInOutQuad,
    EaseInBack,
    EaseOutBack,
    EaseInOutBack,
    EaseInElastic,
    EaseOutElastic,
    EaseInOutElastic,
    EaseInBounce,
    EaseOutBounce,
    EaseInOutBounce,
    Spring
}

/// <summary>
/// Slide animation directions.
/// </summary>
public enum SlideDirection
{
    Left,
    Right,
    Up,
    Down
}
