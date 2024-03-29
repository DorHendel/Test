﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class DBHelper
    {
        //Constants
        public const int WRITEDATA_ERROR = -10;

        //class member variables
        private OleDbConnection conn; //holds the connection for all operations using OleDB
        private string provider; //holds the provider for the connection
        private string source; //holds the source database file (full path)
        private bool connOpen; //indicates if the connection is opened

        //Construct the object with a provided provider and source
        public DBHelper(string provider, string source)
        {
            this.connOpen = false;
            this.conn = null;
            this.provider = provider;
            this.source = source;
        }

        //Open connection to the database. return true if succeed.
        //if not succeed writes to the devug window information from the exception
        public bool OpenConnection()
        {
            if (this.connOpen)
                return true;
            string connString = BuildConnString();

            try
            {
                OleDbConnection conn = new OleDbConnection(connString);
                conn.Open();
                this.conn = conn;
                this.connOpen = true;
                return true;
            }
            catch (OleDbException e)
            {
                this.conn = null;
                return false;
            }
        }

        //Execute SELECT sql commands and return a reference to an OleDbDataReader (forward ONLY)
        //if execution fails return null! and writes to the devug window information about the reason for failure.
        public OleDbDataReader ReadData(string sql)
        {
            try
            {
                if (!this.connOpen)
                {
                    if (!this.OpenConnection())
                        return null;
                }
                OleDbCommand cmd = new OleDbCommand(sql, this.conn);
                OleDbDataReader rd = cmd.ExecuteReader();
                return rd;
            }
            catch (OleDbException e)
            {
                return null;
            }
        }

        //Execute UPDATE or INSERT sql commands and return number of rows affected.
        //return WRITEDATA_ERROR on failure and writes information to the debug window.
        public int WriteData(string sql)
        {
            try
            {
                if (!this.connOpen)
                {
                    if (!this.OpenConnection())
                        return WRITEDATA_ERROR;
                }
                OleDbCommand cmd = new OleDbCommand(sql, this.conn);
                OleDbDataReader rd = cmd.ExecuteReader();
                return rd.RecordsAffected;
            }
            catch (OleDbException e)
            {
                return WRITEDATA_ERROR;
            }
        }
        //This function should be used for inserting a single record into a table in the database with an autonmuber key. the format of the sql must be 
        //INSERT INTO <TableName> (Fields...) VALUES (values...)
        //the function return the autonumber key generated for the new record or WRITEDATA_ERROR if fail.

        public int InsertWithAutoNumKey(string sql)
        {
            try
            {
                int newID = WRITEDATA_ERROR;
                if (!this.connOpen)
                {
                    if (!this.OpenConnection())
                        return WRITEDATA_ERROR;
                }
                OleDbCommand cmd = new OleDbCommand(sql, this.conn);
                OleDbDataReader rd = cmd.ExecuteReader();
                cmd = new OleDbCommand(@"SELECT @@Identity", this.conn);
                rd = cmd.ExecuteReader();
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        newID = (int)rd[0];
                    }
                    return newID;
                }
                return WRITEDATA_ERROR;
            }
            catch (OleDbException e)
            {
                return WRITEDATA_ERROR;
            }
        }

        //CLosing connection!
        public void CloseConnection()
        {
            try
            {
                if (!this.connOpen)
                    return;
                this.conn.Close();
                this.connOpen = false;
            }
            catch (OleDbException e)
            {
            }
        }

        //This function builds the connection string to be used to open connection. 
        //Make sure to set source and provider prior to using this function
        // Example: @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\oferz\Documents\Database1.mdb;Persist Security Info=False;";
        private string BuildConnString()
        {
            return String.Format(@"Provider={0};Data Source={1};Persist Security Info=False;", this.provider, this.source);
            //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\NorthWindForClass.accdb;Persist Security Info=False;
        }

        //*** Use these method if you want to use a data table or data sets for full caching of all data in memory *******
        //This function reads from the database a data table fully cached in memory using a standard SQL SELECT statement.
        //The function returns the data table or null on failure.
        public DataTable GetDataTable(string sql)
        {
            try
            {
                string query = sql;
                DataTable dataTable = new DataTable();
                OleDbDataReader reader = ReadData(sql);
                if (null == reader)
                    return null;
                dataTable.Load(reader);

                return dataTable;
            }
            catch (OleDbException e)
            {
                return null;
            }
        }

        //This function reads from the database a data set fully cached in memory using an array of standard SQL SELECT statements.
        //The function returns the data set or null on failure. The table names inside the dataset are sql1, sql2,...
        public DataSet GetDataSet(string[] sql)
        {
            try
            {
                string connString = BuildConnString();
                DataTable[] tables = new DataTable[sql.Length];
                DataSet ds = new DataSet();
                for (int i = 0; i < sql.Length; i++)
                {

                    tables[i] = GetDataTable(sql[i]);
                    if (null == tables[i])
                        return null;
                    tables[i].TableName = "sql" + (i + 1);

                    // this will query your database and return the result to your datatable
                    ds.Tables.Add(tables[i]);
                }
                return ds;
            }
            catch (OleDbException e)
            {
                return null;
            }
        }
    }
}

