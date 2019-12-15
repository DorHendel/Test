using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    
    public class DBCustomer
    {
        const string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        const string SOURCE = @"..\..\ClubMedDatabase.accdb";

        //Inserts customer into the database
        public static int InsertCustomer(string fname, string lname, string country, string city, string address)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"INSERT INTO Customers (FName, LName, Country, City, Address) VALUES ('{fname}','{lname}', '{country}', '{city}', '{address}') ;";

                int newID = db.InsertWithAutoNumKey(sql);
                if (newID == DBHelper.WRITEDATA_ERROR)
                    throw new Exception("");
                return newID;

            }
            catch
            {
                throw new Exception("Could not insert Customer into database");
            }

        }

        //Returns DataTable of all customers with a specific last name
        public static DataTable GetCustomerByLName(string lname)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Customers WHERE Customers.LName = '{lname}' ;";
                return db.GetDataTable(sql);

            }
            catch
            {
                throw new Exception("Could not find Customer");
            }
        }

        //Returns DatarRow of a customer with a specific ID
        public static DataRow GetCustomerByID(int id)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Customers WHERE Customers.ID = {id} ;";
                return db.GetDataTable(sql).Rows[0];
            }
            catch
            {
                throw new Exception("Could not find Customer");
            }
        }
    }
}
