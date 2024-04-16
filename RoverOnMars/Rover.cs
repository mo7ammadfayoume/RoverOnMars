using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars
{
    public class Rover
    {
        private readonly Planet _planet;
        public char Name {  get; private set; }
        private int[] _roverCoordinate;
        private int _lastDirection;

        public Rover(Planet planet, char name, int xInitialCoordinate, int yInitialCoordinate)
        {

            _lastDirection = SetRandomDirection();
            _planet = planet;
            Name = name;
            _roverCoordinate = [xInitialCoordinate, yInitialCoordinate];
        }

        public int GetLastDirection() => _lastDirection;

        private int SetRandomDirection()
        {
            Random random = new Random();
            var randomDirection = random.Next(1, 5);
            return randomDirection switch
            {
                1 => 0,
                2 => 90,
                3 => 180,
                4 => 270,
                _ => 0
            };
        }

        public int[] GetRoverCoordinate() => _roverCoordinate;

        public void Move(char[] instructions)
        {
            foreach (var instruction in instructions)
            {
                if (char.IsLetter(char.ToUpper(instruction)))
                {
                    ChangeDirection(instruction);
                }
                else
                {
                    MoveSteps(char.ToUpper(instruction));
                }
            }
        }

        private void MoveSteps(char instruction)
        {
            var steps = (int) char.GetNumericValue(instruction);
            _ = _lastDirection switch
            {
                0 => _planet.IsInsideAndNotReserved(_roverCoordinate[0] + steps, _roverCoordinate[1]) ? _roverCoordinate[0] += steps : 0,
                180 => _planet.IsInsideAndNotReserved(_roverCoordinate[0] - steps, _roverCoordinate[1]) ? _roverCoordinate[0] -= steps : 0,
                90 => _planet.IsInsideAndNotReserved(_roverCoordinate[0], _roverCoordinate[1] - steps) ? _roverCoordinate[1] -= steps : 0,
                270 => _planet.IsInsideAndNotReserved(_roverCoordinate[0], _roverCoordinate[1] + steps) ? _roverCoordinate[1] += steps : 0,
                _ => 0
            };
            
        }

        private void ChangeDirection(char instruction)
        {
            if (_lastDirection == 0 && instruction == 'R')
                _lastDirection = 360 - 90;

            else
            {
                if (instruction == 'R')
                    _lastDirection -= 90;
                else if (instruction == 'L')
                    _lastDirection += 90;
            }

            if (_lastDirection == 360)
                _lastDirection = 0;
        }

        public char GetLastDirectionArrow()
        {
            return _lastDirection switch
            {
                0 => '>',
                90 => '^',
                180 => '<',
                270 => 'v',
                _ => '>'
            };
        }
    }
}
