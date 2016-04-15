using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeighborhoodInformantApp.Model
{
    public class CrimeData : Utilities
    {
        public int ID;
        public string CaseNumber;
        public DateTime Date;
        public string Block;
        public string IUCR;
        public string PrimaryType;
        public string Description;
        public string LocationDescription;
        public bool Arrest;
        public bool Domestic;
        public int Beat;
        public int District;
        public int Ward;
        public int CommunityArea;
        public string FBICode;
        public double XCoordinate;
        public double YCoordinate;
        public int Year;
        public DateTime UpdatedOn;
    }
}
