using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeighborhoodInformantApp.Model
{
    public class User
    {
        public int UserId;
        public List<int> favouriteHouses;

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                favouriteHouses = DataMgr.DataMgr.GetFavouriteHouses(_userName);
                Filter = DataMgr.DataMgr.GetFilters(_userName);
            }
        }

        public Dictionary<string, string> Filter;

        public User()
        {
            favouriteHouses = new List<int>();
            Filter = new Dictionary<string, string>();

        }
    }
}
