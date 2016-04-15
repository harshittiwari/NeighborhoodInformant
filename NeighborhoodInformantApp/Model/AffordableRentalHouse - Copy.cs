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
                    Dictionary<int, int> CommunityCrimeCount = new Dictionary<int, int>();
                    foreach (CrimeData crime in DataMgr.DataMgr.Crimes)
                    {
                        if (CommunityCrimeCount.ContainsKey(crime.CommunityArea))
                        {
                            CommunityCrimeCount[crime.CommunityArea] += 1;
                        }
                        else
                        {
                            CommunityCrimeCount.Add(crime.CommunityArea, 1);
                        }
                    }

                    double crimeIndex = 0;
                    if (CommunityCrimeCount.ContainsKey(CommunityAreaNumber))
                        crimeIndex = 0.35 * (CommunityCrimeCount[CommunityAreaNumber]
                                                        - (DataMgr.DataMgr.Crimes.Count / CommunityCrimeCount.Keys.Count));
                    double busStopIndex = 0.1 * (CaculateStops(1000, DataMgr.DataMgr.BusStops).Count
                                                    - (DataMgr.DataMgr.BusStops.Count / CommunityCrimeCount.Keys.Count));
                    double parkIndex = 1.5 * (CaculateParks(1000).Count
                                                    - (DataMgr.DataMgr.Parks.Count / CommunityCrimeCount.Keys.Count));

                    _rating = 70 - crimeIndex + busStopIndex + parkIndex;
                }
                return _rating;
            }
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

        private int _lastCalculatedDistanceForLibrary = 0;
        private List<Library> _library = new List<Library>();
        public List<Library> Library
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
        public int LibrarysCount
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

        private List<Library> CaculateLibraries(int distance)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<Library> parks = new List<Library>();
            foreach (Library lib in DataMgr.DataMgr.Libraries)
            {
                var eCoord = new GeoCoordinate(lib.Latitude, lib.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    parks.Add(lib);
            }
            return parks;
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
        private List<Utilities> CaculateDistance(int distance, List<Utilities> liUtil)
        {
            var sCoord = new GeoCoordinate(Latitude, Longitude);
            List<Utilities> utils = new List<Utilities>();
            foreach (Park park in liUtil)
            {
                var eCoord = new GeoCoordinate(park.Latitude, park.Longitude);
                double distanceInMeters = sCoord.GetDistanceTo(eCoord);// / 1000;
                if (distanceInMeters <= distance)
                    utils.Add(park);
            }
            return utils;
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
