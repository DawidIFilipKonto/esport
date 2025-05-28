using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsportPlayerManager.Models;
using EsportPlayerManager.Repositorys;

namespace EsportPlayerManager.Services;

public class TournamentsService
{
    private readonly TournamentsRepository _tournamentsRepository;

    public TournamentsService(TournamentsRepository tournamentsRepository)
    {
        _tournamentsRepository = tournamentsRepository;
    }
    
    public async Task<List<Tournament>> GetTournaments()
    {
        try
        {
            return await _tournamentsRepository.GetTournaments();
        }
        catch (Exception e)
        {
            throw new Exception($"get all Tournaments err: {e}");
        }
    }
}