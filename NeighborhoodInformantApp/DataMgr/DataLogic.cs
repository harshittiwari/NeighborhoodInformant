
using NeighborhoodInformantApp.Model;
using NeighborhoodInformantApp.DataMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace NeighborhoodInformantApp.Model
{
    class DataLogic
    {
        

        public static int getUserID(string name)
        {
            
            string query = String.Format(@"SELECT ID FROM Users 
                                    WHERE Name = '{0}';", name);
            var userID = DataAccess.ExecuteScalar(query);

            // Id was not found
            if (userID == null)
                return 0;
            else
                return Convert.ToInt32( userID );
        } // end getUserID()

        public static bool isUserExistent(string name)
        {

            int userID = getUserID(name);

            if (userID == 0)
                return false;
            else
                return true;

        }// end isUserExits()

        public static bool createNewUser(string name, string sQuestion, string sAnswer, string pwd)
        {
            // if user name already exists
            if (isUserExistent(name))
                return false;

            string query;
            query = String.Format(@"INSERT INTO Users( Name, SecurityQuestion, SecurityAnswer, Password)
                                    VALUES ('{0}','{1}','{2}','{3}') ;", name, sQuestion, sAnswer, pwd);


            DataAccess.ExecuteNonQuery(query);
            return true;

        } // end createNewUser()

        public static bool checkLogin(string name, string password)
        {
            string query = "";
            query = String.Format(@"SELECT ID FROM Users 
                                    WHERE Name='{0}' 
                                    AND Password = '{1}';", 
                                    name, password);
            
            var result = DataAccess.ExecuteScalar(query);

            if (result == null)
                return false;
            else
                return true;

        } // end checkLogin()
        
        public static bool checkSecurity(string name, string sQuestion, string sAnswer)
        {
            string query = "";
            query = String.Format(@"SELECT ID FROM Users 
                                    WHERE Name='{0}' 
                                    AND SecurityQuestion='{1}' 
                                    AND SecurityAnswer='{2}';",
                                     name, sQuestion, sAnswer);

            var result = DataAccess.ExecuteScalar(query);

            if (result == null)
                return false;
            else
                return true;
        } // end checkSecurity()

        public static bool updatePassword(string name, string password)
        {
            if (isUserExistent(name))
            {
                string query = "";
                query = String.Format(@"UPDATE Users
                                        SET Password = '{0}'
                                        WHERE Name = name;", 
                                        password);
                DataAccess.ExecuteNonQuery(query);
                return true;
            }
            return false;
        } // end updatePassword()

        public static bool isFavoriteExistent(string name, int favorite)
        {

            string query = "";
            int userID;
            string result;

            userID = getUserID(name);

            if (userID == 0)
                return false;

            query = String.Format(@"SELECT ID FROM UserFavoriteHouse
                                    WHERE FavoriteHouse = {0}", favorite);

            result = DataAccess.ExecuteScalar(query);

            if (result == null)
                return false;
            else
                return true;

        } // end isFavoriteExistent()

        public static bool inputFavorites(string name, List<int> favoriteHouses)
        {
            deleteFavorites(name);

            string query = "";
            int userID;

            userID = getUserID(name);

            // Id was not found
            if (userID == 0)
                return false;

            foreach (int house in favoriteHouses)
            {
                // check if favorite already in db
                if (isFavoriteExistent(name, house))
                    continue;
                
                query = String.Format(@"INSERT INTO UserFavoriteHouse(UserID, FavoriteHouse) 
                                        VALUES ({0}, {1}) ;", userID, house);
                
                DataAccess.ExecuteNonQuery(query);    
            }

            return true;

        } // end inputFavorites()

        public static List<int> getFavorites(string name)
        {

            string query = "";
            int n;
            List<int> favorites = new List<int>();

            // check if user exits and get id if they do
            if ((n = getUserID(name)) == 0)
                return null;

            query = String.Format(@"SELECT FavoriteHouse FROM UserFavoriteHouse
                                    WHERE UserID = {0};", n);

            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return favorites;

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    favorites.Add(Convert.ToInt32(row[0]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("getFavorites : '{0}'", e.ToString());
                throw;
            }

            return favorites;
        } // end getFavorites()

        public static bool deleteFavorites(string name)
        {
            
            string query = "";

            int userID = getUserID(name);

            // Id was not found
            if (userID == 0)
                return false;
 
            query = String.Format(@"DELETE FROM UserFavoriteHouse
                                    WHERE UserID = '{0}';", userID);

            DataAccess.ExecuteNonQuery(query);
  
            return true;
        } // end deleteFavorites()

        public static bool setUpdateLog(string setName, string updateOn)
        {

            string query = "";

            query = String.Format(@"INSERT INTO UpdateLog(DataSetName, UpdateOn)
                                    VALUES ('{0}','{1}');", setName, updateOn);
            DataAccess.ExecuteNonQuery(query);

            return true;

        } // end setUpdateLog()
 
        public static Dictionary<string, string> getFilters(string name)
        {

            string query = "";
            int userID; 

            userID = getUserID(name);

            query= String.Format(@"SELECT * FROM UserFilter 
                                  WHERE UserID = {0}", userID);

            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables[0];
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (dt.Rows.Count == 0)
                return dict;

            DataRow dr = dt.Rows[0];
            

            dict.Add("CommunityName", dr["CommunityName"].ToString()); // write key and then value (filled with data table from line) 
            dict.Add("Zip", dr["Zip"].ToString()); 
            dict.Add("PropertyType", dr["PropertyType"].ToString());
            dict.Add("Address", dr["Address"].ToString());
            dict.Add("Distance", dr["Distance"].ToString());
            dict.Add("CrimeIndex", dr["CrimeIndex"].ToString());

            return dict;

        }

        public static bool deleteFilters(string name)
        {
            string query = "";
            int userID;

            userID = getUserID(name);

            if (userID == 0)
                return false;

            query = String.Format(@"DELETE FROM UserFilter
                                    WHERE UserID = {0};", userID);

            DataAccess.ExecuteNonQuery(query);

            return true;

        } // end deleteFilters()

        public static bool setFilters(string name, Dictionary<string, string> filters)
        {

            //delete filters
            if (!deleteFilters(name))
                return false;

            int userID = getUserID(name);

            if (userID == 0)
                return false;
            
            string query = @"INSERT INTO UserFilter (UserID,CommunityName,Zip,PropertyType,
                                Address,Distance,CrimeIndex) 
                                VALUES ("
                                                    + userID +  ",'"
                                                    + filters["CommunityName"] + "','"
                                                    + filters["Zip"] + "','"
                                                    + filters["PropertyType"] + "','"
                                                    + filters["Address"] + "','"
                                                    + filters["Distance"] + "','"
                                                    + filters["CrimeIndex"] + "')";
            DataAccess.ExecuteNonQuery(query);

            return true;

        }

        public static bool isLogExistent(string dataSetName)
        {

            var query = String.Format(@"SELECT ID FROM UpdateLog
                                        WHERE DataSetName = '{0}';",
                                        dataSetName);

            string result = DataAccess.ExecuteScalar(query);

            if (result == null)
                return false;
            else
                return true;

        }// end isUserExits()

        public static bool updateLog(string dataSetName, string updateOn)
        {

            if (!isLogExistent(dataSetName))
                return false;
            
            string query = String.Format(@"UPDATE UpdateLog 
                                            SET UpdateOn = '{0}'
                                            WHERE DataSetName = '{1}';",
                                            updateOn, dataSetName);
            
            DataAccess.ExecuteNonQuery(query);
            return true;

        }
        
        
        ///////////////////////////////////
        // Fetching table data from DB   //
        ///////////////////////////////////
        
        public static Dictionary<string, DateTime> FetchUpdateLog()
        {
            Dictionary<string, DateTime> result = new Dictionary<string, DateTime>();
            string query = @"Select * from UpdateLog;";

            //DataTable dt = new DataTable();
            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return result;

            foreach (DataRow row in dt.Rows)
            {
                result.Add(row[1].ToString(), DateTime.Parse(row[2].ToString()));    
            }

            return result;
         
        } // end FetchUpdateLog
        
        public static List<AffordableRentalHouse> FetchHouses ()
        {
            List<AffordableRentalHouse> result = new List<AffordableRentalHouse>();
            string query = @"Select * from Houses;";

            //DataTable dt = new DataTable();
            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {
                
                AffordableRentalHouse h = new AffordableRentalHouse();
                h.id = Convert.ToInt32( row["ID"]);
                h.CommunityAreaName = row["CommunityAreaName"].ToString();
                h.CommunityAreaNumber = Convert.ToInt32( row["CommunityAreaNumber"]);
                h.PropertyType =  row["PropertyType"].ToString();
                h.PropertyName =  row["PropertyName"].ToString();
                h.Address =  row["Address"].ToString();
                h.ZipCode = Convert.ToInt32( row["ZipCode"]);
                h.PhoneNumber = row["PhoneNumber"].ToString();
                h.ManagementCompany = row["ManagementCompany"].ToString();
                h.Units = Convert.ToInt32( row["Units"]);
                h.XCoordinate = Convert.ToDouble( row["XCoordinate"]);
                h.YCoordinate = Convert.ToDouble( row["YCoordinate"]);
                h.Longitude = Convert.ToDouble( row["Longitude"]);
                h.Latitude = Convert.ToDouble( row["Latitude"]);

                result.Add(h);
            }

            return result;
        } // end FetchHouses()

        public static List<CrimeData> FetchCrimeData()
        {
            List<CrimeData> result = new List<CrimeData>();
            string query = @"Select * from Crimes;";

            //DataTable dt = new DataTable();
            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                CrimeData c = new CrimeData();
   
                c.ID = Convert.ToInt32(row["ID"]);
                c.CaseNumber = row["CaseNumber"].ToString();
                c.Date = Convert.ToDateTime( row["Date"]);
                c.Block = row["Block"].ToString();
                c.IUCR = row["IUCR"].ToString();
                c.PrimaryType = row["PrimaryType"].ToString();
                c.Description = row["Description"].ToString();
                c.LocationDescription = row["LocationDescription"].ToString();
                c.Arrest = Convert.ToBoolean(row["Arrest"]);
                c.Domestic = Convert.ToBoolean(row["Domestic"]);
                c.Beat = Convert.ToInt32(row["Beat"]);
                c.District = Convert.ToInt32(row["District"]);
                c.Ward = Convert.ToInt32(row["Ward"]);
                c.CommunityArea = Convert.ToInt32(row["CommunityArea"]);
                c.FBICode = row["FBICode"].ToString();
                c.XCoordinate = Convert.ToDouble(row["XCoordinate"]);
                c.YCoordinate = Convert.ToDouble(row["YCoordinate"]);
                c.Year = Convert.ToInt32(row["Year"]);
                c.UpdatedOn = Convert.ToDateTime(row["UpdatedOn"]);
                c.Longitude = Convert.ToDouble(row["Longitude"]);
                c.Latitude = Convert.ToDouble(row["Latitude"]);


                result.Add(c);
            }

            return result;
        } // end FetchCrimeData()

        public static List<DivvyStation> FetchDivvyStations()
        {
            List<DivvyStation> result = new List<DivvyStation>();
            string query = @"Select * from DivvyStations;";

            //DataTable dt = new DataTable();
            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                DivvyStation d = new DivvyStation();
                
                d.Id = Convert.ToInt32(row["ID"]);
                d.Station_Name = row["StationName"].ToString();
                d.Address = row["Address"].ToString();
                d.Total_Docks = Convert.ToInt32(row["TotalDocks"]);
                d.Docks_In_Service = Convert.ToDouble(row["Docks_In_Service"]);
                d.Status = row["Status"].ToString();
                d.Longitude = Convert.ToDouble(row["Longitude"]);
                d.Latitude = Convert.ToDouble(row["Latitude"]);

                result.Add(d);
            }

            return result;
        } // end FetchDivvyStations()
              
        public static List<GroceryStore> FetchGroceryStores()
        {
            List<GroceryStore> result = new List<GroceryStore>();
            string query = @"Select * from GroceryStores;";

            //DataTable dt = new DataTable();
            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                GroceryStore g = new GroceryStore();

                g.Store_name = row["Store_name"].ToString();
                g.License_id = Convert.ToInt32(row["License_id"]);
                g.Account_number = Convert.ToInt32(row["Account_number"]);
                g.Square_feet = Convert.ToInt32(row["Square_feet"]);
                g.Buffer_size = Convert.ToChar(row["Buffer_size"]);
                g.Address = row["Address"].ToString();
                g.Zip_code = Convert.ToInt32(row["Zip_code"]);
                g.Community_area_name = row["Community_area_name"].ToString();
                g.Community_area = Convert.ToInt32(row["Community_area"]);
                g.Ward = Convert.ToInt32(row["Ward"]);
                g.X_coordinate = Convert.ToDouble(row["XCoordinate"]);
                g.Y_coordinate = Convert.ToDouble(row["YCoordinate"]);
                g.Longitude = Convert.ToDouble(row["Longitude"]);
                g.Latitude = Convert.ToDouble(row["Latitude"]);

                result.Add(g);
            }

            return result;
        } // end FetchGroceryStores()

        public static List<Library> FetchLibraries()
        {
            List<Library> result = new List<Library>();
            string query = @"Select * from Libraries;";

            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                Library l = new Library();

                l.Name = row["Name"].ToString() ;
                l.Hours_of_operation = row["Hours_of_operation"].ToString() ;
                l.Cybernavigator = Convert.ToBoolean(row["Cybernavigator"]) ;
                l.Teacher_in_the_library = Convert.ToBoolean( row["Teacher_in_the_library"]) ;
                l.Address = row["Address"].ToString() ;
                l.City = row["City"].ToString() ;
                l.State = row["State"].ToString() ;
                l.Zip = Convert.ToInt32( row["Zip"]) ;
                l.Phone =  row["Phone"].ToString() ;
                l.Website = row["Website"].ToString() ;
                l.Longitude = Convert.ToDouble( row["Longitude"] );
                l.Latitude = Convert.ToDouble( row["Latitude"] );

                result.Add(l);
            }

            return result;
        } // end FetchLibraries()

        public static List<Park> FetchParks()
        {
            List<Park> result = new List<Park>();
            string query = @"Select * from Parks;";

            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                Park p = new Park();

                p.ParkNumber = Convert.ToInt32( row["ParkNumber"] );
                p.Name = row["Name"].ToString() ;
                p.Address = row["Address"].ToString() ;
                p.Zip = Convert.ToInt32( row["Zip"] );
                p.Acres = Convert.ToDouble( row["Acres"] );
                p.Ward = Convert.ToInt32( row["Ward"] );
                p.ParkClass = row["ParkClass"].ToString() ;
                p.Longitude = Convert.ToDouble( row["Longitude"] );
                p.Latitude = Convert.ToDouble( row["Latitude"] );

                result.Add(p);
            }

            return result;
        } // end FetchParks()

        public static List<PoliceStation> FetchPoliceStations()
        {
            List<PoliceStation> result = new List<PoliceStation>();
            string query = @"Select * from PoliceStations;";

            //DataTable dt = new DataTable();
            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                PoliceStation p = new PoliceStation();

                p.District = Convert.ToInt32( row["District"] );
                p.Address = row["Address"].ToString() ;
                p.City = row["City"].ToString() ;
                p.State = row["State"].ToString() ;
                p.Zip = Convert.ToInt32( row["Zip"] );
                p.Website = row["Website"].ToString() ;
                p.Phone = row["Phone"].ToString() ;
                p.Fax = row["Fax"].ToString() ;
                p.Tty = row["Tty"].ToString() ;
                p.Longitude = Convert.ToDouble( row["Longitude"] );
                p.Latitude = Convert.ToDouble( row["Latitude"] );

                result.Add(p);
            }

            return result;
        } // end FetchPoliceStations()

        public static List<School> FetchSchools()
        {
            List<School> result = new List<School>();
            string query = @"Select * from Schools;";

            //DataTable dt = new DataTable();
            DataTable dt = DataAccess.ExecuteNonScalar(query).Tables["TABLE"];

            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                School s = new School();

                s.School_id = Convert.ToInt32( row["School_id"] );
                s.School_name = row["School_name"].ToString() ;
                s.School_type = row["School_type"].ToString() ;
                s.Address = row["Address"].ToString() ;
                s.City = row["City"].ToString() ;
                s.State = row["State"].ToString() ;
                s.Zip_code = Convert.ToInt32( row["Zip"] );
                s.Phone_number = row["Phone"].ToString() ;
                s.Link = row["Link"].ToString() ;
                s.Network_manager = row["Network_manager"].ToString() ;
                s.X_coordinate = Convert.ToDouble( row["XCoordinate"] );
                s.Y_coordinate = Convert.ToDouble( row["YCoordinate"] );
                s.Community_area_number = Convert.ToInt32( row["Community_area_number"] );
                s.Community_area_name = row["Community_area_name"].ToString() ;
                s.Ward = Convert.ToInt32( row["Ward"] );
                s.Police_district = Convert.ToInt32( row["Police_district"] );
                s.Longitude = Convert.ToDouble( row["Longitude"] );
                s.Latitude = Convert.ToDouble( row["Latitude"] );

                result.Add(s);
            }

            return result;
        } //end FetchSchools()



        public int getCount(string tableName)
        {
            int count = 0;

            string query = String.Format("Select Count(*) From '{0}';", tableName);

            count = Convert.ToInt32(DataAccess.ExecuteScalar(query));

            if (count < 0)
                return -1;
            else
                return count;
        } // end getCount()



    }
}
