using RoverOnMars.Enums;
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
        public int[] RoverCoordinate {  get; private set; }
        public DirectionsEnum LastDirection { get; set; }

        public Rover(Planet planet, char name, int xInitialCoordinate, int yInitialCoordinate)
        {

            LastDirection = SetRandomDirection();
            _planet = planet;
            Name = name;
            RoverCoordinate = [xInitialCoordinate, yInitialCoordinate];
        }

        private DirectionsEnum SetRandomDirection()
        {
            Random random = new Random();
            var randomDirection = random.Next(1, 5);
            return randomDirection switch
            {
                1 => DirectionsEnum.Right,
                2 => DirectionsEnum.Up,
                3 => DirectionsEnum.Left,
                4 => DirectionsEnum.Down,
                _ => DirectionsEnum.Right
            };
        }

        public int[] GetRoverCoordinate() => RoverCoordinate;

        
    }
}
