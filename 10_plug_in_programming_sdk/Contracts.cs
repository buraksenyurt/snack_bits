namespace Sdk;

public interface IEffectType
{
    string Title { get; }
    string Description { get; }
}

public class ApplyEffectResponse
{
    public bool Applied { get; set; }
    public string? Error { get; set; }
    public required object? Result { get; set; }
}

public class ApplyEffectRequest
{
    public required string EffectName { get; set; }
    public required object Source { get; set; }
}

public interface IApplyEffect
{
    IEffectType Kind { get; }
    ApplyEffectResponse Apply(ApplyEffectRequest request);
}

public interface IEffectCollector
{
    IEnumerable<IApplyEffect> LoadEffects();
}
