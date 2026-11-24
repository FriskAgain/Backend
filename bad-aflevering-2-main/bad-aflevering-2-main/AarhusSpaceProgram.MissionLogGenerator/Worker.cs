using System.Net.Http.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace AarhusSpaceProgram.MissionLogGenerator;

public class MissionLogBackgroundService : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MissionLogBackgroundService> _logger;
    private readonly IMongoCollection<MissionLogEntry> _missionLogs;
    private readonly TimeSpan _interval;

    private static readonly string[] DummyMessages =
    {
        "Telemetry check completed",
        "Crew status good",
        "Systems verified",
        "Update received"
    };

    public MissionLogBackgroundService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<MissionLogBackgroundService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("MissionApi");
        _logger = logger;

        var mongoConnection = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];
        var collectionName = configuration["MongoDb:MissionLogsCollection"];
        var intervalSeconds = configuration.GetValue("MissionLogBackgroundService:IntervalSeconds", 20);

        var mongoClient = new MongoClient(mongoConnection);
        var database = mongoClient.GetDatabase(databaseName);
        _missionLogs = database.GetCollection<MissionLogEntry>(collectionName);
        _interval = TimeSpan.FromSeconds(intervalSeconds);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Giv API tid til at starte
        await Task.Delay(10000, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await GenerateMissionLogs(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling API - retrying...");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task GenerateMissionLogs(CancellationToken stoppingToken)
    {
        var missions = await _httpClient.GetFromJsonAsync<List<MissionDto>>(
            "/api/missions?status=Active",
            stoppingToken);
        
        if(missions == null || missions.Count == 0)
        {
            _logger.LogInformation("No active missions");
            return;
        }

        var logs = missions.Select(m => new MissionLogEntry
        {
            MissionId = m.MissionId,
            Message = DummyMessages[Random.Shared.Next(DummyMessages.Length)],
            Timestamp = DateTime.UtcNow
        }).ToList();

        await _missionLogs.InsertManyAsync(logs, cancellationToken: stoppingToken);

        _logger.LogInformation("Inserted {Count} mission logs", logs.Count);
    }
}

public class MissionDto
{
    public int MissionId{ get; set; }
    public string Name { get; set; } = string.Empty;
}

public class MissionLogEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public int MissionId { get; set; }
    public string Message { get; set; }  = string.Empty;
    public DateTime Timestamp { get; set; }
}