using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EsportPlayerManager.Models;
using Npgsql;

namespace EsportPlayerManager.Repositorys;

public class TournamentsRepository
{
    private readonly string _connectionString;

    public TournamentsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitDb()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync("""
                                      CREATE TABLE IF NOT EXISTS tournaments(
                                          id SERIAL PRIMARY KEY,
                                          name TEXT not null,
                                          entryfree REAL not null,
                                          prizepool REAL not null,
                                          minskill REAL not null 
                                          )
                                      """);
        Console.WriteLine("Tournaments DB init");
    }
    
    public async Task<List<Tournament>> GetTournaments()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var tournament = await connection.QueryAsync<Tournament>("SELECT * FROM tournaments");
        return tournament.ToList();
    }
}