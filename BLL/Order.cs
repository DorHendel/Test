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
    public class Order
    {
        public const int UNDEFINED_ID = -1;

        private int id;
        private int customerId;
        private int requestedWeek;
        private int roomType;
        private double orderPrice;
        private int resortId;
        private int year;

        public int ID
        {
            get { return id; }
        }
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public int RequestedWeek
        {
            get { return requestedWeek; }
            set { requestedWeek = value; }
        }
        public int RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }
        public double OrderPrice
        {
            get { return orderPrice; }
            set { orderPrice = value; }
        }
        public int ResortId
        {
            get { return resortId; }
            set { resortId = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public Order()
        {
            this.id = UNDEFINED_ID;
        }

        //Constructor that pulls an order from the database by its id
        public Order(int id)
        {
            try
            {
                DataRow row = DBOrders.GetOrder(id);
                if (row == null)
                    throw new Exception("Could not find order");
                this.id = (int)row["ID"]; 
                customerId = (int)row["CustomerID"];
                requestedWeek = (int)row["RequestedWeek"];
                roomType = (int)row["RoomType"];
                orderPrice = (int)row["OrderPrice"];
                resortId = (int)row["ResortID"];
                year = (int)row["year"];

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Constructor that creates a new order
        public Order(Customer c, int requestedWeek, Rooms rooms, ResortVillage r)
        {
            try
            {

                this.customerId = c.ID;
                this.requestedWeek = requestedWeek;
                this.roomType = rooms.RoomType;
                this.orderPrice = rooms.ListPrice;
                this.resortId = r.ID;
                DateTime t = DateTime.Now;
                this.year = (int)t.Year;
                this.id = UNDEFINED_ID;





            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Saves the order in the database
        public void SaveOrder()
        {

            try
            {
                int res = DBOrders.InsertOrder(customerId, requestedWeek, year, resortId, roomType, orderPrice);
                this.id = res;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
