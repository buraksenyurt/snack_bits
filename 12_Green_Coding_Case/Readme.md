# Enerji Sarfiyatı Yüksek Kodlama Senaryosu

Senaryoda büyük bir ERP sisteminde çalışan bir müşteri isteği ele alınmaktadır. Bu isteğin karşılanması için örnek bir fonksiyonellik geliştirilmiştir ancak bu işlev enerji tüketimi açısından negatif etkiler bırakmaktadır. Ayrıca SOLID'in bazı ilkelerine de ihlal etmektedir. Çalışmadaki amaçlardan birisi bu tip bir senaryonun olası etkilerini ve çözüm yollarını tartışmaktır.

## Müşteri İsteği

Bir yönetmen olarak İş Emri Talimatı oluşturduğum ekranda birden fazla talimata ait PDF dosyalarını tek tek ürettirmekte ve sonrasında kendi bilgilsayarıma indirdiğim bu dökümanları yardımcı bir program aracılığı ile birleştirerek ilgili kurumlara e-posta eki halinde göndermekteyim. Gün içerisinde bu tip talimatlardan minimum 100 maksimum 1000 adet gelebilmekte. Yönetmen olarak ilgili ekranda seçtiğim talimalatlar için üretilen PDF'lerin tek seferde oluşturulmasını, tek bir PDF içerisinde birleştirilmesini ve otomatik olarak ilgili paydaşa e-posta eki olarak gönderilmesini istiyorum.

## High Level Diagram

Hali hazırda uygulanmış olan çözümün çalıştırdığı proccess'ler ve entegrasyon noktaları aşağıdaki gibi özetlenebilir.

Ana Process 	    : Web Server
Process 1 	        : Database Server
Process 2 	        : Document Server
Process 3 	        : FTP Server
3rd Party Component : PDF Utility

## Problemler

Uygulanan sistemde çok fazla process çalıştığı görülmektedir. Bu process'lerin farklı makinelerde olduğu düşünülürse söz konusu dağıtık sistemin toplamda fazla ram, cpu ve disk tükettiği söylenebilir. Ayrıca ana fonksiyon birden fazla sorumluluğu üstenmekte olup hem Single Responsibility, hem Open/Closed hem de Dependency Inversion ilkelerini ihlal etmektedir. Diğer yandan kodun ilk versiyonu O(n2) gibi bir maliyete sahiptir. Entegrasyon noktalarında oluşabilecek istisnai durumların yönetimi de bellek tüketimini artıracak etkenlerden birisi olarak görülebilir.

## Çözüm Tartışmaları

Daha sürdürülebilir ve özellikle enerji dostu bir çözüm noktasında aşağıdaki başlıklarda tartışmalar yapılabilir.

- Müşterinin geçerli bir gerekçeye dayanan isteğine ait iş modeli yeniden gözden geçirilebilir.
- İhlal edilen SOLID ilkelerine göre kod yeniden düzenlenebilir.
- FTP erişimi, döküman sunucusu gibi enstrümanlar aradan çıkartılabilir.

## Sorular

- İlk kod modelinin tükettiği toplam enerji miktarı hangi metodoloji ile nasıl ölçümlenir?
- Bu senaryodaki iş modelinin değişmeyeceği düşünülürse ideal çözüm senaryoları nelerdir? 