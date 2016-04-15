using NeighborhoodInformantApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;

namespace NeighborhoodInformantApp.DataMgr
{
    public static class DataMgr
    {
        public static User user;
        public static List<AffordableRentalHouse> Houses;
        public static List<CrimeData> Crimes;
        public static List<PoliceStation> PoliceStations;
        public static List<Stop> BusStops;

        internal static List<int> GetFavouriteHouses(string _userName)
        {
            //todo asap
            return DataLogic.getFavorites(_userName);
        }

        internal static Dictionary<string, string> GetFilters(string _userName)
        {
            //todo asap
            Dictionary<string, string> Filter = DataLogic.getFilters(_userName);

            if (!Filter.ContainsKey("CommunityName"))
                Filter.Add("CommunityName", "");
            if (!Filter.ContainsKey("Zip"))
                Filter.Add("Zip", "");
            if (!Filter.ContainsKey("PropertyType"))
                Filter.Add("PropertyType", "");
            if (!Filter.ContainsKey("Address"))
                Filter.Add("Address", "");
            if (!Filter.ContainsKey("Distance"))
                Filter.Add("Distance", "");
            if (!Filter.ContainsKey("CrimeIndex"))
                Filter.Add("CrimeIndex", "");
            if (!Filter.ContainsKey("Radius"))
                Filter.Add("Radius", "");

            return Filter;
        }

        internal static void inputFilters(string username, Dictionary<string, string> filter)
        {
            //todo
            DataLogic.setFilters(username, filter);
        }

        internal static bool CheckLogin(string username, string password)
        {
            // fix this asap
            return DataLogic.checkLogin(username, password);
            //return true;
        }

        internal static void UpdatePassWord(string username, string password)
        {
            //todo asap
            DataLogic.updatePassword(username, password);
        }

        internal static bool Validate(string username, string sq, string sa)
        {
            // todo asap
            return DataLogic.checkSecurity(username, sq, sa);
        }

        internal static bool IsUserExists(string username)
        {
            // todo asap
            return DataLogic.isUserExistent(username);
        }

        internal static void CreateNewUser(string username, string password, string sq, string sa)
        {
            //todo
            DataLogic.createNewUser(username, sq, sa, password);
        }

        internal static void inputFavorites(string username,List<int> houseids)
        {
            // todo
            DataLogic.inputFavorites(username, houseids);
        }

        public static List<Stop> TrainLStops;
        public static List<Stop> MetraStops;
        public static List<Park> Parks;
        public static List<Library> Libraries;
        public static List<GroceryStore> GroceryStores;
        public static List<School> Schools;
        public static List<DivvyStation> DivvyStations;
        public static Dictionary<String, List<int>> NeighBorHoodToZipCode;
        
        public static Dictionary<String, String> NeighBorHoodToCommunityArea;
        public static Dictionary<int, int> CommunityCrimeRating;
        public static int Distance = 100;
        public static Dictionary<string, DateTime> UpdatedOn;

        internal static void SetupDB()
        {
            //SetupNeighBorHoodToZipCode();
            //SetupNeighBorHoodToCommunityArea();

            // get updated on from db
            UpdatedOn = DataLogic.FetchUpdateLog(); //new Dictionary<string, DateTime>();
            
            BusStops = FetchStops(@"Resources/busStops.kml");
            TrainLStops = FetchStops(@"Resources/CTARailStations.kml");
            MetraStops = FetchStops(@"Resources/Metrastations.kml");
            Libraries = WebDataMgr.FetchLibraries();
            GroceryStores = WebDataMgr.FetchGroceryStores();
            Schools = WebDataMgr.FetchSchools();
            DivvyStations = WebDataMgr.FetchDivvyStations();
            Parks = WebDataMgr.FetchParks();
            Houses = WebDataMgr.FetchHouses();
            Crimes = WebDataMgr.FetchCrimeData();
            PoliceStations = WebDataMgr.FetchPoliceStations();
            
            #region get/set
            #region DivvyStations
            // to be merged
            if (DivvyStations == null)
            {
                //get DivvyStations from local db here
                DivvyStations = DataLogic.FetchDivvyStations();
            }
            else
            {
                // delete all data from db and insert from DivvyStations here
                DataAccess.PopulateDivvyStationsTable(DivvyStations);
                DataLogic.updateLog("DivvyStation", UpdatedOn["DivvyStation"].ToString());
            }
            #endregion
            #region GroceryStores
            // to be merged
            if (GroceryStores == null)
            {
                //get GroceryStores from local db here
                GroceryStores = DataLogic.FetchGroceryStores();
            }
            else
            {
                // delete all data from db and insert from GroceryStores here
                DataAccess.PopulateGroceryStoresTable(GroceryStores);
                DataLogic.updateLog("GroceryStore", UpdatedOn["GroceryStore"].ToString());
            }
            #endregion
            #region Schools
            // to be merged
            if (Schools == null)
            {
                //get Schools from local db here
                Schools = DataLogic.FetchSchools();
            }
            else
            {
                // delete all data from db and insert from Schools here
                DataAccess.PopulateSchoolsTable(Schools);
                DataLogic.updateLog("School", UpdatedOn["School"].ToString());
            }
            #endregion
            #region Library
            // to be merged
            if (Libraries == null)
            {
                //get Libraries from local db here
                Libraries = DataLogic.FetchLibraries();
            }
            else
            {
                // delete all data from db and insert from Libraries here
                DataAccess.PopulateLibrariesTable(Libraries);
                DataLogic.updateLog("Library", UpdatedOn["Library"].ToString());
            }
            #endregion
            #region parks
            // to be merged
            if (Parks == null)
            {
                //get parks from local db here
                Parks = DataLogic.FetchParks();
            }
            else
            {
                // delete all data from db and insert from Parks here
                DataAccess.PopulateParksTable(Parks);
                DataLogic.updateLog("Park", UpdatedOn["Park"].ToString());
            }
            #endregion
            #region houses
            if (Houses == null)
            {
                //get Houses from local db here
                Houses = DataLogic.FetchHouses();
            }
            else
            {
                // delete all data from db and insert from Houses here
                DataAccess.PopulateHousesTable(Houses);
                Houses = DataLogic.FetchHouses();
                DataLogic.updateLog("House", UpdatedOn["House"].ToString());
            }
            #endregion
            #region crime
            if (Crimes == null)
            {
                //get Crimes from local db here
                Crimes = DataLogic.FetchCrimeData();
            }
            else
            {
                // delete all data from db and insert from Crimes here
                DataAccess.PopulateCrimesTable(Crimes);
                DataLogic.updateLog("Crime", UpdatedOn["Crime"].ToString());
            }
            #endregion
            #region policestation
            if (PoliceStations == null)
            {
                //get PoliceStations from local db here
                PoliceStations = DataLogic.FetchPoliceStations();
            }
            else
            {
                // delete all data from db and insert from PoliceStations here
                DataAccess.PopulatePoliceStationsTable(PoliceStations);
                DataLogic.updateLog("Police", UpdatedOn["Police"].ToString());
            }
            #endregion
            // to be merged - end
            #endregion

            List<string> type1 = new List<string>() { "KIDNAPPING",
                                                        "HOMICIDE",
                                                        "WEAPONS VIOLATION",
                                                        "PROSTITUTION",
                                                        "SEX OFFENSE",
                                                        "ARSON",
                                                        "OFFENSE INVOLVING CHILDREN",
                                                        "CRIM SEXUAL ASSAULT",
                                                        "ASSAULT"};
            List<string> type2 = new List<string>() {"BATTERY",
                                                        "BURGLARY",
                                                        "ROBBERY",
                                                        "THEFT",
                                                        "MOTOR VEHICLE THEFT",
                                                        "CRIMINAL TRESPASS",
                                                        "CRIMINAL DAMAGE",
                                                        "NARCOTICS" };

            List<string> type3 = new List<string>() { "PUBLIC PEACE VIOLATION",
                                                        "NON - CRIMINAL",
                                                        "INTERFERENCE WITH PUBLIC OFFICER",
                                                        "OTHER OFFENSE",
                                                        "STALKING",
                                                        "LIQUOR LAW VIOLATION",
                                                        "DECEPTIVE PRACTICE"};

            Dictionary<int, int> CommunityCrimeCount1 = new Dictionary<int, int>();
            Dictionary<int, int> CommunityCrimeCount2 = new Dictionary<int, int>();
            Dictionary<int, int> CommunityCrimeCount3 = new Dictionary<int, int>();
            foreach (CrimeData crime in Crimes)
            {
                Dictionary<int, int> CommunityCrimeCount;

                if (type1.Contains(crime.PrimaryType))
                    CommunityCrimeCount = CommunityCrimeCount1;
                else if (type2.Contains(crime.PrimaryType))
                    CommunityCrimeCount = CommunityCrimeCount2;
                else
                    CommunityCrimeCount = CommunityCrimeCount3;

                if (CommunityCrimeCount.ContainsKey(crime.CommunityArea))
                {
                    CommunityCrimeCount[crime.CommunityArea] += 1;
                }
                else
                {
                    CommunityCrimeCount.Add(crime.CommunityArea, 1);
                }
            }

            CommunityCrimeRating = new Dictionary<int, int>();
            foreach (int communityArea in Houses.Select(h => h.CommunityAreaNumber).Distinct())
            {
                CommunityCrimeRating.Add(communityArea, 1);
                if (CommunityCrimeCount1.ContainsKey(communityArea))
                {
                    if (CommunityCrimeCount1[communityArea] > 8)
                        CommunityCrimeRating[communityArea] = 5;
                    else if (CommunityCrimeCount1[communityArea] > 4)
                        CommunityCrimeRating[communityArea] = 4;
                    else
                    {
                        if (CommunityCrimeCount2.ContainsKey(communityArea))
                        {
                            if (CommunityCrimeCount2[communityArea] > 25)
                                CommunityCrimeRating[communityArea] = 5;
                            else if (CommunityCrimeCount2[communityArea] > 15)
                                CommunityCrimeRating[communityArea] = 4;
                            else 
                                CommunityCrimeRating[communityArea] = 3;
                        }

                        else
                            CommunityCrimeRating[communityArea] = 3;
                    }
                }
                else if (CommunityCrimeCount2.ContainsKey(communityArea))
                {
                    if (CommunityCrimeCount2[communityArea] > 30)
                        CommunityCrimeRating[communityArea] = 5;
                    else if (CommunityCrimeCount2[communityArea] > 20)
                        CommunityCrimeRating[communityArea] = 4;
                    else if (CommunityCrimeCount2[communityArea] > 10)
                        CommunityCrimeRating[communityArea] = 3;
                    else if (CommunityCrimeCount2[communityArea] > 5)
                        CommunityCrimeRating[communityArea] = 2;
                    else if (CommunityCrimeCount3.ContainsKey(communityArea))
                    {
                        if (CommunityCrimeCount3[communityArea] > 30)
                            CommunityCrimeRating[communityArea] = 4;
                        else if (CommunityCrimeCount3[communityArea] > 25)
                            CommunityCrimeRating[communityArea] = 3;
                        else if (CommunityCrimeCount3[communityArea] > 10)
                            CommunityCrimeRating[communityArea] = 2;
                    }
                }
                else if (CommunityCrimeCount3.ContainsKey(communityArea))
                {
                    if (CommunityCrimeCount3[communityArea] > 30)
                        CommunityCrimeRating[communityArea] = 4;
                    else if (CommunityCrimeCount3[communityArea] > 25)
                        CommunityCrimeRating[communityArea] = 3;
                    else if (CommunityCrimeCount3[communityArea] > 10)
                        CommunityCrimeRating[communityArea] = 2;
                }
            }
            //foreach (KeyValuePair<int, int> kp in CommunityCrimeCount)
            //    if (kp.Value >= 40)
            //        CommunityCrimeRating[kp.Key] = 5;
            //    else if (kp.Value <= 39 && kp.Value >= 31)
            //        CommunityCrimeRating[kp.Key] = 4;
            //    else if (kp.Value <= 30 && kp.Value >= 15)
            //        CommunityCrimeRating[kp.Key] = 3;
            //    else if (kp.Value <= 14 && kp.Value >= 5)
            //        CommunityCrimeRating[kp.Key] = 2;
            //    else
            //        CommunityCrimeRating[kp.Key] = 1;
        }

        public static bool SendMail(string from, string to, string pwd, string subject, string body, string attachmentPath="")
        {
            bool isMailSent = false;

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.Body = body;

                if (attachmentPath.Length > 0)
                    message.Attachments.Add(new Attachment(attachmentPath));

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from, pwd);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                isMailSent = true;
            }
            catch (Exception)
            {
                isMailSent = false;
            }

            return isMailSent;
        }

        public static IEnumerable<Stop> StreamBusStops(string uri)
        {
            using (XmlReader reader = XmlReader.Create(uri))
            {
                string name = null;
                string snippet = null;
                string altitudeMode = null;
                double latidude = 0.0;
                double longitude = 0.0;

                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name == "Placemark")
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == "name")
                            {
                                name = reader.ReadString();
                                break;
                            }
                        }
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == "Snippet")
                            {
                                snippet = reader.ReadString();
                                break;
                            }
                        }
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == "altitudeMode")
                            {
                                altitudeMode = reader.ReadString();
                                break;
                            }
                        }
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == "coordinates")
                            {
                                string str = reader.ReadString();
                                string[] v = str.Split(',');
                                double.TryParse(v[1].Trim(), out latidude);
                                double.TryParse(v[0].Trim(), out longitude);
                                break;
                            }
                        }
                        yield return new Stop() { Name = name, Snippet = snippet, PointAltitudeMode = altitudeMode, Latitude = latidude, Longitude = longitude };
                    }
                }
            }
        }

        private static List<Stop> FetchStops(string uri)
        {
            //StringBuilder sb = new StringBuilder();
            //System.IO.StreamReader file = new System.IO.StreamReader(@"doc1.kml");
            //sb.Append(file.ReadToEnd());
            //file.Close();

            //var doc = XDocument.Load("doc1.kml");

            List<Stop> busStops = new List<Stop>();

            foreach (var busStop in StreamBusStops(uri))
            {
                busStops.Add(busStop);
            }
            return busStops;

        }

        public static DataTable GetHousesDT(IEnumerable<AffordableRentalHouse> housesToDisplay)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Favourite", typeof(bool)));
            //dt.Columns.Add("Neighborhood");
            dt.Columns.Add("Rating");
            dt.Columns.Add("CommunityAreaName");
            dt.Columns.Add("CommunityAreaNumber");
            dt.Columns.Add("PropertyType");
            dt.Columns.Add("PropertyName");
            dt.Columns.Add("Address");
            dt.Columns.Add("ZipCode");
            dt.Columns.Add("CrimeRate");
            dt.Columns.Add("BusStops");
            dt.Columns.Add("Parks");
            dt.Columns.Add("TrainLStops");
            dt.Columns.Add("MetraStops");
            dt.Columns.Add("Library");
            dt.Columns.Add("GroceryStores");
            dt.Columns.Add("Schools");
            dt.Columns.Add("DivvyStands");
            dt.Columns.Add("PhoneNumber");
            dt.Columns.Add("ManagementCompany");
            dt.Columns.Add("Units");
            dt.Columns.Add("XCoordinate");
            dt.Columns.Add("YCoordinate");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            foreach (AffordableRentalHouse house in housesToDisplay)
            {
                DataRow dr = dt.NewRow();
                //dr["Neighborhood"] = house.NeighborhoodName;
                if (DataMgr.user.favouriteHouses.Contains(house.id))
                    dr["Favourite"] = true;
                else
                    dr["Favourite"] = false;
                dr["Rating"] = house.Rating;
                dr["CommunityAreaName"] = house.CommunityAreaName;
                dr["CommunityAreaNumber"] = house.CommunityAreaNumber;
                dr["PropertyType"] = house.PropertyType;
                dr["PropertyName"] = house.PropertyName;
                dr["Address"] = house.Address;
                dr["ZipCode"] = house.ZipCode;
                dr["CrimeRate"] = house.CrimeRate;
                dr["BusStops"] = house.BusStopsCount;
                dr["Parks"] = house.ParksCount;
                dr["TrainLStops"] = house.TrainLStopsCount;
                dr["MetraStops"] = house.MetraStopsCount;
                dr["Library"] = house.LibrariesCount;
                dr["GroceryStores"] = house.GroceryStoresCount;
                dr["Schools"] = house.SchoolsCount;
                dr["DivvyStands"] = house.DivvyStationsCount;
                dr["PhoneNumber"] = house.PhoneNumber;
                dr["ManagementCompany"] = house.ManagementCompany;
                dr["Units"] = house.Units;
                dr["XCoordinate"] = house.XCoordinate;
                dr["YCoordinate"] = house.YCoordinate;
                dr["Latitude"] = house.Latitude;
                dr["Longitude"] = house.Longitude;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        // http://www.dreamtown.com/maps/chicago-zipcode-map.html
        private static void SetupNeighBorHoodToZipCode()
        {
            NeighBorHoodToZipCode = new Dictionary<string, List<int>>();
            NeighBorHoodToZipCode.Add("Albany Park", new List<int>() { 60625 });
            NeighBorHoodToZipCode.Add("Altgeld Gardens", new List<int>() { 60827 });
            NeighBorHoodToZipCode.Add("Andersonville", new List<int>() { 60640 });
            NeighBorHoodToZipCode.Add("Arcadia Terrace", new List<int>() { 60659 });
            NeighBorHoodToZipCode.Add("Archer Heights", new List<int>() { 60632 });
            NeighBorHoodToZipCode.Add("Ashburn", new List<int>() { 60652 });
            NeighBorHoodToZipCode.Add("Avalon Park", new List<int>() { 60619 });
            NeighBorHoodToZipCode.Add("Avondale", new List<int>() { 60618 });
            NeighBorHoodToZipCode.Add("Back of the Yards", new List<int>() { 60609 });
            NeighBorHoodToZipCode.Add("Belmont Central", new List<int>() { 60639, 60634 });
            NeighBorHoodToZipCode.Add("Belmont Gardens", new List<int>() { 60641, 60639 });
            NeighBorHoodToZipCode.Add("Belmont Heights", new List<int>() { 60634 });
            NeighBorHoodToZipCode.Add("Belmont Terrace", new List<int>() { 60634 });
            NeighBorHoodToZipCode.Add("Beverly", new List<int>() { 60620, 60643 });
            NeighBorHoodToZipCode.Add("Beverly View", new List<int>() { 60620 });
            NeighBorHoodToZipCode.Add("Beverly Woods", new List<int>() { 60655 });
            NeighBorHoodToZipCode.Add("Big Oaks", new List<int>() { 60656 });
            NeighBorHoodToZipCode.Add("Bohemian National Cemetery", new List<int>() { 60630 });
            NeighBorHoodToZipCode.Add("Bowmanville", new List<int>() { 60640, 60625 });
            NeighBorHoodToZipCode.Add("Brainerd", new List<int>() { 60620 });
            NeighBorHoodToZipCode.Add("Bridgeport", new List<int>() { 60608, 60616, 60609 });
            NeighBorHoodToZipCode.Add("Brighten Park", new List<int>() { 60632 });
            NeighBorHoodToZipCode.Add("Bronzeville", new List<int>() { 60653, 60615, 60609, 60616 });
            NeighBorHoodToZipCode.Add("Bucktown", new List<int>() { 60647, 60622, 60614 });
            NeighBorHoodToZipCode.Add("Budlong Woods", new List<int>() { 60625 });
            NeighBorHoodToZipCode.Add("Buena Park", new List<int>() { 60613, 60640 });
            NeighBorHoodToZipCode.Add("Burnside", new List<int>() { 60619 });
            NeighBorHoodToZipCode.Add("Cabrini Green", new List<int>() { 60610 });
            NeighBorHoodToZipCode.Add("Calumet Heights", new List<int>() { 60617, 60619 });
            NeighBorHoodToZipCode.Add("Chatham", new List<int>() { 60619 });
            NeighBorHoodToZipCode.Add("Chicago Lawn", new List<int>() { 60629 });
            NeighBorHoodToZipCode.Add("Chinatown", new List<int>() { 60616 });
            NeighBorHoodToZipCode.Add("Clearing", new List<int>() { 60638 });
            NeighBorHoodToZipCode.Add("Cottage Grove Heights", new List<int>() { 60628 });
            NeighBorHoodToZipCode.Add("Cragin", new List<int>() { 60641, 60639 });
            NeighBorHoodToZipCode.Add("DePaul", new List<int>() { 60614 });
            NeighBorHoodToZipCode.Add("Dearborn Park", new List<int>() { 60605 });
            NeighBorHoodToZipCode.Add("Douglas", new List<int>() { 60616 });
            NeighBorHoodToZipCode.Add("Douglas Park", new List<int>() { 60608 });
            NeighBorHoodToZipCode.Add("Dunning", new List<int>() { 60634 });
            NeighBorHoodToZipCode.Add("East Chicago", new List<int>() { 60617 });
            NeighBorHoodToZipCode.Add("East Garfield Park", new List<int>() { 60624, 60612 });
            NeighBorHoodToZipCode.Add("East Rogers Park", new List<int>() { 60626 });
            NeighBorHoodToZipCode.Add("East Village", new List<int>() { 60622 });
            NeighBorHoodToZipCode.Add("Edgebrook", new List<int>() { 60646 });
            NeighBorHoodToZipCode.Add("Edgewater", new List<int>() { 60640, 60660 });
            NeighBorHoodToZipCode.Add("Edison Park", new List<int>() { 60631 });
            NeighBorHoodToZipCode.Add("Englewood", new List<int>() { 60621, 60636 });
            NeighBorHoodToZipCode.Add("Fifth City", new List<int>() { 60624 });
            NeighBorHoodToZipCode.Add("Ford City", new List<int>() { 60629, 60652 });
            NeighBorHoodToZipCode.Add("Forest Glen", new List<int>() { 60630 });
            NeighBorHoodToZipCode.Add("Fuller Park", new List<int>() { 60609 });
            NeighBorHoodToZipCode.Add("Fulton River District", new List<int>() { 60610, 60622, 60661, 60606 });
            NeighBorHoodToZipCode.Add("Gage Park", new List<int>() { 60632, 60609, 60629 });
            NeighBorHoodToZipCode.Add("Galewood", new List<int>() { 60607 });
            NeighBorHoodToZipCode.Add("Garfield Ridge", new List<int>() { 60638 });
            NeighBorHoodToZipCode.Add("Gold Coast", new List<int>() { 60610, 60611 });
            NeighBorHoodToZipCode.Add("Goose Island", new List<int>() { 60610, 60622 });
            NeighBorHoodToZipCode.Add("Graceland Cemetery", new List<int>() { 60613 });
            NeighBorHoodToZipCode.Add("Graceland West", new List<int>() { 60613 });
            NeighBorHoodToZipCode.Add("Grand Crossing", new List<int>() { 60637, 60619 });
            NeighBorHoodToZipCode.Add("Gresham", new List<int>() { 60620 });
            NeighBorHoodToZipCode.Add("Hanson Park", new List<int>() { 60639 });
            NeighBorHoodToZipCode.Add("Hegewisch", new List<int>() { 60633 });
            NeighBorHoodToZipCode.Add("Hermosa", new List<int>() { 60639 });
            NeighBorHoodToZipCode.Add("Hollywood Park", new List<int>() { 60659 });
            NeighBorHoodToZipCode.Add("Homan Square", new List<int>() { 60624 });
            NeighBorHoodToZipCode.Add("Humboldt Park", new List<int>() { 60651, 60622, 60647 });
            NeighBorHoodToZipCode.Add("Hyde Park", new List<int>() { 60615, 60637 });
            NeighBorHoodToZipCode.Add("Industrial Corridor", new List<int>() { 60618, 60647, 60614, 60622, 60610 });
            NeighBorHoodToZipCode.Add("Irving Park", new List<int>() { 60618 });
            NeighBorHoodToZipCode.Add("Irving Woods", new List<int>() { 60634 });
            NeighBorHoodToZipCode.Add("Jackson Park Highlands", new List<int>() { 60649 });
            NeighBorHoodToZipCode.Add("Jefferson Park", new List<int>() { 60630 });
            NeighBorHoodToZipCode.Add("Jeffery Manor", new List<int>() { 60617 });
            NeighBorHoodToZipCode.Add("Kelvyn Park", new List<int>() { 60639, 60641 });
            NeighBorHoodToZipCode.Add("Kennedy Park", new List<int>() { 60655 });
            NeighBorHoodToZipCode.Add("Kenwood", new List<int>() { 60615 });
            NeighBorHoodToZipCode.Add("Kilbourn Park", new List<int>() { 60641 });
            NeighBorHoodToZipCode.Add("Lakeview", new List<int>() { 60657, 60613 });
            NeighBorHoodToZipCode.Add("Lakeview East", new List<int>() { 60657, 60613 });
            NeighBorHoodToZipCode.Add("Lakewood Balmoral", new List<int>() { 60640 });
            NeighBorHoodToZipCode.Add("Lathrop", new List<int>() { 60614, 60647 });
            NeighBorHoodToZipCode.Add("Lawndale", new List<int>() { 60623, 60624, 60644, 60608, 60612 });
            NeighBorHoodToZipCode.Add("Le Claire Courts", new List<int>() { 60638 });
            NeighBorHoodToZipCode.Add("Lincoln Park", new List<int>() { 60614, 60610 });
            NeighBorHoodToZipCode.Add("Lincoln Square", new List<int>() { 60625 });
            NeighBorHoodToZipCode.Add("Little Village", new List<int>() { 60623, 60608 });
            NeighBorHoodToZipCode.Add("Logan Square", new List<int>() { 60647, 60618 });
            NeighBorHoodToZipCode.Add("Longwood Manor", new List<int>() { 60628, 60643 });
            NeighBorHoodToZipCode.Add("Margate Park", new List<int>() { 60640 });
            NeighBorHoodToZipCode.Add("Marquette Park", new List<int>() { 60629 });
            NeighBorHoodToZipCode.Add("Marycrest", new List<int>() { 60652 });
            NeighBorHoodToZipCode.Add("Marynook", new List<int>() { 60619 });
            NeighBorHoodToZipCode.Add("Mayfair", new List<int>() { 60630 });
            NeighBorHoodToZipCode.Add("McKinley Park", new List<int>() { 60608, 60609 });
            NeighBorHoodToZipCode.Add("Medical District", new List<int>() { 60608, 60612 });
            NeighBorHoodToZipCode.Add("Midway", new List<int>() { 60638 });
            NeighBorHoodToZipCode.Add("Montclare", new List<int>() { 60634, 60607 });
            NeighBorHoodToZipCode.Add("Morgan Park", new List<int>() { 60643 });
            NeighBorHoodToZipCode.Add("Mount Greenwood", new List<int>() { 60655 });
            NeighBorHoodToZipCode.Add("Near North", new List<int>() { 60610 });
            NeighBorHoodToZipCode.Add("Near South Side", new List<int>() { 60616 });
            NeighBorHoodToZipCode.Add("Near Eastside", new List<int>() { 60601 });
            NeighBorHoodToZipCode.Add("Noble Square", new List<int>() { 60622 });
            NeighBorHoodToZipCode.Add("North Austin", new List<int>() { 60639, 60651 });
            NeighBorHoodToZipCode.Add("North Center", new List<int>() { 60618, 60613 });
            NeighBorHoodToZipCode.Add("North Kenwood", new List<int>() { 60653 });
            NeighBorHoodToZipCode.Add("North Mayfair", new List<int>() { 60630 });
            NeighBorHoodToZipCode.Add("North Park", new List<int>() { 60625 });
            NeighBorHoodToZipCode.Add("Norwood Park", new List<int>() { 60631 });
            NeighBorHoodToZipCode.Add("Oakland", new List<int>() { 60653 });
            NeighBorHoodToZipCode.Add("O'Hare", new List<int>() { 60656 });
            NeighBorHoodToZipCode.Add("Old Irving Park", new List<int>() { 60641 });
            NeighBorHoodToZipCode.Add("Old Norwood Park", new List<int>() { 60631 });
            NeighBorHoodToZipCode.Add("Old Town", new List<int>() { 60610 });
            NeighBorHoodToZipCode.Add("Old Town Triangle", new List<int>() { 60614 });
            NeighBorHoodToZipCode.Add("Oriole Park", new List<int>() { 60656 });
            NeighBorHoodToZipCode.Add("Park Manor", new List<int>() { 60637, 60621, 60619 });
            NeighBorHoodToZipCode.Add("Parkview", new List<int>() { 60652 });
            NeighBorHoodToZipCode.Add("Peterson Park", new List<int>() { 60659 });
            NeighBorHoodToZipCode.Add("Peterson Park Grounds", new List<int>() { 60646 });
            NeighBorHoodToZipCode.Add("Peterson Woods", new List<int>() { 60659 });
            NeighBorHoodToZipCode.Add("Pill Hill", new List<int>() { 60617 });
            NeighBorHoodToZipCode.Add("Pilsen", new List<int>() { 60608, 60616 });
            NeighBorHoodToZipCode.Add("Portage Park", new List<int>() { 60634, 60641 });
            NeighBorHoodToZipCode.Add("Prairie District", new List<int>() { 60616 });
            NeighBorHoodToZipCode.Add("Princeton Park", new List<int>() { 60620 });
            NeighBorHoodToZipCode.Add("Printer?s Row", new List<int>() { 60605 });
            NeighBorHoodToZipCode.Add("Pullman", new List<int>() { 60628 });
            NeighBorHoodToZipCode.Add("Ravenswood", new List<int>() { 60640, 60625, 60613 });
            NeighBorHoodToZipCode.Add("Ravenswood Gardens", new List<int>() { 60625 });
            NeighBorHoodToZipCode.Add("Ravenswood Manor", new List<int>() { 60625 });
            NeighBorHoodToZipCode.Add("River North", new List<int>() { 60654, 60611 });
            NeighBorHoodToZipCode.Add("River West", new List<int>() { 60622, 60610 });
            NeighBorHoodToZipCode.Add("Roscoe Village", new List<int>() { 60618, 60657 });
            NeighBorHoodToZipCode.Add("Rosehill Cemetery", new List<int>() { 60625 });
            NeighBorHoodToZipCode.Add("Roseland", new List<int>() { 60628 });
            NeighBorHoodToZipCode.Add("Rosemoor", new List<int>() { 60628 });
            NeighBorHoodToZipCode.Add("Sauganash", new List<int>() { 60646, 60630 });
            NeighBorHoodToZipCode.Add("Schorsch Forest View", new List<int>() { 60656 });
            NeighBorHoodToZipCode.Add("Schorsch Village", new List<int>() { 60634 });
            NeighBorHoodToZipCode.Add("Scottsdale", new List<int>() { 60652 });
            NeighBorHoodToZipCode.Add("Sheridan Park", new List<int>() { 60640 });
            NeighBorHoodToZipCode.Add("Sleepy Hollow", new List<int>() { 60632 });
            NeighBorHoodToZipCode.Add("South Austin", new List<int>() { 60644, 60651 });
            NeighBorHoodToZipCode.Add("South Chicago", new List<int>() { 60617 });
            NeighBorHoodToZipCode.Add("South Deering", new List<int>() { 60617 });
            NeighBorHoodToZipCode.Add("South Edgebrook", new List<int>() { 60646 });
            NeighBorHoodToZipCode.Add("South Loop", new List<int>() { 60605, 60607, 60608, 60616 });
            NeighBorHoodToZipCode.Add("South Shore", new List<int>() { 60649 });
            NeighBorHoodToZipCode.Add("St. Ben?s", new List<int>() { 60618 });
            NeighBorHoodToZipCode.Add("Stony Island Park", new List<int>() { 60617 });
            NeighBorHoodToZipCode.Add("Streeterville", new List<int>() { 60611 });
            NeighBorHoodToZipCode.Add("The Gap", new List<int>() { 60616 });
            NeighBorHoodToZipCode.Add("The Loop", new List<int>() { 60601, 60602, 60603, 60604, 60605, 60606, 60607, 60661 });
            NeighBorHoodToZipCode.Add("Tri-Taylor", new List<int>() { 60612 });
            NeighBorHoodToZipCode.Add("Ukranian Village", new List<int>() { 60622, 60612 });
            NeighBorHoodToZipCode.Add("Union Ridge", new List<int>() { 60656 });
            NeighBorHoodToZipCode.Add("United Center Park", new List<int>() { 60612, 60622 });
            NeighBorHoodToZipCode.Add("University Village/Little Italy", new List<int>() { 60607, 60608 });
            NeighBorHoodToZipCode.Add("Uptown", new List<int>() { 60640 });
            NeighBorHoodToZipCode.Add("Vittum Park", new List<int>() { 60638 });
            NeighBorHoodToZipCode.Add("Washington Heights", new List<int>() { 60643 });
            NeighBorHoodToZipCode.Add("Washington Park", new List<int>() { 60637, 60615, 60609 });
            NeighBorHoodToZipCode.Add("West Beverly", new List<int>() { 60655 });
            NeighBorHoodToZipCode.Add("West Chathum", new List<int>() { 60620 });
            NeighBorHoodToZipCode.Add("West Chesterfield", new List<int>() { 60619 });
            NeighBorHoodToZipCode.Add("West Elsdon", new List<int>() { 60632, 60629 });
            NeighBorHoodToZipCode.Add("West Englewood", new List<int>() { 60636 });
            NeighBorHoodToZipCode.Add("West Garfield Park", new List<int>() { 60624 });
            NeighBorHoodToZipCode.Add("West Humboldt Park", new List<int>() { 60651 });
            NeighBorHoodToZipCode.Add("West Lakeview", new List<int>() { 60657, 60618 });
            NeighBorHoodToZipCode.Add("West Loop", new List<int>() { 60607 });
            NeighBorHoodToZipCode.Add("West Morgan Park", new List<int>() { 60655 });
            NeighBorHoodToZipCode.Add("West Pullman", new List<int>() { 60628, 60643 });
            NeighBorHoodToZipCode.Add("West Rogers Park", new List<int>() { 60645, 60659, 60660 });
            NeighBorHoodToZipCode.Add("Westlawn", new List<int>() { 60629 });
            NeighBorHoodToZipCode.Add("Wicker Park", new List<int>() { 60622 });
            NeighBorHoodToZipCode.Add("Wildwood", new List<int>() { 60646 });
            NeighBorHoodToZipCode.Add("Woodlawn", new List<int>() { 60637, 60649 });
            NeighBorHoodToZipCode.Add("Wrightwood", new List<int>() { 60652 });
            NeighBorHoodToZipCode.Add("Wrigleyville", new List<int>() { 60613 });
        }

        // https://en.wikipedia.org/wiki/Neighborhoods_in_Chicago
        private static void SetupNeighBorHoodToCommunityArea()
        {
            NeighBorHoodToCommunityArea = new Dictionary<string, string>();
            NeighBorHoodToCommunityArea.Add("Albany Park", "Albany Park");
            NeighBorHoodToCommunityArea.Add("Altgeld Gardens", "Riverdale");
            NeighBorHoodToCommunityArea.Add("Andersonville", "Edgewater");
            NeighBorHoodToCommunityArea.Add("Archer Heights", "Archer Heights");
            NeighBorHoodToCommunityArea.Add("Armour Square", "Armour Square");
            NeighBorHoodToCommunityArea.Add("Ashburn", "Ashburn");
            NeighBorHoodToCommunityArea.Add("Ashburn Estates", "Ashburn");
            NeighBorHoodToCommunityArea.Add("Auburn Gresham", "Auburn Gresham");
            NeighBorHoodToCommunityArea.Add("Avalon Park", "Avalon Park");
            NeighBorHoodToCommunityArea.Add("Avondale", "Avondale");
            NeighBorHoodToCommunityArea.Add("Avondale Gardens", "Irving Park");
            NeighBorHoodToCommunityArea.Add("Back of the Yards", "New City");
            NeighBorHoodToCommunityArea.Add("Belmont Central", "Belmont Cragin");
            NeighBorHoodToCommunityArea.Add("Belmont Gardens", "Hermosa");
            NeighBorHoodToCommunityArea.Add("Belmont Heights", "Dunning");
            NeighBorHoodToCommunityArea.Add("Belmont Terrace", "Dunning");
            NeighBorHoodToCommunityArea.Add("Beverly", "Beverly");
            NeighBorHoodToCommunityArea.Add("Beverly View", "Ashburn");
            NeighBorHoodToCommunityArea.Add("Beverly Woods", "Morgan Park");
            NeighBorHoodToCommunityArea.Add("Big Oaks", "Norwood Park");
            NeighBorHoodToCommunityArea.Add("Boystown", "Lake View");
            NeighBorHoodToCommunityArea.Add("Bowmanville", "Lincoln Square");
            NeighBorHoodToCommunityArea.Add("Brainerd", "Washington Heights");
            NeighBorHoodToCommunityArea.Add("Brickyard", "Belmont Cragin");
            NeighBorHoodToCommunityArea.Add("Bridgeport", "Bridgeport");
            NeighBorHoodToCommunityArea.Add("Brighton Park", "Brighton Park");
            NeighBorHoodToCommunityArea.Add("Bronzeville", "Douglas");
            NeighBorHoodToCommunityArea.Add("Bucktown", "Logan Square");
            NeighBorHoodToCommunityArea.Add("Budlong Woods", "Lincoln Square");
            NeighBorHoodToCommunityArea.Add("Buena Park", "Uptown");
            NeighBorHoodToCommunityArea.Add("Burnside", "Burnside");
            NeighBorHoodToCommunityArea.Add("Cabrini–Green", "Near North Side");
            NeighBorHoodToCommunityArea.Add("Calumet Heights", "Calumet Heights");
            NeighBorHoodToCommunityArea.Add("Canaryville", "New City");
            NeighBorHoodToCommunityArea.Add("Central Station", "Near South Side");
            NeighBorHoodToCommunityArea.Add("Chatham", "Chatham");
            NeighBorHoodToCommunityArea.Add("Chicago Lawn", "Chicago Lawn");
            NeighBorHoodToCommunityArea.Add("Chinatown", "Armour Square");
            NeighBorHoodToCommunityArea.Add("Chrysler Village", "Clearing");
            NeighBorHoodToCommunityArea.Add("Clarendon Park", "Uptown");
            NeighBorHoodToCommunityArea.Add("Clearing East", "Clearing");
            NeighBorHoodToCommunityArea.Add("Clearing West", "Clearing");
            NeighBorHoodToCommunityArea.Add("Cottage Grove Heights", "Pullman");
            NeighBorHoodToCommunityArea.Add("Cragin", "Belmont Cragin");
            NeighBorHoodToCommunityArea.Add("Crestline", "Ashburn");
            NeighBorHoodToCommunityArea.Add("Dearborn Homes", "Douglas");
            NeighBorHoodToCommunityArea.Add("Dearborn Park", "Near South Side");
            NeighBorHoodToCommunityArea.Add("Douglas Park", "North Lawndale");
            NeighBorHoodToCommunityArea.Add("Dunning", "Dunning");
            NeighBorHoodToCommunityArea.Add("East Beverly", "Beverly");
            NeighBorHoodToCommunityArea.Add("East Chatham", "Chatham");
            NeighBorHoodToCommunityArea.Add("East Garfield Park", "East Garfield Park");
            NeighBorHoodToCommunityArea.Add("East Hyde Park", "Hyde Park");
            NeighBorHoodToCommunityArea.Add("East Pilsen", "Lower West Side");
            NeighBorHoodToCommunityArea.Add("East Side", "East Side");
            NeighBorHoodToCommunityArea.Add("East Village", "West Town");
            NeighBorHoodToCommunityArea.Add("Eden Green", "Riverdale");
            NeighBorHoodToCommunityArea.Add("Edgebrook", "Forest Glen");
            NeighBorHoodToCommunityArea.Add("Edgewater", "Edgewater");
            NeighBorHoodToCommunityArea.Add("Edgewater Beach", "Edgewater");
            NeighBorHoodToCommunityArea.Add("Edgewater Glen", "Edgewater");
            NeighBorHoodToCommunityArea.Add("Edison Park", "Edison Park");
            NeighBorHoodToCommunityArea.Add("Englewood", "Englewood");
            NeighBorHoodToCommunityArea.Add("Fernwood", "Roseland");
            NeighBorHoodToCommunityArea.Add("Fifth City", "East Garfield Park");
            NeighBorHoodToCommunityArea.Add("Ford City", "West Lawn");
            NeighBorHoodToCommunityArea.Add("Forest Glen", "Forest Glen");
            NeighBorHoodToCommunityArea.Add("Fuller Park", "Fuller Park");
            NeighBorHoodToCommunityArea.Add("Fulton River District", "Near West Side");
            NeighBorHoodToCommunityArea.Add("Gage Park", "Gage Park");
            NeighBorHoodToCommunityArea.Add("Galewood", "Austin");
            NeighBorHoodToCommunityArea.Add("The Gap", "Douglas");
            NeighBorHoodToCommunityArea.Add("Garfield Ridge", "Garfield Ridge");
            NeighBorHoodToCommunityArea.Add("Gladstone Park", "Jefferson Park");
            NeighBorHoodToCommunityArea.Add("Gold Coast", "Near North Side");
            NeighBorHoodToCommunityArea.Add("Golden Gate", "Riverdale");
            NeighBorHoodToCommunityArea.Add("Goose Island", "Near North Side");
            NeighBorHoodToCommunityArea.Add("Graceland West", "Lake View");
            NeighBorHoodToCommunityArea.Add("Grand Boulevard", "Grand Boulevard");
            NeighBorHoodToCommunityArea.Add("Grand Crossing", "Greater Grand Crossing");
            NeighBorHoodToCommunityArea.Add("Greater Grand Crossing", "Greater Grand Crossing");
            NeighBorHoodToCommunityArea.Add("Greektown", "Near West Side");
            NeighBorHoodToCommunityArea.Add("Gresham", "Auburn Gresham");
            NeighBorHoodToCommunityArea.Add("Groveland Park", "Douglas");
            NeighBorHoodToCommunityArea.Add("Hamilton Park", "Englewood");
            NeighBorHoodToCommunityArea.Add("Hanson Park", "Belmont Cragin");
            NeighBorHoodToCommunityArea.Add("Heart of Chicago", "Lower West Side");
            NeighBorHoodToCommunityArea.Add("Hegewisch", "Hegewisch");
            NeighBorHoodToCommunityArea.Add("Hermosa", "Hermosa");
            NeighBorHoodToCommunityArea.Add("Hollywood Park", "North Park");
            NeighBorHoodToCommunityArea.Add("Homan Square", "North Lawndale");
            NeighBorHoodToCommunityArea.Add("Humboldt Park", "Humboldt Park");
            NeighBorHoodToCommunityArea.Add("Hyde Park", "Hyde Park");
            NeighBorHoodToCommunityArea.Add("Illinois Medical District", "Near West Side");
            NeighBorHoodToCommunityArea.Add("Irving Park", "Irving Park");
            NeighBorHoodToCommunityArea.Add("Irving Woods", "Dunning");
            NeighBorHoodToCommunityArea.Add("The Island", "Austin");
            NeighBorHoodToCommunityArea.Add("Jackowo", "Avondale");
            NeighBorHoodToCommunityArea.Add("Jackson Park Highlands", "South Shore");
            NeighBorHoodToCommunityArea.Add("Jefferson Park", "Jefferson Park");
            NeighBorHoodToCommunityArea.Add("K-Town", "North Lawndale");
            NeighBorHoodToCommunityArea.Add("Kelvyn Park", "Hermosa");
            NeighBorHoodToCommunityArea.Add("Kennedy Park", "Morgan Park");
            NeighBorHoodToCommunityArea.Add("Kensington", "Roseland");
            NeighBorHoodToCommunityArea.Add("Kenwood", "Kenwood");
            NeighBorHoodToCommunityArea.Add("Kilbourn Park", "Irving Park");
            NeighBorHoodToCommunityArea.Add("The Land of Koz", "Logan Square");
            NeighBorHoodToCommunityArea.Add("Kosciuszko Park", "Logan Square");
            NeighBorHoodToCommunityArea.Add("Lake Meadows", "Douglas");
            NeighBorHoodToCommunityArea.Add("Lake View", "Lake View");
            NeighBorHoodToCommunityArea.Add("Lake View East", "Lake View");
            NeighBorHoodToCommunityArea.Add("Lakewood / Balmoral", "Edgewater");
            NeighBorHoodToCommunityArea.Add("LeClaire Courts", "Garfield Ridge");
            NeighBorHoodToCommunityArea.Add("Robert Taylor Homes", "Grand Boulevard");
            NeighBorHoodToCommunityArea.Add("Legends South", "Grand Boulevard");
            NeighBorHoodToCommunityArea.Add("Lilydale", "Roseland");
            NeighBorHoodToCommunityArea.Add("Lincoln Park", "Lincoln Park");
            NeighBorHoodToCommunityArea.Add("Lincoln Square", "Lincoln Square");
            NeighBorHoodToCommunityArea.Add("Lithuanian Plaza", "Chicago Lawn");
            NeighBorHoodToCommunityArea.Add("Little Italy", "Near West Side");
            NeighBorHoodToCommunityArea.Add("Little Village", "South Lawndale");
            NeighBorHoodToCommunityArea.Add("Logan Square", "Logan Square");
            NeighBorHoodToCommunityArea.Add("Longwood Manor", "Washington Heights");
            NeighBorHoodToCommunityArea.Add("The Loop", "The Loop");
            NeighBorHoodToCommunityArea.Add("Lower West Side", "Lower West Side");
            NeighBorHoodToCommunityArea.Add("Loyola", "Rogers Park");
            NeighBorHoodToCommunityArea.Add("Magnificent Mile", "Near North Side");
            NeighBorHoodToCommunityArea.Add("Margate Park", "Uptown");
            NeighBorHoodToCommunityArea.Add("Marquette Park", "Chicago Lawn");
            NeighBorHoodToCommunityArea.Add("Marshall Square", "South Lawndale");
            NeighBorHoodToCommunityArea.Add("Marynook", "Avalon Park");
            NeighBorHoodToCommunityArea.Add("Mayfair", "Albany Park");
            NeighBorHoodToCommunityArea.Add("McKinley Park", "McKinley Park");
            NeighBorHoodToCommunityArea.Add("Merchant Park", "Irving Park");
            NeighBorHoodToCommunityArea.Add("Montclare", "Montclare");
            NeighBorHoodToCommunityArea.Add("Morgan Park", "Morgan Park");
            NeighBorHoodToCommunityArea.Add("Mount Greenwood", "Mount Greenwood");
            NeighBorHoodToCommunityArea.Add("Museum Campus", "Near South Side");
            NeighBorHoodToCommunityArea.Add("Near East Side", "The Loop");
            NeighBorHoodToCommunityArea.Add("Near North Side", "Near North Side");
            NeighBorHoodToCommunityArea.Add("Near West Side", "Near West Side");
            NeighBorHoodToCommunityArea.Add("New Chinatown", "Uptown");
            NeighBorHoodToCommunityArea.Add("New City", "New City");
            NeighBorHoodToCommunityArea.Add("Noble Square", "West Town");
            NeighBorHoodToCommunityArea.Add("North Austin", "Austin");
            NeighBorHoodToCommunityArea.Add("North Center", "North Center");
            NeighBorHoodToCommunityArea.Add("North Halsted", "Lake View");
            NeighBorHoodToCommunityArea.Add("North Kenwood", "Kenwood");
            NeighBorHoodToCommunityArea.Add("North Lawndale", "North Lawndale");
            NeighBorHoodToCommunityArea.Add("North Mayfair", "Albany Park");
            NeighBorHoodToCommunityArea.Add("North Park", "North Park");
            NeighBorHoodToCommunityArea.Add("Nortown", "West Ridge");
            NeighBorHoodToCommunityArea.Add("Norwood Park East", "Norwood Park");
            NeighBorHoodToCommunityArea.Add("Norwood Park West", "Norwood Park");
            NeighBorHoodToCommunityArea.Add("Oakland", "Oakland");
            NeighBorHoodToCommunityArea.Add("O'Hare", "O'Hare");
            NeighBorHoodToCommunityArea.Add("Old Edgebrook", "Forest Glen");
            NeighBorHoodToCommunityArea.Add("Old Irving Park", "Irving Park");
            NeighBorHoodToCommunityArea.Add("Old Norwood", "Norwood Park");
            NeighBorHoodToCommunityArea.Add("Old Town", "Near North Side");
            NeighBorHoodToCommunityArea.Add("Old Town Triangle", "Lincoln Park");
            NeighBorHoodToCommunityArea.Add("Oriole Park", "Norwood Park");
            NeighBorHoodToCommunityArea.Add("Palmer Square", "Logan Square");
            NeighBorHoodToCommunityArea.Add("Park Manor", "Greater Grand Crossing");
            NeighBorHoodToCommunityArea.Add("Park West", "Lincoln Park");
            NeighBorHoodToCommunityArea.Add("Parkview", "Ashburn");
            NeighBorHoodToCommunityArea.Add("Peterson Park", "West Ridge");
            NeighBorHoodToCommunityArea.Add("Pill Hill", "Calumet Heights");
            NeighBorHoodToCommunityArea.Add("Pilsen", "Lower West Side");
            NeighBorHoodToCommunityArea.Add("Polish Downtown", "West Town, Logan Square");
            NeighBorHoodToCommunityArea.Add("Polish Village", "Avondale, Irving Park");
            NeighBorHoodToCommunityArea.Add("Portage Park", "Portage Park");
            NeighBorHoodToCommunityArea.Add("Prairie Avenue Historic District", "Near South Side");
            NeighBorHoodToCommunityArea.Add("Prairie Shores", "Douglas");
            NeighBorHoodToCommunityArea.Add("Princeton Park", "Roseland");
            NeighBorHoodToCommunityArea.Add("Printer's Row", "The Loop");
            NeighBorHoodToCommunityArea.Add("Pulaski Park", "West Town");
            NeighBorHoodToCommunityArea.Add("Pullman", "Pullman");
            NeighBorHoodToCommunityArea.Add("Ranch Triangle", "Lincoln Park");
            NeighBorHoodToCommunityArea.Add("Ravenswood", "Lincoln Square");
            NeighBorHoodToCommunityArea.Add("Ravenswood Gardens", "Lincoln Square");
            NeighBorHoodToCommunityArea.Add("Ravenswood Manor", "Albany Park");
            NeighBorHoodToCommunityArea.Add("River North", "Near North Side");
            NeighBorHoodToCommunityArea.Add("River West", "West Town");
            NeighBorHoodToCommunityArea.Add("River's Edge", "North Park");
            NeighBorHoodToCommunityArea.Add("Riverdale", "Riverdale");
            NeighBorHoodToCommunityArea.Add("Rogers Park", "Rogers Park");
            NeighBorHoodToCommunityArea.Add("Roscoe Village", "North Center");
            NeighBorHoodToCommunityArea.Add("Rosehill", "West Ridge");
            NeighBorHoodToCommunityArea.Add("Roseland", "Roseland");
            NeighBorHoodToCommunityArea.Add("Rosemoor", "Roseland");
            NeighBorHoodToCommunityArea.Add("Saint Ben's", "North Center");
            NeighBorHoodToCommunityArea.Add("Sauganash", "Forest Glen");
            NeighBorHoodToCommunityArea.Add("Schorsch Forest View", "O'Hare");
            NeighBorHoodToCommunityArea.Add("Schorsch Village", "Dunning");
            NeighBorHoodToCommunityArea.Add("Scottsdale", "Ashburn");
            NeighBorHoodToCommunityArea.Add("Sheffield Neighbors", "Lincoln Park");
            NeighBorHoodToCommunityArea.Add("Sheridan Park", "Uptown");
            NeighBorHoodToCommunityArea.Add("Sleepy Hollow", "Garfield Ridge");
            NeighBorHoodToCommunityArea.Add("Smith Park", "West Town");
            NeighBorHoodToCommunityArea.Add("South Austin", "Austin");
            NeighBorHoodToCommunityArea.Add("South Chicago", "South Chicago");
            NeighBorHoodToCommunityArea.Add("South Commons", "Douglas");
            NeighBorHoodToCommunityArea.Add("South Deering", "South Deering");
            NeighBorHoodToCommunityArea.Add("South East Ravenswood", "Lake View");
            NeighBorHoodToCommunityArea.Add("South Edgebrook", "Forest Glen");
            NeighBorHoodToCommunityArea.Add("South Lawndale", "South Lawndale");
            NeighBorHoodToCommunityArea.Add("South Loop", "The Loop");
            NeighBorHoodToCommunityArea.Add("South Shore", "South Shore");
            NeighBorHoodToCommunityArea.Add("Stateway Gardens", "Douglas");
            NeighBorHoodToCommunityArea.Add("Stony Island Park", "Avalon Park");
            NeighBorHoodToCommunityArea.Add("Streeterville", "Near North Side");
            NeighBorHoodToCommunityArea.Add("Talley's Corner", "Mount Greenwood");
            NeighBorHoodToCommunityArea.Add("Tri-Taylor", "Near West Side");
            NeighBorHoodToCommunityArea.Add("Ukrainian Village", "West Town");
            NeighBorHoodToCommunityArea.Add("Union Ridge", "Norwood Park");
            NeighBorHoodToCommunityArea.Add("University Village", "Near West Side");
            NeighBorHoodToCommunityArea.Add("Uptown", "Uptown");
            NeighBorHoodToCommunityArea.Add("The Villa", "Irving Park");
            NeighBorHoodToCommunityArea.Add("Vittum Park", "Garfield Ridge");
            NeighBorHoodToCommunityArea.Add("Wacławowo", "Avondale");
            NeighBorHoodToCommunityArea.Add("Washington Heights", "Washington Heights");
            NeighBorHoodToCommunityArea.Add("Washington Park", "Washington Park");
            NeighBorHoodToCommunityArea.Add("Wentworth Gardens", "Armour Square");
            NeighBorHoodToCommunityArea.Add("West Beverly", "Beverly");
            NeighBorHoodToCommunityArea.Add("West Chatham", "Chatham");
            NeighBorHoodToCommunityArea.Add("West Chesterfield", "Chatham, Roseland");
            NeighBorHoodToCommunityArea.Add("West DePaul", "Lincoln Park");
            NeighBorHoodToCommunityArea.Add("West Elsdon", "West Elsdon");
            NeighBorHoodToCommunityArea.Add("West Englewood", "West Englewood");
            NeighBorHoodToCommunityArea.Add("West Garfield Park", "West Garfield Park");
            NeighBorHoodToCommunityArea.Add("West Humboldt Park", "Austin, Humboldt Park");
            NeighBorHoodToCommunityArea.Add("West Lakeview", "Lake View");
            NeighBorHoodToCommunityArea.Add("West Lawn", "West Lawn");
            NeighBorHoodToCommunityArea.Add("West Loop", "Near West Side");
            NeighBorHoodToCommunityArea.Add("West Morgan Park", "Morgan Park");
            NeighBorHoodToCommunityArea.Add("West Pullman", "West Pullman");
            NeighBorHoodToCommunityArea.Add("West Ridge", "West Ridge");
            NeighBorHoodToCommunityArea.Add("West Rogers Park", "West Ridge");
            NeighBorHoodToCommunityArea.Add("West Town", "West Town");
            NeighBorHoodToCommunityArea.Add("West Woodlawn", "Woodlawn");
            NeighBorHoodToCommunityArea.Add("Wicker Park", "West Town");
            NeighBorHoodToCommunityArea.Add("Wildwood", "Forest Glen");
            NeighBorHoodToCommunityArea.Add("Woodlawn", "Woodlawn");
            NeighBorHoodToCommunityArea.Add("Wrightwood", "Ashburn");
            NeighBorHoodToCommunityArea.Add("Wrightwood Neighbors", "Lincoln Park");
            NeighBorHoodToCommunityArea.Add("Wrigleyville", "Lake View");

        }
    }
}
