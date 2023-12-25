using System.Reflection;

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

public class DefaultEffectCollector : IEffectCollector
{
    public IEnumerable<IApplyEffect> LoadEffects()
    {
        var assembly = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "10_plug_in_programming_sdk.dll"));
        var appliers = assembly
            .GetTypes()
            .Where(t => t.GetInterface("Sdk.IApplyEffect") != null)
            .Select(t => t as IApplyEffect);
        return appliers;
    }
}