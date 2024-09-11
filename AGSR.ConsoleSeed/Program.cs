using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using AGSR.ConsoleSeed.Dtos;
using AGSR.ConsoleSeed.Fakers;

using var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("http://localhost:5000/api/");

var serializerOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
};

const int genCount = 100;

var patients = await httpClient.GetFromJsonAsync<List<PatientDto>>("patients");
if (patients is { Count: >= genCount }) return;

var payloads = new PatientFaker().Generate(genCount);

var tasks = new Task[payloads.Count];
var i = 0;

var stopwatch = new Stopwatch();
stopwatch.Start();

foreach (var payload in payloads)
{
    tasks[i++] = httpClient.PostAsJsonAsync("patients", payload, serializerOptions);
}

await Task.WhenAll(tasks);

stopwatch.Stop();

Console.WriteLine($"Seeding finished... ({stopwatch.ElapsedMilliseconds} ms elapsed)");
Console.ReadKey();
    