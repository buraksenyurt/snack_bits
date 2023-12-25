using Moq;
using Sdk;

namespace _10_plug_in_programming_sdk_tests;

public class EffectApplierTests
{
    [Fact]
    public void Default_Effects_Can_Load_From_Assembly_Test()
    {
        // var mockCollector = new Mock<IEffectCollector>();
        // var effects = new List<IApplyEffect>(){
        //     new ShadowEffectApplier(),
        // };
        // mockCollector.Setup(c => c.LoadEffects()).Returns(effects);
        var effectManager = new EffectManager(new List<IEffectCollector>() { });
        var actual = effectManager.Effects.Count();
        Assert.True(actual == 2);
    }

    [Fact]
    public void Applied_Shadow_Effects_Works()
    {
        var mockCollector = new Mock<IEffectCollector>();
        var effects = new List<IApplyEffect>(){
            new ReverseEffectApplier(),
        };
        mockCollector.Setup(c => c.LoadEffects()).Returns(effects);
        var effectManager = new EffectManager(new List<IEffectCollector>() { mockCollector.Object });
        Assert.Equal(3, effectManager.Effects.Count());
        var request = new ApplyEffectRequest
        {
            EffectName = "Shadow",
            Source = new object()
        };
        var actual = effectManager.Apply(request);
        var expected = new ApplyEffectResponse
        {
            Applied = true,
            Result = request.Source,
            Error = string.Empty
        };
        Assert.Equal(expected, actual);
    }
}

class ReverseEffect : IEffectType
{
    public string Title => "Reverse";

    public string Description => "Fotoğrafı ters çevirir.";
}
class ReverseEffectApplier : IApplyEffect
{
    public IEffectType Kind => new ReverseEffect();

    public ApplyEffectResponse Apply(ApplyEffectRequest request)
    {
        return new ApplyEffectResponse
        {
            Result = request.Source,
            Applied = true,
            Error = string.Empty
        };
    }
}