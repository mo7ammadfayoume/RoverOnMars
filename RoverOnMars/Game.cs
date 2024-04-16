using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars
{
    public class Game
    {
        public void Start()
        {
            PrintWelcomMessage();

            PrintGameRules();

            var mars = new Planet();

            SeedDefaultRovers(mars);
            
            DisplayRovers(mars);

            var rover = AddRover(mars);

            DisplayRovers(mars);

            MoveRover(rover, mars);

            Console.ReadLine();
        }

        private void MoveRover(Rover rover, Planet mars)
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
                rover.Move(instructions);
                stop = true;
                DisplayRovers(mars);

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

        private Rover AddRover(Planet mars)
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

                if (IsValidInitialX && IsValidInitialY && mars.IsInsideAndNotReserved(initialX, initialY))
                {
                    var rover = new Rover(mars, char.Parse(roverName), initialX, initialY);
                    mars.AddRoverToPlanet(rover);
                    return rover;
                }
                else
                {
                    Console.WriteLine("Invalid Rover location, please make sure insert valid location");
                    continue;
                }


            } while (true);
        }

        private void SeedDefaultRovers(Planet mars)
        {
            int i = 65;
            while (i < 69)
            {
                Random random = new Random();
                var randomInX = random.Next(mars.XMin, mars.XMax + 1);
                var randomInY = random.Next(mars.YMin, mars.YMax + 1);
                var character = (char)i;
                var rover = new Rover(mars, character, randomInX, randomInY);
                var isRoverAdded = mars.AddRoverToPlanet(rover);
                if (!isRoverAdded) continue;
                i++;
            }
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

        public void DisplayRovers(Planet mars)
        {
            bool isRoverExists = false;
            
            for (int j = mars.YMin; j <= mars.YMax; j++)
            {
                for (int i = mars.XMin; i <= mars.XMax; i++)
                {
                    foreach (var rover in mars.Rovers)
                    {
                        if (rover.GetRoverCoordinate()[0] == i && rover.GetRoverCoordinate()[1] == j)
                        {
                            Console.Write(" " + rover.GetLastDirectionArrow() + rover.Name);
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
    }
}
