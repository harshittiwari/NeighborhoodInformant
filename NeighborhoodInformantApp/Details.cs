using NeighborhoodInformantApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NeighborhoodInformantApp
{
    public partial class Details : Form
    {
        AffordableRentalHouse _house;
        public Details(AffordableRentalHouse house)
        {
            InitializeComponent();
            _house = house;
            tb_cn.Text = house.CommunityAreaName;
            tb_address.Text = house.Address + " " + house.ZipCode.ToString();
            
            SetupAutoComplete(tb_cn,
                DataMgr.DataMgr.Houses.Select(h => h.CommunityAreaName).Distinct().ToArray());

            Setup(house);
        }

        public void SetupAutoComplete(TextBox tb, string[] allowedStrings)
        {
            AutoCompleteStringCollection allowedCollection = new AutoCompleteStringCollection();
            allowedCollection.AddRange(allowedStrings);

            tb.AutoCompleteMode = AutoCompleteMode.Suggest;
            tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tb.AutoCompleteCustomSource = allowedCollection;
        }

        private void Setup(AffordableRentalHouse house)
        {
            SetupSchool(house);
            SetupBusStop(house);
            SetupTrainL(house);
            SetupMetra(house);
            SetupCrime(house);
            SetupPark(house);
            SetupDivvy(house);
            SetupLibrary(house);
            SetupGrocery(house);
        }

        private void SetupGrocery(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("License_id");
            dt.Columns.Add("Account_number");
            dt.Columns.Add("Address");
            dt.Columns.Add("Zip");
            dt.Columns.Add("Square_feet");
            dt.Columns.Add("Buffer_size");

            foreach (GroceryStore sc in house.GroceryStores)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = sc.Store_name;
                dr["License_id"] = sc.License_id;
                dr["Account_number"] = sc.Account_number;
                dr["Address"] = sc.Address;
                dr["Zip"] = sc.Zip_code;
                dr["Square_feet"] = sc.Square_feet;
                dr["Buffer_size"] = sc.Buffer_size;
                dt.Rows.Add(dr);
            }

            dgv_grocery.DataSource = dt;
            if (house.GroceryStoresCount > 0)
                tp_grocery.Text = "Groceries(" + house.GroceryStoresCount + ")";
        }

        private void SetupLibrary(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Cybernavigator");
            dt.Columns.Add("Teacher_in_the_library");
            dt.Columns.Add("Address");
            dt.Columns.Add("Zip");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Website");
            dt.Columns.Add("Hours_of_operation");
            
            foreach (Library sc in house.Libraries)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = sc.Name;
                dr["Cybernavigator"] = sc.Cybernavigator;
                dr["Teacher_in_the_library"] = sc.Teacher_in_the_library;
                dr["Address"] = sc.Address;
                dr["Zip"] = sc.Zip;
                dr["Phone"] = sc.Phone;
                dr["Website"] = sc.Website;
                dr["Hours_of_operation"] = sc.Hours_of_operation;
                dt.Rows.Add(dr);
            }

            dgv_library.DataSource = dt;
            if (house.LibrariesCount > 0)
                tp_library.Text = "Libraries(" + house.LibrariesCount + ")";
        }

        private void SetupDivvy(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Total_Docks");
            dt.Columns.Add("Docks_In_Service");
            dt.Columns.Add("Status");

            foreach (DivvyStation sc in house.DivvyStations)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = sc.Station_Name;
                dr["Address"] = sc.Address;
                dr["Total_Docks"] = sc.Total_Docks;
                dr["Docks_In_Service"] = sc.Docks_In_Service;
                dr["Status"] = sc.Status;
                dt.Rows.Add(dr);
            }

            dgv_divvy.DataSource = dt;
            if (house.DivvyStationsCount > 0)
                tp_divvy.Text = "DivvyStations(" + house.DivvyStationsCount + ")";
        }

        private void SetupPark(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Zip");
            dt.Columns.Add("Acres");
            dt.Columns.Add("ParkClass");

            foreach (Park sc in house.Parks)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = sc.Name;
                dr["Address"] = sc.Address;
                dr["Zip"] = sc.Zip;
                dr["Acres"] = sc.Acres;
                dr["ParkClass"] = sc.ParkClass;
                dt.Rows.Add(dr);
            }

            dgv_park.DataSource = dt;
            if (house.ParksCount > 0)
                tp_Parks.Text = "Parks(" + house.ParksCount + ")";
        }

        private void SetupCrime(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CaseNumber");
            dt.Columns.Add("Date");
            dt.Columns.Add("Block");
            dt.Columns.Add("IUCR");
            dt.Columns.Add("PrimaryType");
            dt.Columns.Add("Description");
            dt.Columns.Add("LocationDescription");
            dt.Columns.Add("Arrest");
            dt.Columns.Add("Domestic");
            dt.Columns.Add("FBICode");

            foreach (CrimeData sc in DataMgr.DataMgr.Crimes.Where(c => c.CommunityArea == house.CommunityAreaNumber))
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = sc.ID;
                dr["CaseNumber"] = sc.CaseNumber;
                dr["Date"] = sc.Date;
                dr["Block"] = sc.Block;
                dr["IUCR"] = sc.IUCR;
                dr["PrimaryType"] = sc.PrimaryType;
                dr["Description"] = sc.Description;
                dr["LocationDescription"] = sc.LocationDescription;
                dr["Arrest"] = sc.Arrest;
                dr["Domestic"] = sc.Domestic;
                dr["FBICode"] = sc.FBICode;

                dt.Rows.Add(dr);
            }

            dgv_crime.DataSource = dt;
            if (dt.Rows.Count > 0)
                tp_CrimeDetails.Text = "Crime(" + dt.Rows.Count + ")";
        }

        private void SetupMetra(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            foreach (Stop sc in house.MetraStops)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = sc.Name;
                dr["Latitude"] = sc.Latitude;
                dr["Longitude"] = sc.Longitude;
                dt.Rows.Add(dr);
            }

            dgv_metra.DataSource = dt;
            if (house.MetraStopsCount > 0)
                tp_metra.Text = "Metra Stations(" + house.MetraStopsCount + ")";
        }

        private void SetupTrainL(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            foreach (Stop sc in house.TrainLStops)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = sc.Name;
                dr["Latitude"] = sc.Latitude;
                dr["Longitude"] = sc.Longitude;
                dt.Rows.Add(dr);
            }

            dgv_trainL.DataSource = dt;
            if (house.TrainLStopsCount > 0)
                tp_lStop.Text = "Train Stops(" + house.TrainLStopsCount + ")";
        }

        private void SetupBusStop(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            foreach (Stop sc in house.BusStops)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = sc.Name;
                dr["Latitude"] = sc.Latitude;
                dr["Longitude"] = sc.Longitude;
                dt.Rows.Add(dr);
            }

            dgv_bus.DataSource = dt;
            if (house.BusStopsCount > 0)
                tp_BusStops.Text = "Bus Stops(" + house.BusStopsCount + ")";
        }

        private void SetupSchool(AffordableRentalHouse house)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("SchoolType");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Columns.Add("State");
            dt.Columns.Add("ZipCode");
            dt.Columns.Add("PhoneNumber");
            dt.Columns.Add("Link");
            dt.Columns.Add("NetworkManager");
            dt.Columns.Add("CommunityArea");
            dt.Columns.Add("Ward");
            dt.Columns.Add("PoliceDistrict");
            dt.Columns.Add("XCoordinate");
            dt.Columns.Add("YCoordinate");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            foreach (School sc in house.Schools)
            {
                DataRow dr = dt.NewRow();
                dr["Id"]=sc.School_id;
                dr["Name"] = sc.School_name;
                dr["SchoolType"] = sc.School_type;
                dr["Address"] = sc.Address;
                dr["City"] = sc.City;
                dr["State"] = sc.State;
                dr["ZipCode"] = sc.Zip_code;
                dr["PhoneNumber"] = sc.Phone_number;
                dr["Link"] = sc.Link;
                dr["NetworkManager"] = sc.Network_manager;
                dr["CommunityArea"] = sc.Community_area_name;
                dr["Ward"] = sc.Ward;
                dr["PoliceDistrict"] = sc.Police_district;
                dr["XCoordinate"] = sc.X_coordinate;
                dr["YCoordinate"] = sc.Y_coordinate;
                dr["Latitude"] = sc.Latitude;
                dr["Longitude"] = sc.Longitude;
                dt.Rows.Add(dr);
            }

            dgv_school.DataSource = dt;
            if (house.SchoolsCount > 0)
                tp_school.Text = "Schools(" + house.SchoolsCount + ")";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AffordableRentalHouse house = new AffordableRentalHouse();
                house.CommunityAreaName = tb_cn.Text;
                house.CommunityAreaNumber = DataMgr.DataMgr.Houses.Where(h => h.CommunityAreaName == house.CommunityAreaName).First().CommunityAreaNumber;
                house.Address = tb_address.Text;

                double lat=0, lng=0;

                GetLatLng(tb_address.Text, ref lat, ref lng);

                house.Longitude = lng;
                house.Latitude = lat;

                Setup(house);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Data!!!");
            }
        }

        private void GetLatLng(string address_new, ref double lat, ref double lng)
        {
            try
            {
                StringBuilder queryAddr = new StringBuilder();

                queryAddr.Append("https://maps.googleapis.com/maps/api/geocode/xml?address=");
                queryAddr.Append(address_new);
                queryAddr.Append("&key=AIzaSyDMc9VGuxnKUV_MTVBenP73RMmEs3LYUgY");

                //listBox1.Items.Add("Create Url");

                var requestUri = queryAddr.ToString();
                var request = WebRequest.Create(requestUri);

                //listBox1.Items.Add("Create Request");

                var response = request.GetResponse();

                //listBox1.Items.Add("Got Response");

                var xdoc = XDocument.Load(response.GetResponseStream());

                //listBox1.Items.Add("Loaded xdoc");

                var status = xdoc.Element("GeocodeResponse").Element("status").Value.ToString();
                if (!status.Equals("OK"))
                {
                    MessageBox.Show("Invalid address, try again.");
                    return;
                }

                var result = xdoc.Element("GeocodeResponse").Element("result");

                //listBox1.Items.Add("Get result");

                var locationElement = result.Element("geometry").Element("location");
                lat = Double.Parse(locationElement.Element("lat").Value);
                lng = Double.Parse(locationElement.Element("lng").Value);

                //listBox1.Items.Add(lat + ", " + lng);

                //MessageBox.Show(lat + ", " + lng);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void btn_maps_Click(object sender, EventArgs e)
        {
            StringBuilder queryAddr = new StringBuilder();

            // create url address
            queryAddr.Append("https://maps.google.com/maps/search/");
            queryAddr.Append(_house.Address.Replace(" ", "+") + "+" + _house.ZipCode);

            //MessageBox.Show(queryAddr.ToString());

            // Launch browser or open new tab to load url
            System.Diagnostics.Process.Start(queryAddr.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string content = "";

            AffordableRentalHouse h = _house;
            content += "\n\n**********";
            content += "\n" + h.PropertyName;
            content += "\n" + h.Address + " " + h.ZipCode;
            content += "\nProperty Type: " + h.PropertyType;
            content += "\nCommunity: " + h.CommunityAreaName;
            content += "\nPhone:" + h.PhoneNumber;
            content += "\n" + h.Units + " units managed by " + h.ManagementCompany;

            if (!string.IsNullOrEmpty(content))
            {
                content = "Greetings!!!\n\n You might be interested in following housings:" + content + "\n\n\nSent by Neighborhood Informant";
                (new SendEmail(content)).ShowDialog();
            }
        }
    }
}
