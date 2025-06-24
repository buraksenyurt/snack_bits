# BenchmarkDotNet Örneği

BenchmarkDotNet kütüphanesinin kullanıldığı bir örnek uygulamadır. Senaryo olarak JSON serileştirme işlemlerinde System.Text.Json ve Newtonsoft.Json kütüphanelerinin performansını karşılaştırır.

Örneğin çalıştırmak için;

```bash
dotnet run -c Release
```

ya da

```bash
dotnet build -c Release
dotnet BenchmarkApp.dll benchmark
```

Çalışma sonucunda aşağıdakine benzer bir çıktı alınır.

| Method                        | Mean        | Error      | StdDev     | Gen0   | Gen1   | Allocated |
|------------------------------ |------------:|-----------:|-----------:|-------:|-------:|----------:|
| SerializeWithSystemTextJson   |    59.92 ns |   0.707 ns |   0.627 ns | 0.0050 |      - |      32 B |
| SerializeWithNewtonsoftJson   |   129.75 ns |   2.628 ns |   6.783 ns | 0.1950 | 0.0004 |    1224 B |
| DeserializeWithSystemTextJson | 5,414.59 ns |  94.143 ns |  88.061 ns | 0.4578 |      - |    2904 B |
| DeserializeWithNewtonsoftJson | 9,268.34 ns | 185.142 ns | 373.997 ns | 1.0376 |      - |    6544 B |

## Süreler

- ns  nanosecond (milyarda bir saniye)
- μs microsecond (milyonda bir saniye)
- ms millisecond (binde bir saniye)

## Temek Kavramlar

- Mean - Ortalama süre, kodun çalıştırılması için geçen ortalama süre.
- Error - Ortalama hata payı, ölçümün ne kadar güvenilir olduğunu gösterir.
- StdDev - Standart sapma, ölçümlerin ne kadar değişken olduğunu gösterir.
- Gen0, Gen1 - Garbage Collector tarafından ayrılan bellek blokları.
- Allocated - Toplam tahsis edilen bellek miktarı.

## Önemli Tespitler

Kaliteli benchmark testlerinde dikkat edilmesi gereken noktaları şöyle ifade edebiliriz.

- Bir warmup aşaması olmalı. Yani belli sayıda ölçüm göz ardı edilmeli. Böylece daha tutarlı sonuçlar elde edilir.
- Ölçüm sayısı yeterli olmalı. Yani her test için yeterli sayıda iterasyon yapılmalı.
- Donanım ve ortam stabil olmalı. Bir başka deyişle testler farklı donanım ve ortamda çalıştırılmamalı.
- Elbette release modda çalıştırılmalı. Zaten aksi durumda terminalden hata alırız.
- GC etkisi olabildiğince azaltılmalı. Esasında, BenchmarkDotNet kütüphanesi bunun için gerekli optimizasyonları yapar.
- Stopwatch kullanmak tam anlamlıya bir benchmark testi için yeterli değildir. Örneğin mikrosaniye düzeyinde yetersiz sonuçlar alınır. Bu yüzden BenchmarkDotNet gibi bu işe özel kütüphaneler kullanılır.
