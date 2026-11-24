using Microsoft.EntityFrameworkCore;
using Serilog;
using AarhusSpaceProgram.API.db;
using AarhusSpaceProgram.Enums;
using AarhusSpaceProgram.Api.Services;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AarhusSpaceProgram.db.model;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Logging
    .ClearProviders()
    .AddSimpleConsole()
    .AddDebug();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
    lc.Enrich.FromLogContext();
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// identity service
builder.Services.AddIdentity<ApiUser, IdentityRole>(options =>
{
options.Password.RequireDigit = true;
options.Password.RequireLowercase = true;
options.Password.RequireUppercase = true;
options.Password.RequireNonAlphanumeric = true;
options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<AarhusSpaceProgramDbContext>();

// authentication service
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWT:Audience"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(
    System.Text.Encoding.UTF8.GetBytes(
    builder.Configuration["JWT:SigningKey"]))
};
});



builder.Services.AddControllers();

//Authorization
builder.Services.AddOpenApi(options =>
    {
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
    {
    In = ParameterLocation.Header,
    Description = "Please enter token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "bearer"
    });
    return Task.CompletedTask;
    });
});


builder.Services.AddDbContext<AarhusSpaceProgramDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<MissionService>();
builder.Services.AddScoped<AstronautService>();
builder.Services.AddScoped<LaunchpadService>();
builder.Services.AddScoped<ManagerService>();
builder.Services.AddScoped<RocketService>();
builder.Services.AddScoped<ScientistService>();
builder.Services.AddScoped<ExperimentService>();
builder.Services.AddSingleton<MissionLogService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AarhusSpaceProgramDbContext>();
    db.Database.Migrate();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApiUser>>();

    // Create roles
    string[] roles = { RoleNames.Astronaut, RoleNames.Scientist, RoleNames.Manager };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.

    app.MapOpenApi();
    app.MapScalarApiReference();


app.UseHttpsRedirection();

app.Use(async (ctx, nxt) =>
{
    await nxt();

    if (HttpMethods.IsPost(ctx.Request.Method) ||
        HttpMethods.IsPut(ctx.Request.Method) ||
        HttpMethods.IsDelete(ctx.Request.Method))
    {
        Log.Information(
            "[{Timestamp}]: HTTP {HttpMethod} {RequestPath} responded {StatusCode}",
            DateTime.UtcNow,
            ctx.Request.Method,
            ctx.Request.Path,
            ctx.Response.StatusCode);
    }
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();