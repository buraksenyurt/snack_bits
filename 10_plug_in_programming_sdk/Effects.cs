namespace Sdk;

public class ShadowEffect : IEffectType
{
    public string Title { get; } = "Shadow(Gölge) Efekti";
    public string Description { get; } = " Seçilen nesneye gölge efekti uygular.";
}

public class BlurEffect : IEffectType
{
    public string Title { get; } = "Blur(Bulanıklaştırma) Efekti";
    public string Description { get; } = " Seçilen nesneyi bulanıklaştırmak için kullanılır.";
}