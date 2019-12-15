using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Reflection;
using System.Data;
using System.Data.OleDb;

namespace BLL
{
    public class BLHelper
    {
        //Loads all customers with specific last name
        public static List<Customer> customers = new List<Customer>();
        public static void GetCustomersByLName(string lname)
        {
            customers.Clear();
            DataTable dt = DBCustomer.GetCustomerByLName(lname);
            foreach (DataRow dr in dt.Rows)
            {
                customers.Add(new Customer(dr));
            }
        }
        //Loads all resort villages
        public static List<ResortVillage> resorts = new List<ResortVillage>();
        public static void GetResortVillages()
        {
            DataTable dt = DBResortVillages.GetResortVillages();
            foreach (DataRow dr in dt.Rows)
            {
                resorts.Add(new ResortVillage(dr));
            }
        }

        //Creates string for an arbitrary object
        public static string ObjectToString(Object o)
        {
            string str = "";

            Type t = o.GetType();
            str += "Type: " + t.Name;
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propInfo in propInfos)
            {
                string propName = propInfo.Name;
                Type propType = propInfo.PropertyType;
                Object value = propInfo.GetValue(o);
                if (value is System.Collections.ICollection || !propInfo.CanRead)
                {
                    str += String.Format("\n\tPropertyName: {0, -16}PropertyType: {1, -24}", propName, propType);
                }
                else
                {
                    str += String.Format("\n\tPropertyName: {0, -16}PropertyType: {1, -24}PropertyValue: {2, -16}", propName, propType, value);
                }
            }
            str += "\n========================================================================================================================";
            return str;
        }
        //Creates string for an arbitrary ICollection
        public static string ListToString(System.Collections.ICollection collection)
        {
            string str = "";
            foreach (Object o in collection)
            {
                str += ObjectToString(o) + "\n";
            }
            return str;
        }
    }
}
