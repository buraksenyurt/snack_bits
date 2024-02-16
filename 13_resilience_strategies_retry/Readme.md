# Resilience Stratejileri - Retry Mekanizması

Dağıtık sistemlerde servisler ile olan iletişimin sekteye uğraması durumlarında ele alınan bazı hata yönetim stratejileri vardır. Bunları Reactive ve Proactive olmak üzere iki ana başlıkta ele alabiliriz. Reactive modelde Retry, Circuit Braker, Fallback ve Hedging teknikleri kullanılır. Proactive modelde ise Timeout ve Rate Limiter teknikleri söz konusudur. Bu 13ncü atölye ile birlikte bu teknikleri ele alacağım. İlk senaryoda Retry mekanizmasına bakacağız. Retry mekanizmasında servisin hata alması gibi bir durumda bunun geçici olabileceği, birkaç deneme daha yaparak başarılı şekilde işlemini yapacağı prensibine güvenilir.

## Senaryo

Örnek senaryomuzda video oyun satış rakamlarına göre ilk 10u döndüren bir servisimiz var. GameSalaryApi isimli projede yer alan servis metodunda stabil olmama halini simüle etmek için de bir kod parçası yer alıyor. %50 şansla servis geriye HTTP 500 Internal Server Error dönüyor. Buna göre istemci tarafında Retry kurgusu işletilmekte. Bu kurguyu kolay bir şekilde işletmek içinde Polly isimli Nuget paketinden yararlanılıyor. Örnek senaryoda toplamda 3 sefer daha deneme yaptırılıyor. Resilience ile ilgili policy ayarlamaları ConsumerService isimli sınıf içerisinde yapılmakta. 

_Ekran görüntüleri eklenecek_