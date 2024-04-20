using RoverOnMars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars.Services
{
    public class GameService : IGameService
    {
        private readonly IPlanetService _planetService;

        public GameService(IPlanetService planetService)
        {
            _planetService = planetService;
        }
        public void Start()
        {

            PrintWelcomMessage();

            PrintGameRules();

            _planetService.SeedDefaultRovers();

            _planetService.DisplayRovers();

            var rover = _planetService.AddRover();

            _planetService.DisplayRovers();

            _planetService.MoveRover(rover);

            Console.ReadLine();
        }

        private void PrintGameRules()
        {
            Console.WriteLine("Rules of the game:\n" +
                "1- Add your Rover to the shown planet.\n" +
                "3- Give instructions to your Rover. 'L' and 'R' to change the direction, " +
                "'0' to '9' number of steps in last direction.");
        }

        private void PrintWelcomMessage()
        {
            Console.Write("What is your Name? ");

            var name = Console.ReadLine();

            Console.WriteLine($"Welcome {name} to the Rover on Mars game.\n" +
                $"In this game you have a Rover and need to give it instructions to move in the shown planet " +
                $"based on your initial Rover location.");
        }

        
    }
}
