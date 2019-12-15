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
    public class Customer
    {
        public const int UNDEFINED_ID = -1;

        private int id;
        private string fname;
        private string lname;
        private string country;
        private string city;
        private string address;
        private List<Order> orders;

        public int ID
        {
            get { return id; }
        }
        public string FName
        {
            get { return fname; }
            set { fname = value; }
        }
        public string LName
        {
            get { return lname; }
            set { lname = value; }
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
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public List<Order> Orders
        {
            get { return orders; }
        }
        public Customer()
        {
            this.id = UNDEFINED_ID;
        }


        //Constructor that creates a new customer
        public Customer(string FName, string LName, string Country, string City, string Address)
        {
            this.id = UNDEFINED_ID;
            this.fname = FName;
            this.lname = LName;
            this.country = Country;
            this.city = City;
            this.address = Address;
            orders = null;
        }

        //Constructor that pulls a customer from the database by his id
        public Customer(int id)
        {
            DataRow dr = DBCustomer.GetCustomerByID(id);
            if (dr == null)
                throw new Exception($"Fail to read customer ID:{ID}");

            this.id = id;
            fname = (string)dr["FName"];
            lname = (string)dr["LName"];
            country = (string)dr["Country"];
            city = (string)dr["City"];
            address = (string)dr["Address"];
            orders = null;
        }

        //Constructor that gets a customers datarow from the database
        public Customer(DataRow dr)
        {
            if (dr == null)
                throw new Exception($"DataRow was null");

            id = (int)dr["ID"];
            fname = (string)dr["FName"];
            lname = (string)dr["LName"];
            country = (string)dr["Country"];
            city = (string)dr["City"];
            address = (string)dr["Address"];
            orders = null;
        }

        //Loads all orders of the customer into a list
        public List<Order> LoadOrders()
        {
            if (orders == null)
            {
                DataTable table = DBOrders.GetCustomerOrders(this.id);
                orders = new List<Order>();

                foreach (DataRow r in table.Rows)
                {
                    Order o = new Order((int)r["ID"]);
                    orders.Add(o);
                }
            }

            return orders;

        }
        public override string ToString()
        {
            return BLHelper.ObjectToString(this);
        }

        //This method saves the customer in the database
        public void SaveCustomer()
        {
            try
            {
                int res = DBCustomer.InsertCustomer(fname, lname, country, city, address);
                this.id = res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
