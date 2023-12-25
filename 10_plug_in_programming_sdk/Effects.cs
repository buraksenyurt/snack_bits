namespace Sdk;

public class ShadowEffect : IEffectType
{
    public string Title { get; } = "Shadow";
    public string Description { get; } = " Seçilen nesneye gölge efekti uygular.";
}

public class BlurEffect : IEffectType
{
    public string Title { get; } = "Blur";
    public string Description { get; } = " Seçilen nesneyi bulanıklaştırmak için kullanılır.";
}