namespace Sdk;

public class EffectManager
{
    public IEnumerable<IApplyEffect> Effects { get; }

    public EffectManager(IEffectCollector collector)
    {
        Effects = collector.LoadEffects();
    }

    public static ApplyEffectResponse Apply(IApplyEffect effect, ApplyEffectRequest request)
    {
        return effect.Apply(request);
    }
}

public class ShadowEffectApplier : IApplyEffect
{
    public IEffectType Kind => new ShadowEffect();

    public ApplyEffectResponse Apply(ApplyEffectRequest request)
    {
        throw new NotImplementedException();
    }
}

public class BlurEffectApplier : IApplyEffect
{
    public IEffectType Kind => new BlurEffect();
    public ApplyEffectResponse Apply(ApplyEffectRequest request)
    {
        throw new NotImplementedException();
    }
}