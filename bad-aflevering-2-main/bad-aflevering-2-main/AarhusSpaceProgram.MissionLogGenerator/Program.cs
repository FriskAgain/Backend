using AarhusSpaceProgram.MissionLogGenerator;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient("MissionApi", client =>
{
    var baseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://api:8080";
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHostedService<MissionLogBackgroundService>();

var host = builder.Build();
host.Run();