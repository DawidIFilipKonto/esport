using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Data.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using EsportPlayerManager.Models;
using EsportPlayerManager.Repositorys;

namespace EsportPlayerManager.Services;

public class PlayerService
{
    private readonly PlayerRepository _playerRepository;


    public PlayerService(PlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        try
        {
            return await _playerRepository.GetAllPlayers();
        }
        catch (Exception e)
        {
            throw new Exception($"get all players err: {e}");
        }
    }

    public async Task AddPlayer(Player player)
    {
        if (
            string.IsNullOrWhiteSpace(player.nickname) ||
            string.IsNullOrWhiteSpace(player.game) ||
            string.IsNullOrWhiteSpace(player.stresslvl) ||
            string.IsNullOrWhiteSpace(player.skilllvl) ||
            string.IsNullOrWhiteSpace(player.fatiguelvl) )
        {
            throw new ArgumentException("nickname and game is reqired");
        }

        try
        {
            int rowsAffected = await _playerRepository.AddPlayer(player);
            if (rowsAffected == 0)
            {
                throw new Exception("Error with adding player");
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Error with adding player {e.Message}");
        }
    }

}