using GameStore.API.Data;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectString = "Data Source=GameStore.db";

builder.Services.AddSqlite<GameStoreContext>(connectString);


var app = builder.Build();

app.MapGamesEndpoints();
app.Run();
