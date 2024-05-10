using GameStore.API.DTOs;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameName = "GetName";
    private static readonly List<GameDto> games = [
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

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);


        // GetGameName /games/1
        group.MapGet("/{id}", (int id) =>
        {

            GameDto? game = games.Find((item) => item.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameName);

        group.MapPost("/", (CreateGameDto newGame) =>
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

        // POST /games/id
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {

            var index = games.FindIndex((item) => item.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );

            return Results.NoContent();

        });

        group.MapDelete("/{id}", (int id) =>
        {

            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();

        });
        return group;
    }
}
