# Plug-In Tabanlı Programlama Deneyimi

Bu çalışmada amaç plug-in tabanlı programlama için zemin hazırlamak. 10_plug_in_programming_sdk isimli class library içerisinde bir grafik programında nesneler için uygulanacak efektler için çeşitli tipler yer alıyor. Efektlere ait bilgiler için IEffectType, efektlerin uygulayıcıları için IApplyEffect, efektleri reflection ile çalışma zamanında toplayacağımız bileşenler için IEffectCollector gibi arayüzler var. SDK içerisinde standart olarak birkaç temel efekt uygulayıcısı ve efekt yükleyicisi bileşenler de tanımlanmış durumda. 

Normalde efektleri yöneten EffectManager sınıfının yapıcı metodu IEffectCollectore arayüzünü uygulayan bir bileşen nesnesini dışarıdan almakta. DefaultEffectCollector isimli sınıfımız internal erişim belirleyicisin sahip ve esasında EffectManager sınıfının yapıcı metodunda doğrudan çalıştırılıyor. Dolayısıyla object user eğer isterse ek efektler için kendi IEffectCollector implementasyonunu yazabilir.

EffectManager sınıfının şu anki versiyonu aşağıdaki gibidir.

```csharp
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
}
```

Hedef, SDK kütüphanesini referans eden başka bir kütüphanede yeni efektler tanımlamak ve bu efektleri varsayılan efektlerle birlikte başka bir Console uygulamasına yükleyebilmek. Yanlız bu oyunda bir kuralımız var. Console uygulaması, SDK türevli kendi kütüphanemizi referans edemez. Bunun yerine kütüphanenin dll dosyası bellir bir klasöre konulabilir. Dolayısıyla console uygulaması EffectManager'ı kullanırken kendi IEffectCollector implementasyonu ile bu fiziki assembly içeriğinin de eklenmesini sağlayabilir.