using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerManager.Models;
using EsportPlayerManager.Services;
using UserMenager.Helpers;

namespace EsportPlayerManager.ViewModels;

public partial class PlayersViewModel: ViewModelBase
{
    private readonly PlayerService _playerService;

    public ICommand AddPlayerCommand { get; }
    
    [ObservableProperty] private string _nickName = string.Empty;
    [ObservableProperty] private string _game = string.Empty;
    [ObservableProperty] private string _skillLevel = string.Empty;
    [ObservableProperty] private string _stressLevel = string.Empty;
    [ObservableProperty] private string _fatigueLevel = string.Empty;

    public ObservableCollection<Player> Players { get; } = [];
    
    public PlayersViewModel()
    {
        _playerService = App.PlayerService;

        AddPlayerCommand = new AsyncRelayCommand(AddPlayer);

        _ = LoadPlayers();
    }

    private async Task LoadPlayers()
    {
        try
        {
            var playersDB = await _playerService.GetAllPlayers();

            Players.Clear();
            Players.AddRange(playersDB);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddPlayer()
    {
        try
        {

                var player = new Player
                {
                    nickname = NickName.Trim(),
                    game = Game.Trim(),
                    skilllvl = SkillLevel.Trim(),
                    stresslvl = StressLevel.Trim(),
                    fatiguelvl = FatigueLevel.Trim()
                };


                await _playerService.AddPlayer(player);
                Players.Add(player);
                ClearFields();
            
        }
        catch (Exception e)
        {
            throw new Exception($"{e.Message}");
        }
    }
    
    private void ClearFields()
    {
        NickName = string.Empty;
        Game = string.Empty;
        SkillLevel = string.Empty;
        StressLevel = string.Empty;
        FatigueLevel = string.Empty;
    }
}