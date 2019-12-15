using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class DBRooms
    {
        const string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        const string SOURCE = @"..\..\ClubMedDatabase.accdb";

        //Returns a DataTable of all rooms within a specific resort village
        public static DataTable GetRoomsForResort(int idResort)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Rooms WHERE ResortID = {idResort} ;";
                return db.GetDataTable(sql);
            }
            catch
            {
                throw new Exception("Could not retrieve Rooms");
            }
        }

        //Returns a DataTable of all rooms within a specific resort village and of a specific roomType
        public static DataRow GetRooms(int idResort, int roomType)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Rooms WHERE ResortID = {idResort} AND Rooms.RoomType = {roomType} ;";
                return db.GetDataTable(sql).Rows[0];
            }
            catch
            {
                throw new Exception("Could not find Rooms");
            }
        }

        //Returns a datatable of all the orders in a specific week, resort and room type
        public static DataTable GetRoomOrdersInWeek(int roomType, int resortID, int week)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Orders WHERE RoomType = {roomType} AND ResortID = {resortID} AND RequestedWeek = {week} AND Year = {DateTime.Now.Year} ;";
                return db.GetDataTable(sql);
            }
            catch
            {
                throw new Exception("No orders found");
            }
        }
    }
}
