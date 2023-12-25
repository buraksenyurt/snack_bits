using Moq;
using Sdk;

namespace _10_plug_in_programming_sdk_tests;

public class EffectApplierTests
{
    [Fact]
    public void Default_Effects_Can_Load_From_Assembly_Test()
    {
        var effectManager = new EffectManager(new List<IEffectCollector>() { });
        Assert.Equal(2, effectManager.Effects.Count());
    }

    [Fact]
    public void Add_Extra_Effects_Can_Load_From_Assembly_Test()
    {
        var mockCollector = new Mock<IEffectCollector>();
        var effects = new List<IEffectApplier>(){
            new ReverseEffectApplier(),
        };
        mockCollector.Setup(c => c.Load()).Returns(effects);
        var effectManager = new EffectManager(new List<IEffectCollector>() { mockCollector.Object });
        Assert.Equal(3, effectManager.Effects.Count());
    }

    [Fact]
    public void Use_With_Interface_Test()
    {
        var se = new ShadowEffectApplier();
        IEffectApplier applier = se;
        Assert.Equal("Shadow", applier.Kind.Title);
    }

    [Fact]
    public void Applied_Shadow_Effects_Works()
    {
        var effectManager = new EffectManager(new List<IEffectCollector>() { });
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
        Assert.Equal(expected.Applied, actual.Applied);
    }
}

class ReverseEffect : IEffectType
{
    public string Title => "Reverse";

    public string Description => "Fotoğrafı ters çevirir.";
}
class ReverseEffectApplier : IEffectApplier
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