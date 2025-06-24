/*

BenchmarkDotNet kütüphanesinin kullanıldığı bir örnek uygulamadır.
Senaryo olarak JSON serileştirme işlemlerinde System.Text.Json
ve Newtonsoft.Json kütüphanelerinin performansını karşılaştırır.

Örneğin çalıştırmak için;

dotnet run -c Release

ya da

dotnet build -c Release
dotnet BenchmarkApp.dll benchmark

Çalışma sonucunda aşağıdakine benzer bir çıktı alınır.

| Method                        | Mean        | Error      | StdDev     | Gen0   | Gen1   | Allocated |
|------------------------------ |------------:|-----------:|-----------:|-------:|-------:|----------:|
| SerializeWithSystemTextJson   |    59.92 ns |   0.707 ns |   0.627 ns | 0.0050 |      - |      32 B |
| SerializeWithNewtonsoftJson   |   129.75 ns |   2.628 ns |   6.783 ns | 0.1950 | 0.0004 |    1224 B |
| DeserializeWithSystemTextJson | 5,414.59 ns |  94.143 ns |  88.061 ns | 0.4578 |      - |    2904 B |
| DeserializeWithNewtonsoftJson | 9,268.34 ns | 185.142 ns | 373.997 ns | 1.0376 |      - |    6544 B |

 */
using BenchmarkApp;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Runner>();

