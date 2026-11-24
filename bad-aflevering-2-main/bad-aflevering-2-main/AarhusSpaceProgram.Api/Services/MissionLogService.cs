using AarhusSpaceProgram.Api.DTOs;
using MongoDB.Driver;

namespace AarhusSpaceProgram.Api.Services;

public class MissionLogService
{
    private readonly IMongoCollection<MissionLogDto> _missionLogs;

    public MissionLogService(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];
        var collectionName = configuration["MongoDb:MissionLogsCollection"];

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _missionLogs = database.GetCollection<MissionLogDto>(collectionName);
    }

    public async Task<List<MissionLogDto>> GetLogsByMissionId(int missionId)
    {
        return await _missionLogs
            .Find(log => log.MissionId == missionId)
            .SortByDescending(log => log.Timestamp)
            .ToListAsync();
    }
}