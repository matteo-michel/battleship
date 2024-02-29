using BattleShip.API;
using BattleShip.API.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WarService>();
builder.Services.AddSingleton<SeaService>();
builder.Services.AddSingleton<PirateService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<LeaderboardContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
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

app.UseHttpsRedirection();

app.RegisterWarController();
app.MapControllers();

app.Run();
