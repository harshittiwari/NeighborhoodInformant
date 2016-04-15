using NeighborhoodInformantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace NeighborhoodInformantApp.DataMgr
{
    public static class DataAccess
    {
        private static string query = "";
        private static string connectionInfo;
        private static SQLiteConnection dbConnection;
        private static SQLiteCommand sqlCommand;
        //private static SQLiteTransaction transaction;


        //
        // Set the connection to the Data Base
        //
        public static void SetConnection()
        {

            try
            {
                string filename = "NI_DB.db3";
                
                connectionInfo = String.Format(@"Data Source={0}", filename);

                //dbConnection = new SQLiteConnection(connectionInfo);
            }
            catch (SQLiteException)
            {
                Console.WriteLine("Connection to DB failed.. ");
            }
        }// end SetConnection()


        public static void InsertLibraryData(Library library, List<string> queryvalues)
        {
            try
            {
                queryvalues.Add(String.Format(@"('{0}','{1}',{2},{3},'{4}','{5}','{6}', {7},'{8}', 
                                            '{9}',{10},{11} )",
                                            library.Name.Replace("'", "").ToString(),
                                            library.Hours_of_operation.Replace("'", "").ToString(),
                                            (library.Cybernavigator) ? 1 : 0,
                                            (library.Teacher_in_the_library) ? 1 : 0,
                                            library.Address.Replace("'", "").ToString(),
                                            library.City.Replace("'", "").ToString(),
                                            library.State.Replace("'", "").ToString(),
                                            library.Zip,
                                            library.Phone.Replace("'", "").ToString(),
                                            library.Website.Replace("'", "").ToString(),
                                            library.Longitude,
                                            library.Latitude
                                            ));
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertLibraryData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertLibrary()

        public static void InsertGroceryStoreData(GroceryStore grocerystore, List<string> queryvalues)
        {
            try
            {
                queryvalues.Add(String.Format(@"('{0}',{1},{2},{3},'{4}','{5}',{6},'{7}', 
                                                                    {8}, {9}, {10}, {11}, {12},{13} )",
                                                               grocerystore.Store_name.Replace("'", "").ToString(),
                                                               grocerystore.License_id,
                                                               grocerystore.Account_number,
                                                               grocerystore.Square_feet,
                                                               grocerystore.Buffer_size,
                                                               grocerystore.Address.Replace("'", "").ToString(),
                                                               grocerystore.Zip_code,
                                                               grocerystore.Community_area_name.Replace("'", "").ToString(),
                                                               grocerystore.Community_area,
                                                               grocerystore.Ward,
                                                               grocerystore.X_coordinate,
                                                               grocerystore.Y_coordinate,
                                                               grocerystore.Longitude,
                                                               grocerystore.Latitude
                                                               ));
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertGroceryStoreData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertGroceryStore()

        public static void InsertSchoolData(School school, List<string> queryvalues)
        {
            try
            {
                queryvalues.Add(String.Format(@"({0},'{1}','{2}','{3}','{4}','{5}',{6}, '{7}', '{8}', 
                                                                '{9}', {10}, {11}, {12},'{13}', {14}, {15},{16},{17} )",
                                                                school.School_id,
                                                                school.School_name.Replace("'", "").ToString(),
                                                                school.School_type.Replace("'", "").ToString(),
                                                                school.Address.Replace("'", "").ToString(),
                                                                school.City.Replace("'", "").ToString(),
                                                                school.State.Replace("'", "").ToString(),
                                                                school.Zip_code,
                                                                school.Phone_number.Replace("'", "").ToString(),
                                                                school.Link.Replace("'", "").ToString(),
                                                                school.Network_manager.Replace("'", "").ToString(),
                                                                school.X_coordinate,
                                                                school.Y_coordinate,
                                                                school.Community_area_number,
                                                                school.Community_area_name.Replace("'", "").ToString(),
                                                                school.Ward,
                                                                school.Police_district,
                                                                school.Longitude,
                                                                school.Latitude
                                                                ));
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertSchoolData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertSchool()

        public static void InsertDivvyStationData(DivvyStation divvystation, List<string> queryvalues)
        {
            try
            {
                queryvalues.Add(String.Format(@"({0},'{1}','{2}',{3},{4},'{5}',{6},{7} )",
                                                                divvystation.Id,
                                                                divvystation.Station_Name.Replace("'", "").ToString(),
                                                                divvystation.Address.Replace("'", "").ToString(),
                                                                divvystation.Total_Docks,
                                                                divvystation.Docks_In_Service,
                                                                divvystation.Status.Replace("'", "").ToString(),
                                                                divvystation.Longitude,
                                                                divvystation.Latitude
                                                                ));
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertDivvyStationData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertDivvyStation()

        public static void InsertParkData(Park park, List<string> queryvalues)
        {
            try
            {
                queryvalues.Add(String.Format(@"({0},'{1}','{2}',{3},{4},{5},'{6}',{7}, {8} )",
                                                            park.ParkNumber,
                                                            park.Name.Replace("'", "").ToString(),
                                                            park.Address.Replace("'", "").ToString(),
                                                            park.Zip,
                                                            park.Acres,
                                                            park.Ward,
                                                            park.ParkClass.Replace("'", "").ToString(),
                                                            park.Longitude,
                                                            park.Latitude
                                                            ));
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertParkData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertPark()

        public static void InsertCrimeData(CrimeData crime, List<string> queryvalues)
        {
            try
            {
                queryvalues.Add(String.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}', {7}, {8}, 
                                                    {9}, {10}, {11}, {12},'{13}', {14}, {15}, {16}, '{17}',{18},{19} )",
                                                    crime.CaseNumber.Replace("'", "").ToString(),
                                                    crime.Date,
                                                    crime.Block.Replace("'", "").ToString(),
                                                    crime.IUCR == null ? "" : crime.IUCR.Replace("'", "").ToString(),
                                                    crime.PrimaryType.Replace("'", "").ToString(),
                                                    crime.Description == null ? "" : crime.Description.Replace("'", "").ToString(),
                                                    crime.LocationDescription == null ? "" : crime.LocationDescription.Replace("'", "").ToString(),
                                                    (crime.Arrest) ? 1 : 0,
                                                    (crime.Domestic) ? 1 : 0,
                                                    crime.Beat,
                                                    crime.District,
                                                    crime.Ward,
                                                    crime.CommunityArea,
                                                    crime.FBICode == null ? "" : crime.FBICode.Replace("'", "").ToString(),
                                                    crime.XCoordinate,
                                                    crime.YCoordinate,
                                                    crime.Year,
                                                    crime.UpdatedOn,
                                                    crime.Longitude,
                                                    crime.Latitude
                                                    ));
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertCrimeData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertCrimeData()

        public static void InsertPoliceStationData(PoliceStation policestation, List<string> queryvalues)
        {
            try
            {
                queryvalues.Add(String.Format(@"({0},'{1}','{2}','{3}', {4},'{5}','{6}', '{7}', '{8}',{9},{10} )",
                                                                    policestation.District,
                                                                    policestation.Address.Replace("'", "").ToString(),
                                                                    policestation.City.Replace("'", "").ToString(),
                                                                    policestation.State.Replace("'", "").ToString(),
                                                                    policestation.Zip,
                                                                    policestation.Website.Replace("'", "").ToString(),
                                                                    policestation.Phone.Replace("'", "").ToString(),
                                                                    policestation.Fax.Replace("'", "").ToString(),
                                                                    policestation.Tty.Replace("'", "").ToString(),
                                                                    policestation.Longitude,
                                                                    policestation.Latitude
                                                                    ));

            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertPoliceStationData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertPoliceStationData()

        public static void InsertHouseData(AffordableRentalHouse house, List<string> queryvalues)
        {

            try
            {
                queryvalues.Add(String.Format(@"('{0}',{1},'{2}','{3}','{4}',{5},'{6}','{7}', 
                                                             {8}, {9}, {10}, {11}, {12} )",
                                                             house.CommunityAreaName.ToString(),
                                                             house.CommunityAreaNumber,
                                                             house.PropertyType.ToString(),
                                                             house.PropertyName.Replace("'", "").ToString(),
                                                             house.Address.ToString(),
                                                             house.ZipCode,
                                                             house.PhoneNumber.ToString(),
                                                             house.ManagementCompany.Replace("'", "").ToString(),
                                                             house.Units,
                                                             house.XCoordinate,
                                                             house.YCoordinate,
                                                             house.Longitude,
                                                             house.Latitude
                                                             ));
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nInsertHouseData  :  '{0}'", e.ToString());
                throw;
            }
        } // end InsertHouse()




        public static void PopulateLibrariesTable(List<Library> libraries)
        {
            // Clear contents of previous Database
            WipeTable("Libraries");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into Libraries (Name, Hours_of_operation, Cybernavigator,
                                             Teacher_in_the_library, Address, City,
                                             State, Zip, Phone, Website, Longitude,
                                             Latitude) values";

            try
            {
                foreach (var library in libraries)
                {
                    InsertLibraryData(library, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nPopulateLibrariesDB  :  '{0}'", e.ToString());
                throw;
            }

        } // end PopulateLibrariesTable()

        public static void PopulateGroceryStoresTable(List<GroceryStore> grocerystores)
        {

            // Clear contents of previous Database
            WipeTable("GroceryStores");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into GroceryStores ( Store_name, License_id, Account_number,
                                                  Square_feet, Buffer_size, Address, Zip_code,
                                                  Community_area_name, Community_area, Ward,
                                                  XCoordinate, YCoordinate, Longitude,
                                                  Latitude) values";
            try
            {
                // fill query values for each house
                foreach (var grocerystore in grocerystores)
                {
                    InsertGroceryStoreData(grocerystore, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("PopulateGroceryStoresDB  :  '{0}'", e.ToString());
                throw;
            }


        } // end PopulateGroceryStoresTable()

        public static void PopulateSchoolsTable(List<School> schools)
        {
            // Clear contents of previous Database
            WipeTable("Schools");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into Schools ( School_id, School_name, School_type, Address,
                                            City, State, Zip, Phone, Link, Network_manager,
                                            XCoordinate, YCoordinate, Community_area_number,
                                            Community_area_name, Ward, Police_district,
                                            Longitude, Latitude) values";

            try
            {
                // fill query values for each house
                foreach (var school in schools)
                {
                    InsertSchoolData(school, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nPopulateSchoolsDB  :  '{0}'", e.ToString());
                throw;
            }

        } // end PopulateSchoolsTable()

        public static void PopulateDivvyStationsTable(List<DivvyStation> divvystations)
        {
            // Clear contents of previous Database
            WipeTable("DivvyStations");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into DivvyStations (  Divvy_id, StationName, Address, TotalDocks,
                                                   Docks_In_Service, Status, Longitude,
                                                   Latitude) values";

            try
            {
                // fill query values for each divvystation
                foreach (var divvystation in divvystations)
                {
                    InsertDivvyStationData(divvystation, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nPopulateDivvyStationsDB  :  '{0}'", e.ToString());
                throw;
            }

        } // end PopulateDivvyStationsTable()

        public static void PopulateParksTable(List<Park> parks)
        {
            // Clear contents of previous Database
            WipeTable("Parks");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into Parks ( ParkNumber, Name, Address, Zip, Acres,
                                          Ward, ParkClass, Longitude, Latitude) 
                                          values";

            try
            {
                // fill query values for each park
                foreach (var park in parks)
                {
                    InsertParkData(park, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nPopulateParksDB  :  '{0}'", e.ToString());
                throw;
            }

        } // end PupulateParksTable()

        public static void PopulateHousesTable(List<AffordableRentalHouse> houses)
        {

            // Clear contents of previous Database
            WipeTable("Houses");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into Houses (CommunityAreaName, CommunityAreaNumber, PropertyType,
                                            PropertyName, Address, ZipCode, PhoneNumber,
                                            ManagementCompany,Units, XCoordinate,YCoordinate,
                                            Longitude, Latitude) values";

            try
            {
                // fill query values for each house
                foreach (var house in houses)
                {
                    InsertHouseData(house, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("PopulateHousesDB  :  '{0}'", e.ToString());
                throw;
                throw;
            }
        } // end PopulateHousesTable()

        public static void PopulateCrimesTable(List<CrimeData> crimes)
        {
            // Clear contents of previous Database
            WipeTable("Crimes");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into Crimes (CaseNumber, Date, Block, IUCR, PrimaryType, Description,
                                          LocationDescription, Arrest, Domestic, Beat, District,
                                          Ward, CommunityArea, FBICode, XCoordinate, YCoordinate,
                                          Year, UpdatedOn, Longitude, Latitude) values";

            try
            {
                // fill query values for each crime
                foreach (var crime in crimes)
                {
                    InsertCrimeData(crime, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nPopulateCrimesDB  :  '{0}'", e.ToString());
                throw;
            }


        } // end PopulateCrimesTable()

        public static void PopulatePoliceStationsTable(List<PoliceStation> policestations)
        {
            // Clear contents of previous Database
            WipeTable("PoliceStations");

            List<string> queryvalues = new List<string>();

            query = @"INSERT into PoliceStations (District, Address, City, State, Zip,
                                                  Website, Phone, Fax, Tty, Longitude,
                                                  Latitude)values";

            try
            {
                foreach (var policestation in policestations)
                {
                    InsertPoliceStationData(policestation, queryvalues);
                }

                // concat query with query values
                query += String.Join(",", queryvalues);
                ExecuteNonQuery(query);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("\n\nPopulatePoliceStationsDB  :  '{0}'", e.ToString());
                throw;
            }

        } // end PopulatePoliceStationsTable()








        //
        // Wipe table clean of any data
        //
        public static void WipeTable(String table)
        {
            try
            {
                query = String.Format("DELETE FROM {0}", table);
                ExecuteNonQuery(query);

            }
            catch (SQLiteException e)
            {

                Console.WriteLine("WipeTable  :  '{0}'", e.ToString());
                throw;
            }
        } // end WipeTable()


        //
        // Execute Non Query 
        //
        public static void ExecuteNonQuery(string sql)
        {
            dbConnection = new SQLiteConnection(connectionInfo);
            dbConnection.Open();

            try
            {
                using (sqlCommand = new SQLiteCommand(sql, dbConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (SQLiteException e)
            {
                dbConnection.Close();
                Console.WriteLine("ExecuteNonQuery() :  '{0}'", e.ToString());
                throw;
            }

            dbConnection.Close();

        } // end ExecuteNonQuery()

        //
        // Execute Scalar
        //
        public static string ExecuteScalar(string sql)
        {

            dbConnection = new SQLiteConnection(connectionInfo);
            dbConnection.Open();

            try
            {
                using (sqlCommand = new SQLiteCommand(sql, dbConnection))
                {
                    object result = sqlCommand.ExecuteScalar();
                    dbConnection.Close();
                    return result != null ? result.ToString() : null;
                }
            }
            catch (SQLiteException e)
            {
                dbConnection.Close();
                Console.WriteLine("ExecuteScalar() :  '{0}'", e.ToString());
                throw;
            }


        } // end ExecuteScalar()

        // 
        // ExecuteNonScalarQuery:  executes a Select query that generates a temporary table,
        // returning this table in the form of a Dataset.
        //
        public static DataSet ExecuteNonScalar(string sql)
        {
            dbConnection = new SQLiteConnection(connectionInfo);
            dbConnection.Open();

            try
            {
                using (sqlCommand = new SQLiteCommand(sql, dbConnection))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlCommand);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dbConnection.Close();

                    if (ds == null)
                        return null;
                    else
                        return ds;
                }
                
            }
            catch (SQLiteException e)
            {
                dbConnection.Close();
                Console.WriteLine("ExecuteNonScalar() :  '{0}'", e.ToString());
                throw;
            }

        } // end ExecuteNonScalar()
    }
}
