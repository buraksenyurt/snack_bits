using BenchmarkDotNet.Attributes;

namespace BenchmarkApp;

[MemoryDiagnoser]
// [SimpleJob(launchCount: 1, warmupCount: 1,iterationCount:3)] // Daha minimal bir benchmark testi için bu tip ayarlar da verilebilir.
public class Runner
{
    private List<Game> games = [];
    private string jsonData = string.Empty;

    [GlobalSetup]
    public void Setup()
    {
        var repository = new DataRepository();
        var games = repository.GetGames();
        jsonData = System.Text.Json.JsonSerializer.Serialize(games);
    }

    [Benchmark]
    public string SerializeWithSystemTextJson()
    {
        return System.Text.Json.JsonSerializer.Serialize(games);
    }

    [Benchmark]
    public string SerializeWithNewtonsoftJson()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(games);
    }

    [Benchmark]
    public List<Game> DeserializeWithSystemTextJson()
    {
        return System.Text.Json.JsonSerializer.Deserialize<List<Game>>(jsonData);
    }

    [Benchmark]
    public List<Game> DeserializeWithNewtonsoftJson()
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Game>>(jsonData);
    }
}
