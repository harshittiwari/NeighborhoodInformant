using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeighborhoodInformantApp.Model
{
    public class Stop : Utilities
    {
        public string Name;
        public string Snippet;
        public string PointAltitudeMode;

        public override string ToString()
        {
            return Name + " (" + Latitude + "," + Longitude + ")";
        }
    }
}
