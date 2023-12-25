using System.Reflection;

namespace Sdk;

public class EffectManager
{
    public IEnumerable<IApplyEffect> Effects { get; }

    public EffectManager(IEnumerable<IEffectCollector> collectors)
    {
        Effects = new DefaultEffectCollector().LoadEffects();
        foreach (var collector in collectors)
        {
            var effects = collector.LoadEffects();
            foreach (var effect in effects)
            {
                _ = Effects.Append(effect);
            }
        }
    }

    public ApplyEffectResponse Apply(ApplyEffectRequest request)
    {
        foreach (var effect in Effects)
        {
            Console.WriteLine($".... {effect.Kind.Title}");
            if (effect.Kind.Title.ToLower().Equals(request.EffectName.ToLower()))
            {
                return effect.Apply(request);
            }
        }
        return new ApplyEffectResponse
        {
            Applied = false,
            Result = null,
            Error = "Efekt bulunamadı"
        };
    }
}

public class ShadowEffectApplier : IApplyEffect
{
    public IEffectType Kind => new ShadowEffect();

    public ApplyEffectResponse Apply(ApplyEffectRequest request)
    {
        return new ApplyEffectResponse
        {
            Applied = true,
            Result = request.Source,
            Error = string.Empty
        };
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

internal class DefaultEffectCollector : IEffectCollector
{
    public IEnumerable<IApplyEffect> LoadEffects()
    {
        var result = new List<IApplyEffect>();
        var assembly = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "10_plug_in_programming_sdk.dll"));
        var appliers = assembly
            .GetTypes()
            .Where(t => t.GetInterface("Sdk.IApplyEffect") != null)
            .Select(t => t as IApplyEffect);
        foreach (var applier in appliers)
        {
            var instance = (IApplyEffect)Activator.CreateInstance(applier.GetType());
            result.Add(instance);
        }
        return result;
    }
}