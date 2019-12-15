using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.OleDb;

namespace BLL
{
    public class ResortVillage
    {
        public const int UNDEFINED_ID = -1;

        private int id;
        private string resortName;
        private int mainActivity;
        private int startActivityTime;
        private int rating;
        private string country;
        private string city;
        private string streetNum;
        private int endActivityTime;
        private List<Rooms> roomsList;

        public int ID
        {
            get { return id; }
        }
        public string ResortName
        {
            get { return resortName; }
            set { resortName = value; }
        }
        public int MainActivity
        {
            get { return mainActivity; }
            set { mainActivity = value; }
        }
        public int StartActivityTime
        {
            get { return startActivityTime; }
            set { startActivityTime = value; }
        }
        public int EndActivityTime
        {
            get { return endActivityTime; }
            set { endActivityTime = value; }
        }
        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string StreetNum
        {
            get { return streetNum; }
            set { streetNum = value; }
        }

        public List<Rooms> RoomsList
        {
            get { return roomsList; }
        }

        public ResortVillage()
        {

        }

        public ResortVillage(string resortName,
                                int mainActivity,
                                int startActivityTime,
                                int rating,
                                string country,
                                string city,
                                string streetNum,
                                int endActivityTime
                            )
        {
            this.id = UNDEFINED_ID;
            this.resortName = resortName;
            this.mainActivity = mainActivity;
            this.startActivityTime = startActivityTime;
            this.rating = rating;
            this.country = country;
            this.city = city;
            this.streetNum = streetNum;
            this.endActivityTime = endActivityTime;
            roomsList = null;
        }

        //Constructor that gets a resort's datarow from the database
        public ResortVillage(DataRow dr)
        {
            id = (int)dr["ID"];
            resortName = (string)dr["ResortName"];
            mainActivity = (int)dr["MainActivity"];
            startActivityTime = (int)dr["StartActivityTime"];
            rating = (int)dr["Rating"];
            country = (string)dr["Country"];
            city = (string)dr["City"];
            streetNum = (string)dr["StreetNum"];
            endActivityTime = (int)dr["EndActivityTime"];
            roomsList = null;
        }

        //Constructor that pulls a resorts from the database by its id
        public ResortVillage(int id)
        {
            DataRow dr = DBResortVillages.GetResortVillageById(id);

            this.id = (int)dr["ID"];
            resortName = (string)dr["ResortName"];
            mainActivity = (int)dr["MainActivity"];
            startActivityTime = (int)dr["StartActivityTime"];
            rating = (int)dr["Rating"];
            country = (string)dr["Country"];
            city = (string)dr["City"];
            streetNum = (string)dr["StreetNum"];
            endActivityTime = (int)dr["EndActivityTime"];
            roomsList = null;
        }

        //Loads all rooms of the resorts
        public List<Rooms> LoadRooms()
        {
            if (roomsList == null)
            {
                DataTable table = DBRooms.GetRoomsForResort(this.id);
                roomsList = new List<Rooms>();

                foreach (DataRow r in table.Rows)
                {
                    Rooms o = new Rooms(r);
                    roomsList.Add(o);
                }
            }

            return roomsList;
        }

    }
}
