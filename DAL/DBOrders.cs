using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class DBOrders
    {

        const string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        const string SOURCE = @"..\..\ClubMedDatabase.accdb";

        //Returns DataTable of all orders of a specifc customer
        public static DataTable GetCustomerOrders(int customerId)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Orders WHERE CustomerID = {customerId} ;";
                return db.GetDataTable(sql);
            }
            catch
            {
                throw new Exception("Could not find Orders");
            }
        }

        //Returns DataRow of a specific order by its ID
        public static DataRow GetOrder(int id)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Orders WHERE ID = {id} ;";
                return db.GetDataTable(sql).Rows[0];
            }
            catch
            {
                throw new Exception("Could not find Order");
            }
        }

        //Inserts a new order into the database
        public static int InsertOrder(int customerId, int requestedWeek, int year, int resortId, int roomType, double orderPrice)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"INSERT INTO Orders ( CustomerID, RequestedWeek, RoomType, OrderPrice, ResortID, [Year] ) VALUES({customerId}, {requestedWeek}, {roomType}, {orderPrice}, {resortId}, {year}) ;";
                int newID = db.InsertWithAutoNumKey(sql);
                if (newID == DBHelper.WRITEDATA_ERROR)
                    throw new Exception("");
                return newID;
            }
            catch
            {
                throw new Exception("Could not insert Order into database");
            }
        }

        
        
    }
}
