using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars
{
    public class Planet
    {
        public int XMin { get; } = 1;
        public int XMax { get; } = 20;
        public int YMin { get; } = 1;

        public int YMax { get; } = 20;
        public ICollection<Rover> Rovers { get; private set; }
        public Planet()
        {
            Rovers = new HashSet<Rover>();
        }
        public bool IsInsideAndNotReserved(int x, int y)
        {
            return (x >= XMin && x <= XMax && y >= YMin && y <= YMax) && !(Rovers.Any(r => r.GetRoverCoordinate()[0] == x && r.GetRoverCoordinate()[1] == y));
        }
        public bool AddRoverToPlanet(Rover rover)
        {
            if (IsInsideAndNotReserved(rover.GetRoverCoordinate()[0], rover.GetRoverCoordinate()[1]))
            {
                Rovers.Add(rover);
                return true;
            }
            else
                return false;
        }

    }
}
