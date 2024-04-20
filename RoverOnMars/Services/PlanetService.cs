using Mapster;
using RoverOnMars.Enums;
using RoverOnMars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars.Services;

public class PlanetService : IPlanetService
{
    private readonly Planet _planet;
    private readonly IRoverService _roverService;

    public PlanetService(IRoverService roverService)
    {
        _planet = new Planet();
        _roverService = roverService;
    }
    
    public void SeedDefaultRovers()
    {
        int i = 65;
        while (i < 69)
        {
            Random random = new Random();
            var randomInX = random.Next(_planet.XMIN, _planet.XMAX + 1);
            var randomInY = random.Next(_planet.YMIN, _planet.YMAX + 1);
            var character = (char)i;
            var rover = new Rover(_planet, character, randomInX, randomInY);
            var isRoverAdded = AddRoverToPlanet(rover);
            if (!isRoverAdded) continue;
            i++;
        }
    }
    public void DisplayRovers()
    {
        bool isRoverExists = false;

        for (int j = _planet.YMIN; j <= _planet.YMAX; j++)
        {
            for (int i = _planet.XMIN; i <= _planet.XMAX; i++)
            {
                foreach (var rover in _planet.Rovers)
                {
                    if (rover.GetRoverCoordinate()[0] == i && rover.GetRoverCoordinate()[1] == j)
                    {
                        Console.Write(" " + _roverService.GetLastDirectionArrow(rover) + rover.Name);
                        isRoverExists = true;
                    }
                }

                if (!isRoverExists)
                    Console.Write(" . ");

                isRoverExists = false;
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    public Rover AddRover()
    {
        do
        {
            Console.Write("Add your Rover, please make sure the name is one charecter: ");
            var roverName = Console.ReadLine();
            if (roverName.Length > 1)
            {
                Console.WriteLine("Rover name should be ONE character");
                continue;
            }
            Console.Write("Insert Rover location in X direction: ");
            var initialXInput = Console.ReadLine();
            Console.Write("Insert Rover location in Y direction: ");
            var initialYInput = Console.ReadLine();

            var IsValidInitialX = int.TryParse(initialXInput, out var initialX);
            var IsValidInitialY = int.TryParse(initialYInput, out var initialY);

            if (IsValidInitialX && IsValidInitialY && IsInsideAndNotReserved(initialX, initialY))
            {
                var rover = new Rover(_planet, char.Parse(roverName), initialX, initialY);
                AddRoverToPlanet(rover);
                return rover;
            }
            else
            {
                Console.WriteLine("Invalid Rover location, please make sure insert valid location");
                continue;
            }


        } while (true);
    }
    public void MoveRover(Rover rover)
    {
        bool stop = false;
        do
        {
            Console.WriteLine("Please give the instructions for Rover to move, \nex: 'RR3L2'" +
                "this instructions mean: \n1- Turn right twice.\n2- Move 3 steps in the last direction.\n3- Turn to left.\n4- Move two steps in the last direction.");

            repeat:
            Console.Write("Instructions: ");
            var instructionsInput = Console.ReadLine();
            var instructions = instructionsInput.ToCharArray();

            bool isValidInstructions = ValidateInstructions(instructions);

            if (!isValidInstructions)
            {
                Console.WriteLine("Invalid instructions");
                goto repeat;
            }
            Move(rover, instructions);
            stop = true;
            DisplayRovers();

            playAgain:
            Console.Write("Do you want to continue Playing?(y/n): ");

            var continuePlaying = Console.ReadLine();

            if (continuePlaying.ToUpper() == "Y" || continuePlaying.ToUpper() == "YES")
            {
                goto repeat;
            }
            else if (continuePlaying.ToUpper() == "N" || continuePlaying.ToUpper() == "NO")
            {
                Console.WriteLine("Thank you for Playing.");
                stop = true;
            }
            else
            {
                goto playAgain;
            }

        } while (!stop);
    }
    private bool ValidateInstructions(char[] instructions)
    {
        foreach (char instruction in instructions)
        {
            if (!(instruction == 'L' || instruction == 'R' || (instruction >= '0' && instruction <= '9')))
                return false;
        }
        return true;
    }
    private bool AddRoverToPlanet(Rover rover)
    {
        if (IsInsideAndNotReserved(rover.GetRoverCoordinate()[0], rover.GetRoverCoordinate()[1]))
        {
            _planet.Rovers.Add(rover);
            return true;
        }
        else
            return false;
    }
    public bool IsInsideAndNotReserved(int x, int y)
    {
        return (x >= _planet.XMIN && x <= _planet.XMAX && y >= _planet.YMIN && y <= _planet.YMAX) && !(_planet.Rovers.Any(r => r.GetRoverCoordinate()[0] == x && r.GetRoverCoordinate()[1] == y));
    }
    private void Move(Rover rover, char[] instructions)
    {
        foreach (var instruction in instructions)
        {
            bool isDirectionInstruction = instruction == (char)InstructionsEnum.Right || instruction == (char)InstructionsEnum.Left;
            bool isDigit = instruction >= '0' && instruction <= '9';

            if (!isDirectionInstruction && !isDigit)
                throw new Exception($"Invalid Instruction {instruction}");

            if (char.IsLetter(char.ToUpper(instruction)))
            {
                ChangeDirection(rover, instruction);
            }
            else
            {
                MoveSteps(rover, char.ToUpper(instruction));
            }
        }
    }

    private void MoveSteps(Rover rover, char instruction)
    {
        var steps = (int)char.GetNumericValue(instruction);
        _ = rover.LastDirection switch
        {
            DirectionsEnum.Right => IsInsideAndNotReserved(rover.RoverCoordinate[0] + steps, rover.RoverCoordinate[1]) ? rover.RoverCoordinate[0] += steps : 0,
            DirectionsEnum.Left => IsInsideAndNotReserved(rover.RoverCoordinate[0] - steps, rover.RoverCoordinate[1]) ? rover.RoverCoordinate[0] -= steps : 0,
            DirectionsEnum.Up => IsInsideAndNotReserved(rover.RoverCoordinate[0], rover.RoverCoordinate[1] - steps) ? rover.RoverCoordinate[1] -= steps : 0,
            DirectionsEnum.Down => IsInsideAndNotReserved(rover.RoverCoordinate[0], rover.RoverCoordinate[1] + steps) ? rover.RoverCoordinate[1] += steps : 0,
            _ => 0
        };

    }

    private void ChangeDirection(Rover rover, char instruction)
    {
        rover.LastDirection = rover.LastDirection switch
        {
            DirectionsEnum.Right => instruction == (char)InstructionsEnum.Right ? DirectionsEnum.Down : DirectionsEnum.Up,
            DirectionsEnum.Up => instruction == (char)InstructionsEnum.Right ? DirectionsEnum.Right : DirectionsEnum.Left,
            DirectionsEnum.Left => instruction == (char)InstructionsEnum.Right ? DirectionsEnum.Up : DirectionsEnum.Down,
            DirectionsEnum.Down => instruction == (char)InstructionsEnum.Right ? DirectionsEnum.Left : DirectionsEnum.Right,
            _ => DirectionsEnum.Right
        };
    }
}

