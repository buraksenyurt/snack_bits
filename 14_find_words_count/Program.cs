using System.Collections.Concurrent;

var stopwatch = System.Diagnostics.Stopwatch.StartNew();

var filePath = "lorem_ipsum_large.txt";
var wordCounts = new ConcurrentDictionary<string, int>();

Parallel.ForEach(File.ReadLines(filePath), line =>
{
    var words = line.Split(new char[] { ' ', '.', '?', '!', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
    foreach (var word in words)
    {
        wordCounts.AddOrUpdate(word.ToLower(), 1, (key, oldValue) => oldValue + 1);
    }
});

foreach (var pair in wordCounts.OrderByDescending(pair => pair.Value))
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}


stopwatch.Stop();
Console.WriteLine($"Geçen süre: {stopwatch.ElapsedMilliseconds} ms");
