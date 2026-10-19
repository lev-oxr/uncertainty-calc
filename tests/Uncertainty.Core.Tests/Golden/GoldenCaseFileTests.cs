using System.Text.Json;

namespace Uncertainty.Core.Tests.Golden;

public sealed record GoldenCase(
    string Id, string Source, string Checks,
    JsonElement Input, JsonElement Expected, string Status);

public class GoldenCaseFileTests
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    internal static IReadOnlyList<GoldenCase> Load()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Golden", "GoldenCases.json");
        return JsonSerializer.Deserialize<List<GoldenCase>>(File.ReadAllText(path), Options)!;
    }

    [Fact]
    public void Ids_are_unique()
    {
        var cases = Load();
        Assert.Equal(cases.Count, cases.Select(c => c.Id).Distinct().Count());
    }
}