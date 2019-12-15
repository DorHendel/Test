using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BLL;

namespace UI
{

/// <summary>
/// Projects ICollections
/// </summary>
/// 
    class ObjectsList: Screen
    {
        private const int COLUMN_LENGTH = 25;
        private int columnLength;
        protected ICollection<Object> data;
        public ObjectsList(string title, ICollection<Object> data) : base(title)
        {
            this.data = data;
            columnLength = COLUMN_LENGTH;
        }
        public int ColumnLength
        {
            get
            {
                return columnLength;
            }
            set
            {
                columnLength = value;
            }
        }
        private string CreateSpace(string s)
        {
            
            for(int i = 0; i < COLUMN_LENGTH; i++)
            {
                s += "   ";
            }
            return s.Substring(0, COLUMN_LENGTH);
        }
        public override void Show()
        {
            //Display title
            Console.WriteLine($"\t{Title}");

            //check if list contains data
            if (data.Count == 0)
            {
                Console.WriteLine("\tNo Data Found");
                return;
            }
            //Get the type of the object!
            Type t = data.ElementAt<Object>(0).GetType();
            // Get the public properties of the instance (not only related to Object).
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Display information for all properties.
            Console.Write("\t");
            foreach (PropertyInfo propInfo in propInfos)
            {
                bool readable = propInfo.CanRead;
                if (readable)
                {
                    Object firstObj = data.ElementAt<Object>(0);
                    Object prop = propInfo.GetValue(firstObj);
                    if (prop != null && !(prop is System.Collections.ICollection))
                        Console.Write("{0}", CreateSpace(propInfo.Name));
                }
            }

            //list values for all data objects

            foreach (Object obj in data)
            {
                Console.WriteLine();
                Console.Write("\t");
                foreach (PropertyInfo propInfo in propInfos)
                {
                    bool readable = propInfo.CanRead;
                    if (readable)
                    {
                        Object prop = propInfo.GetValue(obj);
                        if (prop != null && !(prop is System.Collections.ICollection))
                            Console.Write("{0}", CreateSpace(propInfo.GetValue(obj).ToString()));
                    }
                }
            }
        }
    }
}
