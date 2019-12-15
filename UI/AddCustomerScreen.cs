using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace UI
{
    /// <summary>
    /// A screen that lets the user upload a new customer to the database
    /// </summary>
    public class AddCustomerScreen : Screen
    {
        public AddCustomerScreen() : base("Add customer to database")
        {

        }
        
        public override void Show()
        {

            base.Show();
            bool check = false;
            string FName = "";
            string LName = "";
            string Country = "";
            string City = "";
            string Address = "";
            Console.WriteLine("Enter customer's first name, press (-) to exit");
            while (!check)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char ch = keyInfo.KeyChar;
                if (ch == '-')
                {
                    return;
                }
                check = true;
                FName = ch + Console.ReadLine();
                int i = 0;
                while (i < FName.Length)
                {
                    

                    if (!((FName[i] <= 'z' && FName[i] >= 'a') || (FName[i] >= 'A' && FName[i] <= 'Z')))
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
            check = false;
            Console.WriteLine("Enter customer's last name, press (-) to exit");
            while (!check)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char ch = keyInfo.KeyChar;
                if (ch == '-')
                {
                    return;
                }
                check = true;
                LName = ch + Console.ReadLine();
                int i = 0;
                while (i < LName.Length)
                {
                    if (!((LName[i] <= 'z' && LName[i] >= 'a') || (LName[i] >= 'A' && LName[i] <= 'Z')))
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

            Console.WriteLine("Enter customer's country, press (-) to exit");
            check = false;
            while (!check)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char ch = keyInfo.KeyChar;
                if (ch == '-')
                {
                    return;
                }
                check = true;
                Country = ch + Console.ReadLine();
                int i = 0;
                while (i < Country.Length)
                {
                    if (!((Country[i] <= 'z' && Country[i] >= 'a') || (Country[i] >= 'A' && Country[i] <= 'Z')))
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
            Console.WriteLine("Enter customer's city, press (-) to exit");
            check = false;
            while (!check)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char ch = keyInfo.KeyChar;
                if (ch == '-')
                {
                    return;
                }
                check = true;
                City = ch + Console.ReadLine();
                int i = 0;
                while (i < City.Length)
                {
                    if (!((City[i] <= 'z' && City[i] >= 'a') || (City[i] >= 'A' && City[i] <= 'Z')))
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
            Console.WriteLine("Enter customer's Address, press (-) to exit");
            check = false;
            while (!check)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char ch = keyInfo.KeyChar;
                if (ch == '-')
                {
                    return;
                }
                check = true;
                Address = ch + Console.ReadLine();
                int i = 0;
                while (i < Address.Length)
                {
                    if (!((Address[i] <= 'z' && Address[i] >= 'a') || (Address[i] >= 'A' && Address[i] <= 'Z') || (Address[i] >=0 && Address[i]<=9)))
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
                    Customer cus = new Customer(FName, LName, Country, City, Address);
                    cus.SaveCustomer();
                    Console.WriteLine("Customer was successfully added, new id: " + cus.ID);
                    Console.WriteLine("Press any key to main menu");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to main menu");
                }

                Console.ReadKey();

            }
        
    }
}
