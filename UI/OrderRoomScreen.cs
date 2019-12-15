using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;


namespace UI
{
    /// <summary>
    /// A screen that lets the customer order a room 
    /// </summary>
    class OrderRoomScreen : Screen
    {

        public OrderRoomScreen() : base("Order a room") { }
        public override void Show()
        {
            base.Show();


            BLHelper.GetResortVillages();
            List<object> list = BLHelper.resorts.ToList<object>();
            foreach(ResortVillage r in list)
            {
                r.LoadRooms();
            }
            ObjectsList oblist = new ObjectsList("Resorts", list);
            oblist.Show();
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine("What resort would you like to order from?(type in the ID)");

            

            int resortid =-1;
            ResortVillage resort = new ResortVillage();
            bool valid = false;
            while (!valid)
            {
                try
                {
                   
                    int.TryParse(Console.ReadLine(), out resortid);
                    resort = new ResortVillage(resortid);
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Invalid resort ID, please try again");
                }

            }

            List<object> roomsl = resort.LoadRooms().ToList<object>();
            ObjectsList roomsList = new ObjectsList("Rooms", roomsl);
            roomsList.Show();
            Console.WriteLine();
            Console.WriteLine();

            valid = false;
            int roomType;
            Console.WriteLine("What type of room would you like to order? (write the room type number)");
            Rooms rooms = new Rooms();
            while (!valid)
            {
                try
                {
                    
                    int.TryParse(Console.ReadLine(), out roomType);
                    rooms = new Rooms(resortid, roomType);
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Invalid room type");
                }
            }
            
            int requestedweek = -1;
            valid = false;
            Console.WriteLine("What week would you like to order?");
            while (!valid)
            {
                try
                {
                    
                    int.TryParse(Console.ReadLine(), out requestedweek);
                    if (resort.StartActivityTime > requestedweek || resort.EndActivityTime < requestedweek)
                        throw new Exception("The resort is not open in the requested week, please try again");
                    valid = rooms.IsThereAvailableRoom(requestedweek);
                    if (!valid)
                        throw new Exception("There arent any free rooms in the requested week, please try again");
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            int cusId = -1;
            Customer customer = new Customer();
            valid = false;
            Console.WriteLine("What is your customer ID?");
            while (!valid)
            {
                try
                {
                    
                    int.TryParse(Console.ReadLine(), out cusId);
                    customer = new Customer(cusId);
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Could not find this customer");
                }
            }

            try
            {
                Console.WriteLine("Are you sure? ( 1 for yes, 2 to return to menu)");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();
                char ch = keyInfo.KeyChar;
                if (ch == '2')
                {
                    return;
                }
                Order order = new Order(customer, requestedweek, rooms, resort);
                order.SaveOrder();
                Console.WriteLine("Order successfull, order id: " + order.ID);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Press any key to return to main menu");
            Console.ReadKey();

        }


    }
}
