using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace UI
{
    /// <summary>
    /// A screen that lets the customer search customers by their last name
    /// </summary>
    class FindCustomerScreen : Screen
    {
        public FindCustomerScreen() : base("search customer by last name") { }
        public override void Show()
        {
            base.Show();
            bool check = false;
            int i;
            string cusLName = "";
            Console.WriteLine("please enter customer last name");
            while (!check)
            {
                i = 0;
                check = true;
                cusLName = Console.ReadLine();
                while (i < cusLName.Length)
                {
                    if (!((cusLName[i] <= 'z' && cusLName[i] >= 'a') || (cusLName[i] >= 'A' && cusLName[i] <= 'Z')))
                    {
                        check = false;
                    }
                    i++;
                }
                if (check == false)
                {
                    Console.WriteLine("try again");
                }
                
                    
            }
            

            try
            {
                
                BLHelper.GetCustomersByLName(cusLName);
                List<Customer> list = BLHelper.customers; 
                List<object> objectList = list.ToList<Object>();
                ObjectsList customersList = new ObjectsList("customers",objectList);
                customersList.Show();
                Console.WriteLine("");
                Console.WriteLine("");
                if (list.Count == 0)
                {
                    Console.WriteLine("press any key to return...");
                    Console.ReadKey();
                }
                    
                else
                {
                    Console.WriteLine("Press c to view a specific customer's details, any key to return...");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    Console.WriteLine();
                    char ch = keyInfo.KeyChar;
                    if (ch == 'c' || ch == 'C')
                    {
                        CustomerViewScreen customerViewScreen = new CustomerViewScreen();
                        customerViewScreen.Show();
                    }
                }
            }
            catch
            {
                Console.WriteLine($"failed to recieve data for customer last name - {cusLName}, sorry");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
            }
        }
    }
}
