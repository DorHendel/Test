using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace UI
{
    /// <summary>
    /// A screen that shows the user a customer details by his id, and gives the option to also show his orders
    /// </summary>
    public class CustomerViewScreen : Screen
    {
        public CustomerViewScreen() : base("Show customer details") { }
        public override void Show()
        {
            
            Console.WriteLine("Please enter the customer's id");
            bool check = false;
            int id = -1;
            while (!check)
            {
                check = int.TryParse(Console.ReadLine(), out id);
                if (id == -1)
                {
                    Console.WriteLine("try again");
                }
                
            }
            try
            {
                Customer cus = new Customer(id);
                ObjectView view = new ObjectView("customer's details",cus);
                view.Show();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("press o to load orders, any key to return");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                check = false;
                char ch = keyInfo.KeyChar;
                Console.WriteLine();
                if (ch == 'o' || ch == 'O')
                {
                    List<object> list = cus.LoadOrders().ToList<object>();
                    ObjectsList objects = new ObjectsList("Orders", list);
                    Console.WriteLine();
                    objects.Show();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                    
                }
                

            }
            catch
            {
                Console.WriteLine($"failed to recieve data for customer id - {id}, sorry");

            }
            
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                  
    }
}
