using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EsportPlayerManager.Models;
using EsportPlayerManager.Services;
using UserMenager.Helpers;

namespace EsportPlayerManager.ViewModels;

public class TournamentsViewModel: ViewModelBase
{
    private readonly TournamentsService _tournamentsService;
    
    public ObservableCollection<Tournament> Tournaments { get; } = [];

    public TournamentsViewModel()
    {
        _tournamentsService = App.TournamentsService;

        _ = LoadTournaments();
    }
    
    private async Task LoadTournaments()
    {
        try
        {
            var tournamentsDB = await _tournamentsService.GetTournaments();

            Tournaments.Clear();
            Tournaments.AddRange(tournamentsDB);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}