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
    /// Projects objects
    /// </summary>
    class ObjectView : Screen
    {
        private Object obj;
        public ObjectView(string title, Object obj) : base(title)
        {
            this.obj = obj;
        }


        public override void Show()
        {
            Console.WriteLine($"\t{Title}");
            Type t = obj.GetType();
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propInfo in propInfos)
            {
                bool readable = propInfo.CanRead;
                bool writable = propInfo.CanWrite;

                if (readable)
                {
                    Object prop = propInfo.GetValue(obj);
                    if (prop != null && !(prop is System.Collections.ICollection))
                        Console.WriteLine("\t{0}: {1}", propInfo.Name, propInfo.GetValue(obj));
                }
            }
        }
    }
}
