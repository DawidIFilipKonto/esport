using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using EsportPlayerManager.Repositorys;
using EsportPlayerManager.Services;
using EsportPlayerManager.ViewModels;
using EsportPlayerManager.Views;

namespace EsportPlayerManager;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public static PlayerService PlayerService { get; private set; }
    public static TournamentsService TournamentsService { get; private set; }
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
           
            DisableAvaloniaDataAnnotationValidation();
            var connectionString = """Host=localhost;Port=5432;Username=postgres;Password=haslo123;Database=postgres; """;

            var playerRepository = new PlayerRepository(connectionString);
            PlayerService = new PlayerService(playerRepository);  
            
            var tournamentsRepository = new TournamentsRepository(connectionString);
            TournamentsService = new TournamentsService(tournamentsRepository);
            // _playerViewModel = new PlayerViewModel(playerService);

            _ = playerRepository.InitDb().ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Console.WriteLine($"DB ERROR PLAYER: ${t.Exception}");
                }
            }, TaskScheduler.Default);
            
            
            _ = tournamentsRepository.InitDb().ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Console.WriteLine($"DB ERROR TOURNAMENTS: ${t.Exception}");
                }
            }, TaskScheduler.Default);
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}