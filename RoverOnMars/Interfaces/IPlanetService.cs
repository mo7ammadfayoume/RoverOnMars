using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars.Interfaces
{
    public interface IPlanetService
    {
        
        void SeedDefaultRovers();
        void DisplayRovers();
        Rover AddRover();
        void MoveRover(Rover rover);
        bool IsInsideAndNotReserved(int x, int y);
    }
}
