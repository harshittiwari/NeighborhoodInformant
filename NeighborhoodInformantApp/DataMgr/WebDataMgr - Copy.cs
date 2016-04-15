using NeighborhoodInformantApp.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NeighborhoodInformantApp.DataMgr
{
    public static class WebDataMgr
    {
        public static List<AffordableRentalHouse> FetchHouses()
        {
            string sURL = "https://data.cityofchicago.org/api/views/s6ha-ppgi/rows.json?accessType=DOWNLOAD";

            JObject odc = GetJasonObject(sURL, "House");
            if (odc == null)
                return null;
            // get JSON result objects into a list
            var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<AffordableRentalHouse> Houses = new List<AffordableRentalHouse>();
            foreach (var v1 in odc["data"])
            {
                //Console.WriteLine(v1.ToString());
                var vv = v1.First();
                for (int i1 = 0; i1 < 8; i1++)
                    vv = vv.Next;
                AffordableRentalHouse r = new AffordableRentalHouse();

                if (vv == null)
                    continue;

                r.CommunityAreaName = vv.ToString();
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.CommunityAreaNumber = int.Parse(vv.ToString());
                vv = vv.Next;
                r.PropertyType = vv.ToString();
                vv = vv.Next;
                r.PropertyName = vv.ToString();
                vv = vv.Next;
                r.Address = vv.ToString();
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.ZipCode = int.Parse(vv.ToString());
                vv = vv.Next;
                r.PhoneNumber = vv.ToString();
                vv = vv.Next;
                r.ManagementCompany = vv.ToString();
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.Units = int.Parse(vv.ToString());
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.XCoordinate = double.Parse(vv.ToString());
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.YCoordinate = double.Parse(vv.ToString());
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.Latitude = double.Parse(vv.ToString());
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.Longitude = double.Parse(vv.ToString());
                Houses.Add(r);
            }
            return Houses;
        }

        internal static List<School> FetchSchools()
        {
            string sURL = "https://data.cityofchicago.org/api/views/9xs2-f89t/rows.json?accessType=DOWNLOAD";

            JObject odc = GetJasonObject(sURL, "School");
            if (odc == null)
                return null;
            // get JSON result objects into a list
            var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<School> Schools = new List<School>();
            foreach (var v1 in odc["data"])
            {
                var vv = v1.First();
                for (int i1 = 0; i1 < 8; i1++)
                    vv = vv.Next;
                School r = new School();

                if (vv == null)
                    continue;

                int.TryParse(vv.ToString(), out r.School_id);
                vv = vv.Next;
                r.School_name = vv.ToString();
                vv = vv.Next;
                r.School_type = vv.ToString();
                vv = vv.Next;
                r.Address = vv.ToString();
                vv = vv.Next;
                r.City = vv.ToString();
                vv = vv.Next;
                r.State = vv.ToString();
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Zip_code);
                vv = vv.Next;
                r.Phone_number = vv.ToString();
                vv = vv.Next;
                r.Link = vv[0].ToString();
                vv = vv.Next;
                r.Network_manager = vv.ToString();

                for (int i = 0; i < 61; i++)
                    vv = vv.Next;

                double.TryParse(vv.ToString(), out r.X_coordinate);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Y_coordinate);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Latitude);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Longitude);
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Community_area_number);
                vv = vv.Next;
                r.Community_area_name = vv.ToString();
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Ward);
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Police_district);
                
                Schools.Add(r);
            }

            return Schools;
        }

        internal static List<GroceryStore> FetchGroceryStores()
        {
            string sURL = "https://data.cityofchicago.org/api/views/53t8-wyrc/rows.json?accessType=DOWNLOAD";

            JObject odc = GetJasonObject(sURL, "GroceryStore");
            if (odc == null)
                return null;
            // get JSON result objects into a list
            var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<GroceryStore> Stores = new List<GroceryStore>();
            foreach (var v1 in odc["data"])
            {
                var vv = v1.First();
                for (int i1 = 0; i1 < 8; i1++)
                    vv = vv.Next;
                GroceryStore r = new GroceryStore();

                if (vv == null)
                    continue;

                r.Store_name = vv.ToString();
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.License_id);
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Account_number);
                vv = vv.Next;
                int.TryParse(vv.ToString().Replace(",", ""), out r.Square_feet);
                vv = vv.Next;
                r.Buffer_size = vv.ToString()[0];
                vv = vv.Next;
                r.Address = vv.ToString();
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Zip_code);
                vv = vv.Next;
                r.Community_area_name = vv.ToString();
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Community_area);
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Ward);
                vv = vv.Next;
                vv = vv.Next;
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.X_coordinate);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Y_coordinate);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Latitude);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Longitude);

                Stores.Add(r);
            }

            return Stores;
        }

        internal static List<DivvyStation> FetchDivvyStations()
        {
            string sURL = "https://data.cityofchicago.org/api/views/bbyy-e7gq/rows.json?accessType=DOWNLOAD";

            JObject odc = GetJasonObject(sURL, "DivvyStation");
            if (odc == null)
                return null;
            // get JSON result objects into a list
            var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<DivvyStation> Stations = new List<DivvyStation>();
            foreach (var v1 in odc["data"])
            {
                var vv = v1.First();
                for (int i1 = 0; i1 < 8; i1++)
                    vv = vv.Next;
                DivvyStation r = new DivvyStation();

                if (vv == null)
                    continue;

                int.TryParse(vv.ToString(), out r.Id);
                vv = vv.Next;

                r.Station_Name = vv.ToString();
                vv = vv.Next;
                r.Address = vv.ToString();
                vv = vv.Next;
                int.TryParse(vv.ToString(), out r.Total_Docks);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Docks_In_Service);
                vv = vv.Next;
                r.Status = vv.ToString();
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Latitude);
                vv = vv.Next;
                double.TryParse(vv.ToString(), out r.Longitude);
                
                Stations.Add(r);
            }

            return Stations;
        }

        internal static List<Library> FetchLibraries()
        {
            string sURL = "https://data.cityofchicago.org/api/views/x8fc-8rcq/rows.json?accessType=DOWNLOAD";

            JObject odc = GetJasonObject(sURL, "Library");
            if (odc == null)
                return null;
            // get JSON result objects into a list
            var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<Library> Libs = new List<Library>();
            foreach (var v1 in odc["data"])
            {
                var vv = v1.First();
                for (int i1 = 0; i1 < 8; i1++)
                    vv = vv.Next;
                Library r = new Library();

                if (vv == null)
                    continue;

                r.Name = vv.ToString();
                vv = vv.Next;
                r.Hours_of_operation = vv.ToString();
                vv = vv.Next;
                r.Cybernavigator = vv.ToString().Contains("Y") ? true : false;
                vv = vv.Next;
                r.Teacher_in_the_library = vv.ToString().Contains("Y") ? true : false;
                vv = vv.Next;
                r.Address = vv.ToString();
                vv = vv.Next;
                r.City = vv.ToString();
                vv = vv.Next;
                r.State = vv.ToString();
                vv = vv.Next;

                if (!string.IsNullOrEmpty(vv.ToString()))
                    int.TryParse(vv.ToString(), out r.Zip);
                vv = vv.Next;
                r.Phone = vv.ToString();
                vv = vv.Next;
                r.Website = vv[0].ToString();
                vv = vv.Next;
                
                if (!string.IsNullOrEmpty(vv[1].ToString()))
                    double.TryParse(vv[1].ToString(), out r.Latitude);
                if (!string.IsNullOrEmpty(vv[2].ToString()))
                    double.TryParse(vv[2].ToString(), out r.Longitude);

                Libs.Add(r);
            }

            return Libs;
        }

        internal static List<Park> FetchParks()
        {
            string sURL = "https://data.cityofchicago.org/api/views/wwy2-k7b3/rows.json?accessType=DOWNLOAD";

            JObject odc = GetJasonObject(sURL, "Park");
            if (odc == null)
                return null;
            // get JSON result objects into a list
            var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<Park> Parks = new List<Park>();
            foreach (var v1 in odc["data"])
            {
                //Console.WriteLine(v1.ToString());
                var vv = v1.First();
                for (int i1 = 0; i1 < 8; i1++)
                    vv = vv.Next;
                Park r = new Park();

                if (vv == null)
                    continue;

                int.TryParse(vv.ToString(), out r.ParkNumber);
                vv = vv.Next;
                r.Name = vv.ToString();
                vv = vv.Next;
                r.Address = vv.ToString();
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    int.TryParse(vv.ToString(), out r.Zip);
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    double.TryParse(vv.ToString(), out r.Acres);
                vv = vv.Next;
                if (!string.IsNullOrEmpty(vv.ToString()))
                    int.TryParse(vv.ToString(), out r.Ward);
                vv = vv.Next;
                r.ParkClass = vv.ToString();

                for (int i = 0; i < 68; i++)
                    vv = vv.Next;

                if (!string.IsNullOrEmpty(vv[1].ToString()))
                    double.TryParse(vv[1].ToString(), out r.Latitude);
                if (!string.IsNullOrEmpty(vv[2].ToString()))
                    double.TryParse(vv[2].ToString(), out r.Longitude);

                Parks.Add(r);
            }
            return Parks;
        }

        internal static void FetchLatLong()//string text, ref double lat, ref double lon)
        {
            string sURL = "https://maps.googleapis.com/maps/api/geocode/json?address=903+S+Ashland+Ave+Chicago+IL&key=AIzaSyC-nd-lccEHVJ8GLZR5zp3_fyH_zsuQDsc";

            JObject odc = GetJasonObject(sURL, "Police");

            string lat1 = odc["results"][0]["geometry"]["location"]["lat"].ToString();
            string lon1 = odc["results"][0]["geometry"]["location"]["lng"].ToString();
            Console.WriteLine("lat1");
            Console.WriteLine("lon1");
        }

        public static List<CrimeData> FetchCrimeData(bool isOneYearData = false)
        {
            string sURL;

            if (!IsDataSetUpdate("https://data.cityofchicago.org/api/views/x2n5-8w5q/rows.json?accessType=DOWNLOAD", "Crime"))
                return null;

            WebRequest wrGETURL = WebRequest.Create("https://data.cityofchicago.org/resource/ijzp-q8t2.json");

            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;
            Stream objStream;

            try
            {
                wrGETURL.Proxy = WebRequest.DefaultWebProxy;     //WebProxy.GetDefaultProxy(); // This has been deprecated
                objStream = wrGETURL.GetResponse().GetResponseStream();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in network connection!");
                return null;
            }            

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            StringBuilder sb = new StringBuilder();
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    sb.Append(sLine);
                    //Console.WriteLine("{0}:{1}", i, sLine);
                }
            }
            JToken odc1 = JRaw.Parse(sb.ToString());

            //if (isOneYearData)
            //    //sURL = "https://data.cityofchicago.org/resource/s6ha-ppgi.json";
            //    sURL = "https://data.cityofchicago.org/api/views/x2n5-8w5q/rows.json?accessType=DOWNLOAD";
            //else
            //    sURL = "https://data.cityofchicago.org/api/views/ijzp-q8t2/rows.json?accessType=DOWNLOAD";

            //JObject odc = GetJasonObject(sURL);
            //JObject odc = GetObj();

            //var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<CrimeData> crimeData = new List<CrimeData>();
            foreach (var v1 in odc1)
            {
                if (crimeData.Count > 1000)
                    break;
                CrimeData r = new CrimeData();

                try
                {
                    r.CaseNumber = v1["case_number"].ToString();
                    int.TryParse(v1["beat"].ToString(), out r.Beat);                    
                    r.Block = v1["block"].ToString();
                    r.PrimaryType = v1["primary_type"].ToString();
                    r.LocationDescription = v1["location_description"].ToString();
                    DateTime.TryParse(v1["date"].ToString(), out r.Date);
                    r.IUCR = v1["iucr"].ToString();
                    bool.TryParse(v1["domestic"].ToString(), out r.Domestic);
                    int.TryParse(v1["id"].ToString(), out r.ID);
                    int.TryParse(v1["ward"].ToString(), out r.Ward);
                    bool.TryParse(v1["arrest"].ToString(), out r.Arrest);
                    r.Description = v1["description"].ToString();                    
                    DateTime.TryParse(v1["updated_on"].ToString(), out r.UpdatedOn);
                    r.FBICode = v1["fbi_code"].ToString();                    
                    int.TryParse(v1["year"].ToString(), out r.Year);
                    int.TryParse(v1["community_area"].ToString(), out r.CommunityArea);                    
                    int.TryParse(v1["district"].ToString(), out r.District);

                    if (v1["x_coordinate"] != null)
                        double.TryParse(v1["x_coordinate"].ToString(), out r.XCoordinate);
                    if (v1["y_coordinate"] != null)
                        double.TryParse(v1["y_coordinate"].ToString(), out r.YCoordinate);
                    if (v1["longitude"] != null)
                        double.TryParse(v1["longitude"].ToString(), out r.Longitude);
                    if (v1["latitude"] != null)
                        double.TryParse(v1["latitude"].ToString(), out r.Latitude);

                    //Console.WriteLine(v1.ToString());
                    //var vv = v1.First();
                    //for (int i1 = 0; i1 < 8; i1++)
                    //    vv = vv.Next;


                    //if (vv == null)
                    //    continue;
                    //if (!isOneYearData)
                    //{
                    //    if (!string.IsNullOrEmpty(vv.ToString()))
                    //        int.TryParse(vv.ToString(), out r.ID);
                    //    vv = vv.Next;
                    //}
                    //r.CaseNumber = vv.ToString();
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    DateTime.TryParse(vv.ToString(), out r.Date);
                    //vv = vv.Next;
                    //r.Block = vv.ToString();
                    //vv = vv.Next;
                    //r.IUCR = vv.ToString();
                    //vv = vv.Next;
                    //r.PrimaryType = vv.ToString();
                    //vv = vv.Next;
                    //r.Description = vv.ToString();
                    //vv = vv.Next;
                    //r.LocationDescription = vv.ToString();
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()) && vv.ToString() == "Y")
                    //    r.Arrest = true;
                    //else
                    //    r.Arrest = false;
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()) && vv.ToString() == "Y")
                    //    r.Domestic = true;
                    //else
                    //    r.Domestic = false;
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    int.TryParse(vv.ToString(), out r.Beat);
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    int.TryParse(vv.ToString(), out r.District);
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    int.TryParse(vv.ToString(), out r.Ward);
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    int.TryParse(vv.ToString(), out r.CommunityArea);
                    //vv = vv.Next;
                    //r.FBICode = vv.ToString();
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    r.XCoordinate = double.Parse(vv.ToString());
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    r.YCoordinate = double.Parse(vv.ToString());
                    //vv = vv.Next;
                    //if (!isOneYearData)
                    //{
                    //    if (!string.IsNullOrEmpty(vv.ToString()))
                    //        int.TryParse(vv.ToString(), out r.Year);
                    //    vv = vv.Next;
                    //    if (!string.IsNullOrEmpty(vv.ToString()))
                    //        DateTime.TryParse(vv.ToString(), out r.UpdatedOn);
                    //    vv = vv.Next;
                    //}

                    ////will have to fix getting latitude and longiude later
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    r.Latitude = double.Parse(vv.ToString());
                    //vv = vv.Next;
                    //if (!string.IsNullOrEmpty(vv.ToString()))
                    //    r.Longitude = double.Parse(vv.ToString());
                }
                catch (Exception)
                {

                }
                crimeData.Add(r);
            }

            return crimeData;
        }

        public static JObject GetObj()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("d:\\rows.json");
            string str = file.ReadToEnd();
            file.Close();
            JObject obj = JObject.Parse(str);
            return obj;
        }

        public static List<PoliceStation> FetchPoliceStations()
        {
            string sURL = "https://data.cityofchicago.org/api/views/z8bn-74gv/rows.json?accessType=DOWNLOAD";

            JObject odc = GetJasonObject(sURL, "Police");

            // get JSON result objects into a list
            var v = odc["meta"]["view"]["rowsUpdatedAt"];//.Children().ToList();
            //Console.WriteLine(v);

            List<PoliceStation> PoliceStations = new List<PoliceStation>();
            foreach (var v1 in odc["data"])
            {
                //Console.WriteLine(v1.ToString());
                var vv = v1.First();
                for (int i1 = 0; i1 < 8; i1++)
                    vv = vv.Next;
                PoliceStation r = new PoliceStation();

                if (vv == null)
                    continue;

                int.TryParse(vv.ToString(), out r.District);
                vv = vv.Next;

                r.Address = vv.ToString();
                vv = vv.Next;
                r.City = vv.ToString();
                vv = vv.Next;
                r.State = vv.ToString();
                vv = vv.Next;

                if (!string.IsNullOrEmpty(vv.ToString()))
                    r.Zip = int.Parse(vv.ToString());

                vv = vv.Next;
                r.Website = vv[0].ToString();
                vv = vv.Next;
                r.Phone = vv[0].ToString();
                vv = vv.Next;
                r.Fax = vv.ToString();
                vv = vv.Next;
                r.Tty = vv.ToString();
                vv = vv.Next;

                string location = vv.ToString();
                char[] delimiterChars = { ',', '(', ')' };
                string[] arr = Regex.Match(location, "\\(.*\\)").Value.Split(delimiterChars);
                if (arr.Count() == 4)
                {
                    if (!string.IsNullOrEmpty(arr[1]))
                        r.Latitude = double.Parse(arr[1].TrimStart(delimiterChars));
                    if (!string.IsNullOrEmpty(arr[2]))
                        r.Longitude = double.Parse(arr[2].TrimStart(delimiterChars));
                }
                if (r.Latitude == 0 || r.Longitude == 0)
                {
                    if (!string.IsNullOrEmpty(vv[1].ToString()))
                        r.Latitude = double.Parse(vv[1].ToString());
                    if (!string.IsNullOrEmpty(vv[2].ToString()))
                        r.Longitude = double.Parse(vv[2].ToString());

                }
                PoliceStations.Add(r);
            }
            return PoliceStations;
        }

        private static JObject GetJasonObject(string sURL)
        {
            WebRequest wrGETURL = WebRequest.Create(sURL);

            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;

            wrGETURL.Proxy = WebRequest.DefaultWebProxy; //WebProxy.GetDefaultProxy(); //

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            StringBuilder sb = new StringBuilder();
            int i = 0;

            bool isCheck = true;
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    sb.Append(sLine);
                    //Console.WriteLine("{0}:{1}", i, sLine);
                }
            }
            objReader.Close();
            //Console.ReadLine();
            //JToken odc1 = JRaw.Parse(sb.ToString());
            JObject odc = JObject.Parse(sb.ToString());
            return odc;
        }

        private static JObject GetJasonObject(string sURL, string dataSetName)
        {
            WebRequest wrGETURL = WebRequest.Create(sURL);

            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;

            wrGETURL.Proxy = WebRequest.DefaultWebProxy; //WebProxy.GetDefaultProxy(); //

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            StringBuilder sb = new StringBuilder();
            int i = 0;

            bool isCheck = true;
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    if (isCheck && sLine.Contains("rowsUpdatedAt"))
                    {
                        DateTime UpdatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        UpdatedAt = UpdatedAt.AddSeconds(int.Parse(Regex.Match(sLine, "\\d+").Value)).ToLocalTime();
                        if (DataMgr.UpdatedOn.ContainsKey(dataSetName))
                        {
                            if (DataMgr.UpdatedOn[dataSetName] == UpdatedAt)
                            {
                                objReader.Close();
                                return null;
                            }
                            else
                            {
                                DataMgr.UpdatedOn[dataSetName] = UpdatedAt;
                            }
                        }
                        else
                            DataMgr.UpdatedOn.Add(dataSetName, UpdatedAt);
                        isCheck = false;
                    }
                    sb.Append(sLine);
                    //Console.WriteLine("{0}:{1}", i, sLine);
                }
            }
            objReader.Close();
            //Console.ReadLine();
            //JToken odc1 = JRaw.Parse(sb.ToString());
            JObject odc = JObject.Parse(sb.ToString());
            return odc;
        }

        private static bool IsDataSetUpdate(string sURL, string dataSetName)
        {
            WebRequest wrGETURL = WebRequest.Create(sURL);

            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;

            wrGETURL.Proxy = WebRequest.DefaultWebProxy; //WebProxy.GetDefaultProxy();

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            StringBuilder sb = new StringBuilder();
            int i = 0;

            bool isCheck = true;
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    if (isCheck && sLine.Contains("rowsUpdatedAt"))
                    {
                        DateTime UpdatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        UpdatedAt = UpdatedAt.AddSeconds(int.Parse(Regex.Match(sLine, "\\d+").Value)).ToLocalTime();
                        if (DataMgr.UpdatedOn.ContainsKey(dataSetName))
                        {
                            if (DataMgr.UpdatedOn[dataSetName] == UpdatedAt)
                            {
                                objReader.Close();
                                return false;
                            }
                            else
                            {
                                DataMgr.UpdatedOn[dataSetName] = UpdatedAt;
                            }
                        }
                        else
                            DataMgr.UpdatedOn.Add(dataSetName, UpdatedAt);
                        isCheck = false;
                        objReader.Close();
                        return true;
                    }
                }
            }
            return true;
        }
    }
}

#region commented faster limited stuff
//public static class WebDataMgr
//{
//    public static List<AffordableRentalHouse> FetchHouses()
//    {
//        string sURL = "https://data.cityofchicago.org/resource/s6ha-ppgi.json";

//        JArray odc = GetJasonObject(sURL);

//        List<AffordableRentalHouse> Houses = new List<AffordableRentalHouse>();
//        foreach (var item in odc.Children())
//        {
//            AffordableRentalHouse r = new AffordableRentalHouse();

//            var itemProperties = item.Children<JProperty>();

//            r.CommunityAreaName = GetValue(itemProperties, "community_area");
//            int.TryParse(GetValue(itemProperties, "community_area_number"), out r.CommunityAreaNumber);
//            r.PropertyType = GetValue(itemProperties, "property_type");
//            r.PropertyName = GetValue(itemProperties, "property_name");
//            r.Address = GetValue(itemProperties, "address");
//            int.TryParse(GetValue(itemProperties, "zip_code"), out r.ZipCode);
//            r.PhoneNumber = GetValue(itemProperties, "phone_number");
//            r.ManagementCompany = GetValue(itemProperties, "management_company");
//            int.TryParse(GetValue(itemProperties, "units"), out r.Units);
//            double.TryParse(GetValue(itemProperties, "x_coordinate"), out r.XCoordinate);
//            double.TryParse(GetValue(itemProperties, "y_coordinate"), out r.YCoordinate);
//            double.TryParse(GetValue(itemProperties, "latitude"), out r.Latitude);
//            double.TryParse(GetValue(itemProperties, "longitude"), out r.Longitude);

//            Houses.Add(r);
//        }
//        return Houses;
//    }

//    // one year prior to present
//    public static List<CrimeData> FetchCrimeData(bool isOneYearData = true)
//    {
//        string sURL = "https://data.cityofchicago.org/resource/x2n5-8w5q.json";
//        if (!isOneYearData)
//            sURL = "https://data.cityofchicago.org/resource/ijzp-q8t2.json";
//        JArray odc = GetJasonObject(sURL);

//        List<CrimeData> crimeData = new List<CrimeData>();
//        foreach (var item in odc.Children())
//        {
//            CrimeData r = new CrimeData();

//            var itemProperties = item.Children<JProperty>();

//            int.TryParse(GetValue(itemProperties, "id"), out r.ID);
//            r.CaseNumber = GetValue(itemProperties, "case_number");
//            DateTime.TryParse(GetValue(itemProperties, "date"), out r.Date);
//            r.IUCR = GetValue(itemProperties, "iucr");
//            r.PrimaryType = GetValue(itemProperties, "primary_type");
//            r.Description = GetValue(itemProperties, "description");
//            r.LocationDescription = GetValue(itemProperties, "location_description");
//            int.TryParse(GetValue(itemProperties, "beat"), out r.Beat);
//            int.TryParse(GetValue(itemProperties, "district"), out r.District);
//            int.TryParse(GetValue(itemProperties, "ward"), out r.Ward);
//            int.TryParse(GetValue(itemProperties, "community_area"), out r.CommunityArea);
//            r.FBICode = GetValue(itemProperties, "fbi_code");
//            double.TryParse(GetValue(itemProperties, "x_coordinate"), out r.XCoordinate);
//            double.TryParse(GetValue(itemProperties, "y_coordinate"), out r.YCoordinate);
//            int.TryParse(GetValue(itemProperties, "year"), out r.Year);
//            DateTime.TryParse(GetValue(itemProperties, "updated_on"), out r.UpdatedOn);
//            double.TryParse(GetValue(itemProperties, "latitude"), out r.Latitude);
//            double.TryParse(GetValue(itemProperties, "longitude"), out r.Longitude);

//            string val = GetValue(itemProperties, "arrest");
//            if (!string.IsNullOrEmpty(val) && val.ToString() == "Y")
//                r.Arrest = true;
//            else r.Arrest = false;
//            val = GetValue(itemProperties, "domestic");
//            if (!string.IsNullOrEmpty(val) && val.ToString() == "Y")
//                r.Domestic = true;
//            else r.Domestic = false;

//            crimeData.Add(r);
//        }

//        return crimeData;
//    }

//    private static string GetValue(JEnumerable<JProperty> itemProperties, string str)
//    {
//        var myElement = itemProperties.FirstOrDefault(x => x.Name == str);
//        if (myElement == null)
//            return "";
//        else return myElement.Value.ToString(); ////This is a JValue type
//    }

//    private static JArray GetJasonObject(string sURL)
//    {
//        WebRequest wrGETURL = WebRequest.Create(sURL);

//        WebProxy myProxy = new WebProxy("myproxy", 80);
//        myProxy.BypassProxyOnLocal = true;

//        wrGETURL.Proxy = WebProxy.GetDefaultProxy();

//        Stream objStream;
//        objStream = wrGETURL.GetResponse().GetResponseStream();

//        StreamReader objReader = new StreamReader(objStream);

//        string sLine = "";
//        StringBuilder sb = new StringBuilder();
//        int i = 0;

//        while (sLine != null)
//        {
//            i++;
//            sLine = objReader.ReadLine();
//            if (sLine != null)
//            {
//                sb.Append(sLine);
//                //Console.WriteLine("{0}:{1}", i, sLine);
//            }
//        }
//        //Console.ReadLine();

//        JArray array = JArray.Parse(sb.ToString());
//        //JObject odc = JObject.Parse(sb.ToString());
//        return array;
//    }
//}
#endregion
