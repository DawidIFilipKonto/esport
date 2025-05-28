using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EsportPlayerManager.Models;
using Npgsql;

namespace EsportPlayerManager.Repositorys;

public class PlayerRepository
{
    private readonly string _connectionString;

    public PlayerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitDb()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync("""
                                          CREATE TABLE IF NOT EXISTS players (
                                          id SERIAL PRIMARY KEY,
                                          nickname TEXT not null,
                                          game int NOT NULL,
                                          skilllvl REAL NOT NULL,
                                          stresslvl REAL NOT NULL,
                                          fatiguelvl REAL NOT NULL
                                          )
                                      """);
        Console.WriteLine("Players DB init");

    }

    public async Task<List<Player>> GetAllPlayers()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var players = await connection.QueryAsync<Player>("SELECT * FROM players");
        return players.ToList();
    }

    public async Task<int> AddPlayer(Player player)
    {

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteAsync("""
                                                   INSERT INTO players(nickname, game, skilllvl, stresslvl, fatiguelvl)
                                                   VALUES (@nickname, @game, @skilllvl, @stresslvl, @fatiguelvl)
                                                   """, player);
    }
}