using GameStore.API.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameName = "GetName";

List<GameDto> games = [
    new(
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(2019, 7, 15)
    ),
    new(
        2,
        "Call of Duty V",
        "Conmbat",
        25.99M,
        new DateOnly(2025, 6, 25)
    ),
    new(
        3,
        "NFS MV VIII",
        "Racing",
        55.8M,
        new DateOnly(2024, 1, 15)
    ),
    new(
        4,
        "FIFA 25",
        "Sport",
        72M,
        new DateOnly(2025, 10, 15)
    ),

];


app.MapGet("games", () => games);


app.MapGet("games/{id}", (int id) => games.Find((item) => item.Id == id)).WithName(GetGameName);

app.MapPost("games", (CreateGameDto newGame) =>
{

    GameDto game = new(
       games.Count + 1,
       newGame.Name,
       newGame.Genre,
       newGame.Price,
       newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameName, new { id = game.Id }, game);


});

app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) =>
{

    var index = games.FindIndex((item) => item.Id == id);

    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );

    return Results.NoContent();

});

app.MapDelete("games/{id}", (int id) =>
{

    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();

});

app.Run();
