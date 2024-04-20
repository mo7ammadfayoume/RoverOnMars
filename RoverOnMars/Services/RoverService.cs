using RoverOnMars.Enums;
using RoverOnMars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars.Services
{
    public class RoverService : IRoverService
    {

        public RoverService()
        {
        }

        public char GetLastDirectionArrow(Rover rover)
        {
            return rover.LastDirection switch
            {
                DirectionsEnum.Right => '>',
                DirectionsEnum.Up => '^',
                DirectionsEnum.Left => '<',
                DirectionsEnum.Down => 'v',
                _ => '>'
            };

        }
        
    }
}
