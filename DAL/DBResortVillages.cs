using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class DBResortVillages
    {
        const string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        const string SOURCE = @"..\..\ClubMedDatabase.accdb";

        //Returns a DataTable of all resort villages
        public static DataTable GetResortVillages()
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Resort ;";
                return db.GetDataTable(sql);
            }
            catch
            {
                throw new Exception("Could not retrieve ResortVillages");
            }
        }

        //Returns a DataRow of a specific resort village by its ID
        public static DataRow GetResortVillageById(int id)
        {
            try
            {
                DBHelper db = new DBHelper(PROVIDER, SOURCE);
                string sql = $"SELECT* FROM Resort WHERE Resort.ID = {id} ;";
                DataTable table = db.GetDataTable(sql);
                return table.Rows[0];
            }
            catch
            {
                throw new Exception("Could not find ResortVillage");
            }
        }


    }
}
