using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;


namespace BLL
{
    public class Rooms
    {
        public const int UNDEFINED_ID = -1;

        int roomType;
        int roomNum;
        int resortId;
        double listPrice;

        public int RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }

        public int ResortId
        {
            get { return resortId; }
            
        }
        public int RoomNum
        {
            get { return roomNum; }
            set { roomNum = value; }
        }
        public double ListPrice
        {
            get { return listPrice; }
            set { listPrice = value; }
        }

        //Constructor that gets a rooms datarow from the database
        public Rooms(DataRow row)
        {
            try
            {
                roomType = (int)row["RoomType"];
                roomNum = (int)row["RoomTypeNum"];
                resortId = (int)row["ResortID"];
                listPrice = (int)row["ListPrice"];
            }

            catch(Exception e)
            {
                throw e;
            }
        }

        //Constructor that pulls a resorts rooms from the database by the resorts id and a roomtype
        public Rooms(int resortId, int roomType)
        {
            try
            {
                DataRow row = DBRooms.GetRooms(resortId, roomType);
                this.roomType = (int)row["RoomType"];
                roomNum = (int)row["RoomTypeNum"];
                this.resortId = (int)row["ResortID"];
                listPrice = (int)row["ListPrice"];
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        //An empty constructor
        public Rooms()
        {
            
        }

        //Checks if there is an available room in a specific week
        public bool IsThereAvailableRoom(int week)
        {
            
            try
            {
                int ordernum;
                DataTable table = DBRooms.GetRoomOrdersInWeek(roomType, resortId, week);
                if (table == null)
                    ordernum = 0;
                else
                    ordernum = table.Rows.Count;
                if (roomNum - ordernum > 0)
                    return true;
                return false;
                 
            }
            catch
            {
                throw new Exception("Error");
            }
        }
    }
}
