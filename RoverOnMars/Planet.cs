using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverOnMars
{
    public class Planet
    {
        public int XMIN { get; } = 1;
        public int XMAX { get; } = 20;
        public int YMIN { get; } = 1;
        public int YMAX { get; } = 20;
        public ICollection<Rover> Rovers { get; private set; }
        public Planet()
        {
            Rovers = new HashSet<Rover>();
        }
        
        

    }
}
