using Microsoft.Extensions.DependencyInjection;
using RoverOnMars;
using RoverOnMars.Interfaces;
using RoverOnMars.Services;
using System;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IRoverService, RoverService>()
    .AddSingleton<IPlanetService, PlanetService>()
    .AddSingleton<IGameService, GameService>()
    .BuildServiceProvider();

var gameService = serviceProvider.GetRequiredService<IGameService>();


gameService.Start();