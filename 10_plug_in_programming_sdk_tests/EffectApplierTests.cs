using Sdk;

namespace _10_plug_in_programming_sdk_tests;

public class EffectApplierTests
{
    [Fact]
    public void Default_Effects_Can_Load_From_Assembly_Test()
    {
        var effectManager = new EffectManager(new List<IEffectCollector>() { new DefaultEffectCollector() });
        var actual = effectManager.Effects.Count();
        Assert.True(actual > 0);
    }
}