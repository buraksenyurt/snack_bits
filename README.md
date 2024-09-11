# Abur Cubur

Tek başına repo olamayacak, farklı konularda ve anlık olarak karşıma çıkan durumlara istinaden ele aldığım kod parçalarını toplamayı düşündüğüm alandır. Örneğin işimde karşılaştığım bir kod probleminin çözümü ya da öğrendiğim pratik bir bilginin basit uygulamasını bu repo altındaki klasörlerde toparlayabilirim.

_**Not:** 11nci proje örnekleri SOLID ilkelerini anlatmak için kullanılıyor. Öğrencilere BadSample.cs içeriği gösterilip hangi ilkenin ihlal ettiği sorulur. Proje adlarında SOLID ilke adları kullanılmadığından ellerinde bir ipucu yoktur. Örneğin MasterOfPuppets isimli proje Single Responsibility ilkesinin ihlali ile ilgili örnek kod parçası içermektedir. Diğer 11 örneklerinde de benzer yaklaşım kullanılır. Hangi prensibe hangi şarkı adının yakışacağına karar verirken ChatGPT'den yararlandım._

- [x] **15_using_refit:** Bu klasörde bir web api servisini tüketirken işleri kolaylaştıran Refit paketinden nasıl yararlanabileceğimizi incelemeye çalışıyoruz.
- [x] **14_find_words_count:** Bu örnekte 50 Mb büyüklüğündeki bir Lorem Ipsum dosyasında yer alan tekrarlı kelimelerin sayısını bulan bir kod parçası yer almaktadır. Amacım aynı işi Rust tarafında icra edip performans kıyaslaması yapmak idi. Rayon küfesini kullandığım Rust kod parçası aynı dosyası daha uzun sürede işledi diyebilirim. Yani .Net GC ve paralellik şimdilik kazanmış görünüyor.
- [x] **13_resilience_strategies_retry:** Resilince stratejilerinden retry mekanizmasını Polly nuget aracını kullanarak ele alıyoruz.
- [x] **12_Green_Coding_Case:** Bu örnekte kullandığı kaynaklar bakımından enerji sarfiyatı yüksek olabilecek bir senaryo ele alınmaktadır.
- [x] **11_HighwayToHell:** Şirketteki eğitimlerde **SOLID** ilkelerinden **Dependency Inversion** prensibini anlatmak için kullandığım örnek proje. BadSample dosyasında ihlal, Refactored dosyasında ise ideal uygulanma biçimi yer alıyor.
- [x] **11_Paranoid:** Şirketteki eğitimlerde **SOLID** ilkelerinden **Interface Segregation** prensibini anlatmak için kullandığım örnek proje. BadSample dosyasında ihlal, Refactored dosyasında ise ideal uygulanma biçimi yer alıyor.
- [x] **11_FearOfTheDark:** Şirketteki eğitimlerde **SOLID** ilkelerinden **Liskov Substitution** prensibini anlatmak için kullandığım örnek proje. BadSample dosyasında ihlal, Refactored dosyasında ise ideal uygulanma biçimi yer alıyor.
- [x] **11_SymphonyOfDestruction:** Şirketteki eğitimlerde **SOLID** ilkelerinden **Open/Closed** prensibini anlatmak için kullandığım örnek proje. BadSample dosyasında ihlal, Refactored dosyasında ise ideal uygulanma biçimi yer alıyor.
- [x] **11_MasterOfPuppets:** Şirketteki eğitimlerde **SOLID** ilkelerinden **Single Responsibility** prensibini anlatmak için kullandığım örnek proje. BadSample dosyasında ihlal, Refactored dosyasında ise ideal uygulanma biçimi yer alıyor.
- [x] **10_plug_in_programming ve 10_plug_in_programming_sdk:** Bu çalışmadaki amacım dışarıdan yeni efektler entegre edilebilen bir fotoğraf kütüphanesinin iskeletini oluşturabilmek. Temel gayem plug-in'lerin interface'ler aracılığıyla sisteme dahil edilmesini sağlamak.
- [x] **09_mocking ve 09_mocking_test :** Bu çalışmadaki amaç test edilebilirlik için kodun yeniden değerlendirilmesi ve mock nesne kullanımlarını keşfetmektir.
- [x] **08_dependencies :** Nesne bağımlılıklarını yönetmenin etkili enstrümanlarından birisi de arayüz(Interface) kullanımı. Bu örnekte bir veri kümesinin fiziki diske yazma operasyonuna ilişkin bağımlılığı dışarıya açmanın ideal yolunu bulmaya çalışıyorum. 
- [x] **07_remembering_events :** C# ile geliştirilen uygulamalarda event kullanımını hatırlamak amacıyla basit ve az biraz eğlenceli olduğunu düşündüğüm bir uygulamayı eklemek istedim.
- [x] **06_avrasya_passenger :** Değerli meslektaşlarımdan Muhtalip Dede'nin geliştirdiği Nodejs tabanlı web framework'ü kullanmayı denediğim bir uygulama eklemek istedim.
- [x] **05_eat_switch_case_csharp:** En temel seviyede switch...case bloğundan kaçınmanın bir yolunu incelemek istedim.
- [x] **04_constants_to_enum:** Bir ihtiyaç sebebiyle sayısız constant değer barındıran static sınıfların enum sabiti olarak dönüştürülmesi gerekmişti.
- [x] **03_find_switch_with_rust:** Bu örnekte amacım c# kod dosyalarında geçen switch bloklarını tespit etmek ve bunu Rust ile yapmak. Hatta switch bloklarının kolayca bulunması için pest isimli yardımcı bir rust küfesini kullanmayı düşünüyorum.
- [x] **02_if_rule_engine_csharp:** Bu ikinci örnekte de if bloklarından sıyrılmaya çalışıyorum. Bu sefer .Net tarafında popüler olan bir rule engine paketini işin içerisine katmaktayım.
- [x] **01_if_challenge_csharp :** Repoyu açmaya karar verdiğim günün ilk örneği. Karşılaştığım bir kod parçasındaki if bloklarından sadece kurtulmak istedim.