using ZorUI.Core;

namespace ZorUI.Animations;

/// <summary>
/// Wrapper component that applies animations to child elements.
/// </summary>
public class Animatable : ZorComponent
{
    public ZorElement Child { get; set; }
    public List<Animation> Animations { get; set; } = new();
    public AnimationTrigger Trigger { get; set; } = AnimationTrigger.OnMount;

    private bool _hasPlayed = false;

    public Animatable(ZorElement child)
    {
        Child = child;
    }

    public override ZorElement Build()
    {
        if (Trigger == AnimationTrigger.OnMount && !_hasPlayed)
        {
            PlayAnimations();
            _hasPlayed = true;
        }

        return Child;
    }

    public override void OnMount()
    {
        base.OnMount();
        if (Trigger == AnimationTrigger.OnMount)
        {
            PlayAnimations();
        }
    }

    private void PlayAnimations()
    {
        foreach (var animation in Animations)
        {
            // Trigger animation
            Child.Metadata["CurrentAnimation"] = animation;
        }
    }

    // Fluent API

    public Animatable WithAnimation(Animation animation)
    {
        Animations.Add(animation);
        return this;
    }

    public Animatable WithTrigger(AnimationTrigger trigger)
    {
        Trigger = trigger;
        return this;
    }

    public void Play()
    {
        PlayAnimations();
    }

    public void Stop()
    {
        Child.Metadata.Remove("CurrentAnimation");
    }
}

/// <summary>
/// Determines when animation should be triggered.
/// </summary>
public enum AnimationTrigger
{
    OnMount,
    Manual,
    OnHover,
    OnClick
}

/// <summary>
/// Extension methods for animating elements.
/// </summary>
public static class AnimationExtensions
{
    public static Animatable Animate(this ZorElement element, Animation animation)
    {
        return new Animatable(element).WithAnimation(animation);
    }

    public static Animatable Animate(this ZorElement element, params Animation[] animations)
    {
        var animatable = new Animatable(element);
        foreach (var animation in animations)
        {
            animatable.WithAnimation(animation);
        }
        return animatable;
    }
}
