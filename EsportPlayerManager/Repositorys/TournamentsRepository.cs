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

  
    
    public async Task<List<Tournament>> GetTournaments()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var tournament = await connection.QueryAsync<Tournament>("SELECT * FROM tournaments");
        return tournament.ToList();
    }
}