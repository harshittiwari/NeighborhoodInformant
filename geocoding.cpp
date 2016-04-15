// Note: the listbox was used to display the debugging steps to make sure 
// it was going through all the lines.

// The lat/long is not guaranteed to be correct depending on what the user gives as input.
// Should make it so that at the very least the user should enter street and zipcode
// to ensure the greatest change of getting the correct result


using System.Xml.Linq;
using System.Net;

private void search_Button_Click(object sender, EventArgs e)
{

    //string street = "1600 Amphitheatre Parkway";  // sample street address
    //string zipcode = "94043";


	if (textBox1.Text.Length == 0) {
        MessageBox.Show("Please enter an address.");
        return;
    }

    var address = textBox1.Text;                        // takes address from textbox
    var address_new = address.Replace(" ", "+");             // changes spaces to plus signs for url

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
        var lat = locationElement.Element("lat").Value;
        var lng = locationElement.Element("lng").Value;
 
       	//listBox1.Items.Add(lat + ", " + lng);
        
        MessageBox.Show(lat + ", " + lng);

    } catch (Exception ex) {
        MessageBox.Show(ex.Message.ToString(), "Error");
    }
}