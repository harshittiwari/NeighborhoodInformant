using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighborhoodInformantApp.Model
{
    public class AffordableRentalHouse
    {
        public int id;
        public string CommunityAreaName;
        public int CommunityAreaNumber;
        public string PropertyType;
        public string PropertyName;
        public string Address;
        public int ZipCode;
        public string PhoneNumber;
        public string ManagementCompany;
        public int Units;
        public double XCoordinate;
        public double YCoordinate;
        public double Latitude;
        public double Longitude;

        public int CrimeRate
        {
            get
            {
                return DataMgr.DataMgr.CommunityCrimeRating[CommunityAreaNumber];
            }
        }

        private double _rating = -1;
        public double Rating
        {
            get
            {
                if (_rating == -1)
                {
                    double crimeIndex = DataMgr.DataMgr.CommunityCrimeRating[CommunityAreaNumber] * 2;

                    double busStopIndex = getIndex(CaculateStops(500, DataMgr.DataMgr.BusStops).Count);
                    double metraStopIndex = getIndex(CaculateStops(1000, DataMgr.DataMgr.MetraStops).Count);
                    double trainStopIndex = getIndex(CaculateStops(1000, DataMgr.DataMgr.TrainLStops).Count);
                    double divvyStopIndex = getIndex(CaculateDivvyStations(1000).Count);

                    int criteria = CaculateParks(1000).Count;
                    double parkIndex = 0;
                    if (criteria > 5)
                        parkIndex = 5;
                    else
                        parkIndex = criteria;

                    criteria = CaculateGroceryStores(2000).Count;
                    double groceryIndex = 0;
                    if (criteria > 5)
                        groceryIndex = 5;
                    else
                        groceryIndex = criteria;

                    criteria = CaculateSchools(1000).Count;
                    double schoolIndex = 0;
                    if (criteria > 5)
                        schoolIndex = 5;
                    else
                        schoolIndex = criteria;

                    criteria = CaculateLibraries(1000).Count;
                    double libraryIndex = 0;
                    if (criteria > 5)
                        libraryIndex = 5;
                    else
                        libraryIndex = criteria;

                    double normalizedIndex = busStopIndex + metraStopIndex + trainStopIndex + divvyStopIndex + parkIndex + groceryIndex + schoolIndex + libraryIndex;

                    if (crimeIndex > 7)
                        _rating = 40 + crimeIndex + normalizedIndex;
                    else if (crimeIndex < 5)
                        _rating = 60 + crimeIndex + normalizedIndex;
                    else
                        _rating = 50 + crimeIndex + normalizedIndex;
                }
                return _rating;
            }
        }

        private static double getIndex(int criteria)
        {
            double trainStopIndex = 0;
            if (criteria > 5)
                trainStopIndex = 2.5;
            else if (criteria > 3)
                trainStopIndex = 1.75;
            else if (criteria > 1)
                trainStopIndex = 1;
            else if (criteria > 0)
                trainStopIndex = 0.5;
            return trainStopIndex;
        }

        private int _lastCalculatedDistanceForTrainLStop = 0;
        private List<Stop> _trainLStops = new List<Stop>();
        public List<Stop> TrainLStops
        {
            get
            {
                if (_lastCalculatedDistanceForTrainLStop == DataMgr.DataMgr.Distance)
                    return _trainLStops;

                _lastCalculatedDistanceForTrainLStop = DataMgr.DataMgr.Distance;

                _trainLStops = CaculateStops(_lastCalculatedDistanceForTrainLStop, DataMgr.DataMgr.TrainLStops);
                return _trainLStops;
            }
        }
        public int TrainLStopsCount
        {
            get
            {
                if (_lastCalculatedDistanceForTrainLStop == DataMgr.DataMgr.Distance)
                    return _trainLStops.Count;

                _lastCalculatedDistanceForTrainLStop = DataMgr.DataMgr.Distance;

                _trainLStops = CaculateStops(_lastCalculatedDistanceForTrainLStop, DataMgr.DataMgr.TrainLStops);
                return _trainLStops.Count;
            }
        }

        private int _lastCalculatedDistanceForBusStop = 0;
        private List<Stop> _busStops = new List<Stop>();
        public List<Stop> BusStops
        {
            get
            {
                if (_lastCalculatedDistanceForBusStop == DataMgr.DataMgr.Distance)
                    return _busStops;

                _lastCalculatedDistanceForBusStop = DataMgr.DataMgr.Distance;

                _busStops = CaculateStops(_lastCalculatedDistanceForBusStop, DataMgr.DataMgr.BusStops);
                return _busStops;
            }
        }
        public int BusStopsCount
        {
            get
            {
                if (_lastCalculatedDistanceForBusStop == DataMgr.DataMgr.Distance)
                    return _busStops.Count;

                _lastCalculatedDistanceForBusStop = DataMgr.DataMgr.Distance;

                _busStops = CaculateStops(_lastCalculatedDistanceForBusStop, DataMgr.DataMgr.BusStops);
                return _busStops.Count;
            }
        }

        private int _lastCalculatedDistanceForMetraStop = 0;
        private List<Stop> _metraStops = new List<Stop>();
        public List<Stop> MetraStops
        {
            get
            {
                if (_lastCalculatedDistanceForMetraStop == DataMgr.DataMgr.Distance)
                    return _metraStops;

                _lastCalculatedDistanceForMetraStop = DataMgr.DataMgr.Distance;

                _metraStops = CaculateStops(_lastCalculatedDistanceForMetraStop, DataMgr.DataMgr.MetraStops);
                return _metraStops;
            }
        }
        public int MetraStopsCount
        {
            get
            {
                if (_lastCalculatedDistanceForMetraStop == DataMgr.DataMgr.Distance)
                    return _metraStops.Count;

                _lastCalculatedDistanceForMetraStop = DataMgr.DataMgr.Distance;

                _metraStops = CaculateStops(_lastCalculatedDistanceForMetraStop, DataMgr.DataMgr.MetraStops);
                return _metraStops.Count;
            }
        }

        private int _lastCalculatedDistanceForDivvyStation = 0;
        private List<DivvyStation> _divvyStations = new List<DivvyStation>();
        public List<DivvyStation> DivvyStations
        {
            get
            {
                if (_lastCalculatedDistanceForDivvyStation == DataMgr.DataMgr.Distance)
                    return _divvyStations;

                _lastCalculatedDistanceForDivvyStation = DataMgr.DataMgr.Distance;

                _divvyStations = CaculateDivvyStations(_lastCalculatedDistanceForDivvyStation);
                return _divvyStations;
            }
        }
        public int DivvyStationsCount
        {
            get
            {
                if (_lastCalculatedDistanceForDivvyStation == DataMgr.DataMgr.Distance)
                    return _divvyStations.Count;

                _lastCalculatedDistanceForDivvyStation = DataMgr.DataMgr.Distance;

                _divvyStations = CaculateDivvyStations(_lastCalculatedDistanceForDivvyStation);
                return _divvyStations.Count;
            }
        }        

        private int _lastCalculatedDistanceForLibrary = 0;
        private List<Library> _library = new List<Library>();
        public List<Library> Libraries
        {
            get
            {
                if (_lastCalculatedDistanceForLibrary == DataMgr.DataMgr.Distance)
                    return _library;

                _lastCalculatedDistanceForLibrary = DataMgr.DataMgr.Distance;

                _library = CaculateLibraries(_lastCalculatedDistanceForLibrary);
                return _library;
            }
        }       
        public int LibrariesCount
        {
            get
            {
                if (_lastCalculatedDistanceForLibrary == DataMgr.DataMgr.Distance)
                    return _library.Count;

                _lastCalculatedDistanceForLibrary = DataMgr.DataMgr.Distance;

                _library = CaculateLibraries(_lastCalculatedDistanceForLibrary);
                return _library.Count;
            }
        }

        private int _lastCalculatedDistanceForSchool = 0;
        private List<School> _school = new List<School>();
        public List<School> Schools
        {
            get
            {
                if (_lastCalculatedDistanceForSchool == DataMgr.DataMgr.Distance)
                    return _school;

                _lastCalculatedDistanceForSchool = DataMgr.DataMgr.Distance;

                _school = CaculateSchools(_lastCalculatedDistanceForSchool);
                return _school;
            }
        }        
        public int SchoolsCount
        {
            get
            {
                if (_lastCalculatedDistanceForSchool == DataMgr.DataMgr.Distance)
                    return _school.Count;

                _lastCalculatedDistanceForSchool = DataMgr.DataMgr.Distance;

                _school = CaculateSchools(_lastCalculatedDistanceForSchool);
                return _school.Count;
            }
        }

        private int _lastCalculatedDistanceForPark = 0;
        private List<Park> _parks = new List<Park>();
        public List<Park> Parks
        {
            get
            {
                if (_lastCalculatedDistanceForPark == DataMgr.DataMgr.Distance)
                    return _parks;

                _lastCalculatedDistanceForPark = DataMgr.DataMgr.Distance;

                _parks = CaculateParks(_lastCalculatedDistanceForPark);
                return _parks;
            }
        }
        public int ParksCount
        {
            get
            {
                if (_lastCalculatedDistanceForPark == DataMgr.DataMgr.Distance)
                    return _parks.Count;

                _lastCalculatedDistanceForPark = DataMgr.DataMgr.Distance;
                
                _parks = CaculateParks(_lastCalculatedDistanceForPark);
                return _parks.Count;
            }
        }

        private int _lastCalculatedDistanceForGroceryStore = 0;
        private List<GroceryStore> _groceryStores = new List<GroceryStore>();
        public List<GroceryStore> GroceryStores
        {
            get
            {
                if (_lastCalculatedDistanceForGroceryStore == DataMgr.DataMgr.Distance)
                    return _groceryStores;

                _lastCalculatedDistanceForGroceryStore = DataMgr.DataMgr.Distance;

                _groceryStores = CaculateGroceryStores(_lastCalculatedDistanceForGroceryStore);
                return _groceryStores;
            }
        }
        public int GroceryStoresCount
        {
            get
            {
                if (_lastCalculatedDistanceForGroceryStore == DataMgr.DataMgr.Distance)
                    return _groceryStores.Count;

                _lastCalculatedDistanceForGroceryStore = DataMgr.DataMgr.Distance;

                _groceryStores = CaculateGroceryStores(_lastCalculatedDistanceForGroceryStore);
                return _groceryStores.Count;
            }
        }

        private List<GroceryStore> CaculateGroceryStores(int distance)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<GroceryStore> li = new List<GroceryStore>();
            foreach (GroceryStore lib in DataMgr.DataMgr.GroceryStores)
            {
                var eCoord = new GeoCoordinate(lib.Latitude, lib.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    li.Add(lib);
            }
            return li;
        }
        private List<DivvyStation> CaculateDivvyStations(int distance)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<DivvyStation> li = new List<DivvyStation>();
            foreach (DivvyStation lib in DataMgr.DataMgr.DivvyStations)
            {
                var eCoord = new GeoCoordinate(lib.Latitude, lib.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    li.Add(lib);
            }
            return li;
        }
        private List<School> CaculateSchools(int distance)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<School> li = new List<School>();
            foreach (School lib in DataMgr.DataMgr.Schools)
            {
                var eCoord = new GeoCoordinate(lib.Latitude, lib.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    li.Add(lib);
            }
            return li;
        }
        private List<Library> CaculateLibraries(int distance)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<Library> libs = new List<Library>();
            foreach (Library lib in DataMgr.DataMgr.Libraries)
            {
                var eCoord = new GeoCoordinate(lib.Latitude, lib.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    libs.Add(lib);
            }
            return libs;
        }
        private List<Stop> CaculateStops(int distance, List<Stop> liStop)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<Stop> stops = new List<Stop>();
            foreach (Stop stop in liStop)
            {
                var eCoord = new GeoCoordinate(stop.Latitude, stop.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    stops.Add(stop);
            }
            return stops;
        }
        private List<Park> CaculateParks(int distance)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<Park> parks = new List<Park>();
            foreach (Park park in DataMgr.DataMgr.Parks)
            {
                var eCoord = new GeoCoordinate(park.Latitude, park.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    parks.Add(park);
            }
            return parks;
        }
    }
}
