using BattleShip.API;
using BattleShip.API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Grpc.AspNetCore.Web;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WarService>();
builder.Services.AddSingleton<FleetService>();
builder.Services.AddSingleton<ShipService>();
builder.Services.AddSingleton<SeaService>();
builder.Services.AddSingleton<PirateService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<LeaderboardContext>();
builder.Services.AddGrpc();

builder.Services.AddCors();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(c =>
    {
        c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}"
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var leaderboardContext = scope.ServiceProvider.GetRequiredService<LeaderboardContext>();
    leaderboardContext.Database.EnsureCreated();
    leaderboardContext.SaveChanges();
}

app.UseCors("AllowAnyOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors((c) =>
{
    c.AllowAnyMethod();
    c.AllowAnyHeader();
    c.AllowAnyOrigin();
});
app.UseGrpcWeb();
app.MapGrpcService<LeaderBoardGrpcService>().EnableGrpcWeb();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterWarController();
app.MapControllers();

app.Run();
