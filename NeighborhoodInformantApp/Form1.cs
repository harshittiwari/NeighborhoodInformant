using NeighborhoodInformantApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeighborhoodInformantApp
{
    public partial class Form1 : Form
    {
        List<string> lb_items;
        public Form1()
        {
            InitializeComponent();

            SetupListBox();

            this.Text += ":" + DataMgr.DataMgr.user.UserName;

            SetupAutoComplete(tb_propertyType,
                DataMgr.DataMgr.Houses.Select(h => h.PropertyType).Distinct().ToArray());

            SetupAutoComplete(tb_communityName,
                DataMgr.DataMgr.Houses.Select(h => h.CommunityAreaName).Distinct().ToArray());

            SetupAutoComplete(tb_ZipCode,
                DataMgr.DataMgr.Houses.Select(h => h.ZipCode.ToString()).Distinct().ToArray());

            string[] arr = new string[5];
            for (int i = 0; i < 5; i++)
                arr[i] = (i + 1).ToString();

            SetupAutoComplete(tb_crimeIndex, arr);

            //SetupAutoComplete(tb_neighborhood,
            //    DataMgr.DataMgr.NeighBorHoodToCommunityArea.Keys.ToArray().Union(DataMgr.DataMgr.NeighBorHoodToZipCode.Keys).Distinct().ToArray());

            tb_address.Enter += new EventHandler(tb_address_Enter);
            tb_address.Leave += new EventHandler(tb_address_Leave);
            tb_address_SetText();

            // setup default user settings here
            tb_communityName.Text = DataMgr.DataMgr.user.Filter["CommunityName"];
            tb_ZipCode.Text = DataMgr.DataMgr.user.Filter["Zip"];
            tb_propertyType.Text = DataMgr.DataMgr.user.Filter["PropertyType"];
            tb_address.Text = DataMgr.DataMgr.user.Filter["Address"];
            tb_distance.Text = DataMgr.DataMgr.user.Filter["Distance"];
            tb_crimeIndex.Text = DataMgr.DataMgr.user.Filter["CrimeIndex"];
        }

        private void SetupListBox()
        {
            lb_items = new List<string>() { "Rating",
                                        "CommunityAreaName" ,
                                        "CommunityAreaNumber" ,
                                        "PropertyType" ,
                                        "PropertyName" ,
                                        "Address" ,
                                        "ZipCode" ,
                                        "CrimeRate" ,
                                        "BusStops" ,
                                        "Parks" ,
                                        "TrainLStops",
                                        "MetraStops",
                                        "Library",
                                        "GroceryStores",
                                        "Schools",
                                        "DivvyStands",
                                        "PhoneNumber" ,
                                        "ManagementCompany" ,
                                        "Units" ,
                                        "XCoordinate" ,
                                        "YCoordinate" ,
                                        "Latitude" ,
                                        "Longitude" ,
                                        };
            lb_Fields.DataSource = lb_items;
            lb_Fields.SelectedIndices.Clear();
            lb_Fields.SelectedIndices.Add(0);
            lb_Fields.SelectedIndices.Add(1);
            for (int i = 3; i <= 16; i++)
                lb_Fields.SelectedIndices.Add(i);
        }

        protected void tb_address_SetText()
        {
            tb_address.Text = "enter keyword here...";
            tb_address.ForeColor = Color.Gray;
        }

        private void tb_address_Enter(object sender, EventArgs e)
        {
            if (tb_address.ForeColor == Color.Black)
                return;
            tb_address.Text = "";
            tb_address.ForeColor = Color.Black;
        }

        private void tb_address_Leave(object sender, EventArgs e)
        {
            if (tb_address.Text.Trim() == "")
                tb_address_SetText();
        }
        List<AffordableRentalHouse> housesToDisplay;
        private void btn_go_Click(object sender, EventArgs e)
        {
                  
            //IEnumerable<AffordableRentalHouse> housesToDisplay = DataMgr.DataMgr.Houses;

            if (String.IsNullOrEmpty(tb_distance.Text))
                tb_distance.Text = "500";
            int x;
            if (int.TryParse(tb_distance.Text, out x))
                DataMgr.DataMgr.Distance = x;

            Thread thread3 = new Thread(new ThreadStart(SetupRating));
            thread3.Start();
            thread3.Join();

            housesToDisplay = DataMgr.DataMgr.Houses.Where(h => h.CommunityAreaNumber > 0).OrderByDescending(h => h.Rating).ToList();

            Thread thread1 = new Thread(new ThreadStart(SetupStops));
            thread1.Start();
            Thread thread2 = new Thread(new ThreadStart(SetupParks));
            thread2.Start();
            Thread thread4 = new Thread(new ThreadStart(SetupSchools));
            thread4.Start();
            Thread thread5 = new Thread(new ThreadStart(SetupTrainL));
            thread5.Start();
            Thread thread6 = new Thread(new ThreadStart(SetupMetra));
            thread6.Start();
            Thread thread7 = new Thread(new ThreadStart(SetupDivvy));
            thread7.Start();
            Thread thread8 = new Thread(new ThreadStart(SetupLibrary));
            thread8.Start();
            Thread thread9 = new Thread(new ThreadStart(SetupGrocery));
            thread9.Start();

            thread1.Join();
            thread2.Join();
            thread4.Join();
            thread5.Join();
            thread6.Join();
            thread7.Join();
            thread8.Join();
            thread9.Join();

            FilterUnits(ref housesToDisplay);

            DataMgr.DataMgr.user.Filter["CommunityName"] = tb_communityName.Text;
            DataMgr.DataMgr.user.Filter["Zip"] = tb_ZipCode.Text;
            DataMgr.DataMgr.user.Filter["PropertyType"] = tb_propertyType.Text;
            DataMgr.DataMgr.user.Filter["Address"] = tb_address.Text;
            DataMgr.DataMgr.user.Filter["Distance"] = tb_distance.Text;
            DataMgr.DataMgr.user.Filter["CrimeIndex"] = tb_crimeIndex.Text;

            UpdateUI(housesToDisplay);

            //Testing SQLite Query
            //DataLogic ml = new DataLogic();

            //List<School> hse = new List<School>();
            //hse = DataLogic.FetchSchools();

            //if (hse == null) MessageBox.Show("ERROR OCCURED getting object from datalogic");
            //else MessageBox.Show(hse[0].Network_manager);

            //DataLogic.createNewUser("filip", "Question", "Answer", "password");
            //MessageBox.Show("filip = " + DataLogic.isUserExistent("filip").ToString());
            //MessageBox.Show("jake = " + DataLogic.isUserExistent("jake").ToString());

            //MessageBox.Show("CheckLogin filip+ :  " + DataLogic.checkLogin("filip", "password").ToString());
            //MessageBox.Show("CheckLogin jake+ :  " + DataLogic.checkLogin("jake", "password").ToString());
            //MessageBox.Show("CheckLogin filip- :  " + DataLogic.checkLogin("filip", "assword").ToString());
            //MessageBox.Show("CheckLogin jake- :  " + DataLogic.checkLogin("jake", "assword").ToString());


            //MessageBox.Show("CheckSecurity 1 = " + DataLogic.checkSecurity("filip", "Question", "Answer").ToString());
            //MessageBox.Show("CheckSecurity 2 = " + DataLogic.checkSecurity("filip", "Question", "a").ToString());
            //MessageBox.Show("CheckSecurity 3 = " + DataLogic.checkSecurity("filip", "q", "Answer").ToString());
            //MessageBox.Show("CheckSecurity 4 = " + DataLogic.checkSecurity("filip", "q", "a").ToString());
            //MessageBox.Show("CheckSecurity 5 = " + DataLogic.checkSecurity("f", "Question", "Answer").ToString());
            //MessageBox.Show("CheckSecurity 6 = " + DataLogic.checkSecurity("f", "Question", "a").ToString());
            //MessageBox.Show("CheckSecurity 7 = " + DataLogic.checkSecurity("f", "q", "Answer").ToString());
            //MessageBox.Show("CheckSecurity 8 = " + DataLogic.checkSecurity("f", "q", "a").ToString());

            //MessageBox.Show("updatePassword- = " + DataLogic.updatePassword("jake", "ass").ToString());
            //MessageBox.Show("updatePassword+ = " + DataLogic.updatePassword("filip", "password").ToString());

            //List<int> f = new List<int>();
            //f.Add(12);
            //f.Add(13);
            //f.Add(14);
            //f.Add(14);
            //MessageBox.Show("inputFavorites = " + DataLogic.inputFavorites("filip", f).ToString());

            ////MessageBox.Show("deleteFavorites = " + DataLogic.deleteFavorites("filip").ToString());

            //MessageBox.Show("getFavorites = " + DataLogic.getFavorites("filip").ToString());

            //MessageBox.Show("isFavoriteExistent 12 = " + DataLogic.isFavoriteExistent("filip", 12).ToString());
            //MessageBox.Show("isFavoriteExistent 22 = " + DataLogic.isFavoriteExistent("filip", 22).ToString());

            
        
        
        
        }

        private void SetupGrocery()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.GroceryStoresCount;
        }
        private void SetupLibrary()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.LibrariesCount;
        }
        private void SetupDivvy()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.DivvyStationsCount;
        }
        private void SetupMetra()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.MetraStopsCount;
        }
        private void SetupTrainL()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.TrainLStopsCount;
        }
        private void SetupSchools()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.SchoolsCount;
        }
        private void SetupRating()
        {
            double x;
            foreach (AffordableRentalHouse house in DataMgr.DataMgr.Houses)
                x = house.Rating;
        }
        private void SetupStops()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.BusStopsCount;
        }
        private void SetupParks()
        {
            int x;
            foreach (AffordableRentalHouse house in housesToDisplay)
                x = house.ParksCount;
        }

        private void FilterUnits(ref List<AffordableRentalHouse> housesToDisplay)
        {
            // filter neighborhood
            //if (!string.IsNullOrEmpty(tb_neighborhood.Text))
            //{
            //    //if (DataMgr.DataMgr.NeighBorHoodToZipCode.ContainsKey(tb_neighborhood.Text))
            //    //{
            //    //    List<int> zips = DataMgr.DataMgr.NeighBorHoodToZipCode[tb_neighborhood.Text];
            //    //    housesToDisplay = housesToDisplay.Where(h => zips.Contains(h.ZipCode));
            //    //}
            //    //if (DataMgr.DataMgr.NeighBorHoodToCommunityArea.ContainsKey(tb_neighborhood.Text))
            //    //{
            //    //    Regex reg = new Regex(DataMgr.DataMgr.NeighBorHoodToCommunityArea[tb_neighborhood.Text], RegexOptions.IgnoreCase);
            //    //    housesToDisplay = housesToDisplay.Where(h => reg.IsMatch(h.CommunityAreaName));
            //    //}                
            //}

            // filter favorite houses
            if(cb_onlyFavourites.Checked)
            {
                housesToDisplay = housesToDisplay.Where(h => DataMgr.DataMgr.user.favouriteHouses.Contains(h.id)).ToList();
            }

            int zipCode;
            // filter zipcode
            if (!string.IsNullOrEmpty(tb_ZipCode.Text))
            {
                if (int.TryParse(tb_ZipCode.Text, out zipCode))
                    housesToDisplay = housesToDisplay.Where(h => h.ZipCode == zipCode).ToList();
                else
                    MessageBox.Show("Invalid ZipCode Entered");
            }

            // filter community area
            if (!string.IsNullOrEmpty(tb_communityName.Text))
                housesToDisplay = housesToDisplay.Where(h => h.CommunityAreaName == tb_communityName.Text).ToList();

            // filter property type
            if (!string.IsNullOrEmpty(tb_propertyType.Text))
                housesToDisplay = housesToDisplay.Where(h => h.PropertyType == tb_propertyType.Text).ToList();

            // filter address
            if (!string.IsNullOrEmpty(tb_address.Text) && tb_address.ForeColor == Color.Black)
            {
                Regex reg = new Regex(tb_address.Text, RegexOptions.IgnoreCase);
                housesToDisplay = housesToDisplay.Where(h => reg.IsMatch(h.Address)).ToList();
            }

            // filter crimeIndex
            if (!string.IsNullOrEmpty(tb_crimeIndex.Text))
                housesToDisplay = housesToDisplay.Where(h => h.CrimeRate.ToString() == tb_crimeIndex.Text).ToList();
        }

        private void dgv_Results_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Email Details", new EventHandler(SendEmail_Click)));
                m.MenuItems.Add("Show Details", new EventHandler(ShowDetails_Click));
                m.MenuItems.Add("Show in maps", new EventHandler(ShowMaps_Click));

                //List<int> selectedRowIndex = new List<int>();
                //foreach (DataGridViewCell dgvr in dgv_Results.SelectedCells)
                //    selectedRowIndex.Add(dgvr.RowIndex);

                //m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", String.Join(";", selectedRowIndex.Distinct().ToArray()))));

                m.Show(dgv_Results, new Point(e.X, e.Y));
            }
        }

        private void ShowMaps_Click(object sender, EventArgs e)
        {
            ShowMaps();
        }

        private void ShowMaps()
        {
            List<int> rowIndex = new List<int>();
            foreach (DataGridViewCell dgvr in dgv_Results.SelectedCells)
                rowIndex.Add(dgvr.RowIndex);

            foreach (int index in rowIndex.Distinct())
            {
                // housesToDisplay.ElementAt(dgvr.RowIndex)
                AffordableRentalHouse h = housesToDisplay.ElementAt(index);

                try
                {
                    StringBuilder queryAddr = new StringBuilder();

                    // create url address
                    queryAddr.Append("https://maps.google.com/maps/search/");
                    queryAddr.Append(h.Address.Replace(" ", "+") + "+" + h.ZipCode);

                    //MessageBox.Show(queryAddr.ToString());

                    // Launch browser or open new tab to load url
                    System.Diagnostics.Process.Start(queryAddr.ToString());

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error");
                }
            }
        }


        private void SendEmail_Click(object sender, EventArgs e)
        {
            SendEmail();
        }

        private void SendEmail()
        {
            string content = "";
            List<int> rowIndex = new List<int>();
            foreach (DataGridViewCell dgvr in dgv_Results.SelectedCells)
                rowIndex.Add(dgvr.RowIndex);

            foreach (int index in rowIndex.Distinct())
            {
                // housesToDisplay.ElementAt(dgvr.RowIndex)
                AffordableRentalHouse h = housesToDisplay.ElementAt(index);
                content += "\n\n**********";
                content += "\n" + h.PropertyName;
                content += "\n" + h.Address + " " + h.ZipCode;
                content += "\nProperty Type: " + h.PropertyType;
                content += "\nCommunity: " + h.CommunityAreaName;
                content += "\nPhone:" + h.PhoneNumber;
                content += "\n" + h.Units + " units managed by " + h.ManagementCompany;
            }
            if (!string.IsNullOrEmpty(content))
            {
                content = "Greetings!!!\n\n You might be interested in following housings:" + content + "\n\n\nSent by Neighborhood Informant";
                (new SendEmail(content)).ShowDialog();
            }
        }

        private void ShowDetails_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void dgv_Results_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowDetails();
        }

        private void UpdateUI(IEnumerable<AffordableRentalHouse> housesToDisplay)
        {
            DataTable dt = DataMgr.DataMgr.GetHousesDT(housesToDisplay);

            if (lb_Fields.SelectedItems.Count > 0)
            {
                List<string> selectedCols = new List<string>();
                foreach (string item in lb_Fields.SelectedItems)
                    selectedCols.Add(item.ToString());

                foreach (string col in lb_items.Where(t => !selectedCols.Contains(t)))
                {
                    dt.Columns.Remove(col);
                }
            }

            dgv_Results.AutoGenerateColumns = true;
            dgv_Results.DataSource = dt;

            groupBox2.Text = housesToDisplay.Count() + " units found";

            if (cb_ColorCode.Checked)
                foreach (DataGridViewRow dgvr in dgv_Results.Rows)
                {
                    double outVar;
                    if (double.TryParse(dgvr.Cells["Rating"].Value.ToString(), out outVar))
                    {
                        if (outVar >= 80)
                            dgvr.DefaultCellStyle.BackColor = Color.FromArgb(0, 255, 92);
                        else if (outVar > 65)
                            dgvr.DefaultCellStyle.BackColor = Color.FromArgb(250, 255, 92);
                        else// (outVar > 80)
                            dgvr.DefaultCellStyle.BackColor = Color.FromArgb(255, 92, 92);
                    }
                }
        }

        public void SetupAutoComplete(TextBox tb, string[] allowedStrings)
        {
            AutoCompleteStringCollection allowedCollection = new AutoCompleteStringCollection();
            allowedCollection.AddRange(allowedStrings);

            tb.AutoCompleteMode = AutoCompleteMode.Suggest;
            tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tb.AutoCompleteCustomSource = allowedCollection;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_FormClosing(null, null);
            Environment.Exit(0);
        }

        private void tb_ZipCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btn_go.PerformClick();
            }
        }

        private void tb_propertyType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btn_go.PerformClick();
            }
        }

        private void tb_address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btn_go.PerformClick();
            }
        }

        private void tb_neighborhood_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btn_go.PerformClick();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G)
            {
                btn_go.PerformClick();
            }
            //else if (e.Control && e.KeyCode == Keys.S && e.Shift)
            //{
            //    Save(getSelectedRows());
            //}
            //else if (e.Control && e.KeyCode == Keys.S)
            //{
            //    Save((DataTable)dgv_Results.DataSource);
            //}
            //else if (e.KeyCode == Keys.F11)
            //{
            //    helpToolStripMenuItem1_Click(null, null);
            //}
        }

        private DataTable getSelectedRows()
        {

            DataTable dt = new DataTable();
            foreach(DataGridViewColumn col in  this.dgv_Results.Columns)                
                dt.Columns.Add(col.Name);
            
            foreach (DataGridViewRow dgvr in this.dgv_Results.SelectedRows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewColumn col in this.dgv_Results.Columns)
                    dr[col.Name] = dgvr.Cells[col.Name].Value;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private void Save(DataTable dt)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "CSV files (*.csv)|*.csv|XML-Excel (*.xml)|*.xml|HTML (*.html)|*.html|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FilterIndex == 1)
                {
                    SaveCSV(dt, saveFileDialog1.FileName.ToString());
                }
                if (saveFileDialog1.FilterIndex == 2)
                {
                    SaveExcel(saveFileDialog1.FileName.ToString(), dt);
                }
                if (saveFileDialog1.FilterIndex == 3)
                {
                    SaveHTML(dt, saveFileDialog1.FileName.ToString());
                }
            }
        }

        private void SaveHTML(DataTable dt, string docName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<Table border=\"1\">");

            sb.Append("<tr>");

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append("<th valign=middle>" + dt.Columns[i].ColumnName + "</th>");
            }

            sb.Append("</tr>");
            sb.Append("<tr>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                    sb.Append("<td align=center>" + dt.Rows[i][j].ToString() + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</Table></body></html>");

            System.IO.StreamWriter file = new System.IO.StreamWriter(docName);
            file.WriteLine(sb.ToString());
            file.Close();
        }

        internal static void SaveExcel(string docName, DataTable dt)
        {
            //export a DataTable to Excel
            DialogResult retry = DialogResult.Retry;

            while (retry == DialogResult.Retry)
            {
                try
                {
                    using (ExcelExport writer = new ExcelExport(docName))
                    {
                        writer.WriteStartDocument();

                        // Write the worksheet contents
                        writer.WriteStartWorksheet("Sheet1");

                        //Write header row
                        writer.WriteStartRow();
                        foreach (DataColumn col in dt.Columns)
                            writer.WriteExcelUnstyledCell(col.Caption);
                        writer.WriteEndRow();

                        //write data
                        foreach (DataRow row in dt.Rows)
                        {
                            writer.WriteStartRow();
                            foreach (object o in row.ItemArray)
                            {
                                writer.WriteExcelAutoStyledCell(o);
                            }
                            writer.WriteEndRow();
                        }

                        // Close up the document
                        writer.WriteEndWorksheet();
                        writer.WriteEndDocument();
                        writer.Close();
                        //if (true)
                        //    OpenFile(fileName);
                        retry = DialogResult.Cancel;
                    }
                }
                catch (Exception myException)
                {
                    retry = MessageBox.Show(myException.Message, "Excel Export", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void SaveCSV(DataTable dt, string docName)
        {
            List<string> lines = new List<string>();

            string[] columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();

            string header = string.Join(",", columnNames);
            lines.Add(header);

            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder str = new StringBuilder();
                foreach (object cell in dr.ItemArray)
                {
                    str.Append(cell.ToString().Replace(",", " "));
                    str.Append(",");

                }
                lines.Add(str.Remove(str.Length - 1, 1).ToString());
            }

            System.IO.StreamWriter file = new System.IO.StreamWriter(docName);
            foreach (string line in lines)
                file.WriteLine(line);
            file.Close();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (new HelpWindow()).ShowDialog();
            //MessageBox.Show("Ctrl + G -> GO"
            //                + "\nCtrl + S -> Save All"
            //                + "\nCtrl + Shift + S -> Save Selected Rows");
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save((DataTable)dgv_Results.DataSource);
        }

        private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(getSelectedRows());
        }

        private void dgv_Results_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgv_Results.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = !(bool)cell.Value;
                int id = housesToDisplay.ElementAt(cell.RowIndex).id;
                if ((bool)cell.Value)
                {
                    DataMgr.DataMgr.user.favouriteHouses.Add(id);
                }
                else
                {
                    DataMgr.DataMgr.user.favouriteHouses.Remove(id);
                }
            }
        }

        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void ShowDetails()
        {
            List<int> rowIndex = new List<int>();
            foreach (DataGridViewCell dgvr in dgv_Results.SelectedCells)
                rowIndex.Add(dgvr.RowIndex);

            foreach (int index in rowIndex.Distinct())
                (new Details(housesToDisplay.ElementAt(index))).Show();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendEmail();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataMgr.DataMgr.inputFavorites(DataMgr.DataMgr.user.UserName, DataMgr.DataMgr.user.favouriteHouses);
            DataMgr.DataMgr.inputFilters(DataMgr.DataMgr.user.UserName, DataMgr.DataMgr.user.Filter);
        }

        private void inMapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMaps();
        }
    }
}
