using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            UIMain main = new UIMain(new MainScreen());
            main.ApplicationStart();
            
           
        }
    }
}
