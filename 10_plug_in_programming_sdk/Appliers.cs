using System.Reflection;

namespace Sdk;

public class EffectManager
{
    public IEnumerable<IEffectApplier> Effects { get; }

    public EffectManager(IEnumerable<IEffectCollector> collectors)
    {
        Effects = new DefaultEffectCollector().Load();
        foreach (var collector in collectors)
        {
            var effects = collector.Load();
            foreach (var effect in effects)
            {
                Effects = Effects.Append(effect);
            }
        }
    }

    public ApplyEffectResponse Apply(ApplyEffectRequest request)
    {
        foreach (var e in Effects)
        {
            if (e.Kind.Title.ToLower().Equals(request.EffectName.ToLower()))
            {
                return e.Apply(request);
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

public class ShadowEffectApplier : IEffectApplier
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

public class BlurEffectApplier : IEffectApplier
{
    public IEffectType Kind => new BlurEffect();
    public ApplyEffectResponse Apply(ApplyEffectRequest request)
    {
        throw new NotImplementedException();
    }
}

internal class DefaultEffectCollector : IEffectCollector
{
    public IEnumerable<IEffectApplier> Load()
    {
        return new List<IEffectApplier>{
            new ShadowEffectApplier(),
            new BlurEffectApplier()
        };
        // var assembly = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "10_plug_in_programming_sdk.dll"));

        // var appliers = assembly
        //    .GetTypes()
        //    .Where(t => t.GetInterface("Sdk.IEffectApplier") != null);

        // var types = appliers.Select(t => (IEffectApplier)Activator.CreateInstance(t)).ToArray();

        // return types;
    }
}