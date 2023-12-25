namespace Sdk;

public interface IEffectType
{
    string Title { get; set; }
    string Description { get; set; }
}

public interface IApplyEffectResponse
{
    bool Applied { get; set; }
    string Error { get; set; }
    object Result { get; set; }
}

public interface IApplyEffectRequest
{
    object Source { get; set; }
}

public interface IApplyEffect
{
    IApplyEffectResponse Apply(IApplyEffectRequest request);
}
