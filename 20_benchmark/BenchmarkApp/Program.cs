/*

BenchmarkDotNet kütüphanesinin kullanıldığı bir örnek uygulamadır.
Senaryo olarak JSON serileştirme işlemlerinde System.Text.Json
ve Newtonsoft.Json kütüphanelerinin performansını karşılaştırır.

Örneğin çalıştırmak için;

dotnet run -c Release

Çalışma sonucunda aşağıdakine benzer bir çıktı alınır.

| Method                        | Mean        | Error      | StdDev     |
|------------------------------ |------------:|-----------:|-----------:|
| SerializeWithSystemTextJson   |    68.17 ns |   1.398 ns |   3.240 ns |
| SerializeWithNewtonsoftJson   |   126.53 ns |   2.580 ns |   5.664 ns |
| DeserializeWithSystemTextJson | 5,455.51 ns |  25.586 ns |  22.682 ns |
| DeserializeWithNewtonsoftJson | 9,468.13 ns | 189.066 ns | 336.065 ns |

 */
using BenchmarkApp;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Runner>();

