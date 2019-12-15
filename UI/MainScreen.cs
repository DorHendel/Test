using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace UI
{
    /// <summary>
    /// The main screen
    /// </summary>
    public class MainScreen : Menu
    {
        public MainScreen(): base("Main Menu")
        {
            AddItem("Insert Customer", new AddCustomerScreen());
            AddItem("Find customer by last name", new FindCustomerScreen());
            AddItem("Find customer by ID", new CustomerViewScreen());
            AddItem("Order a Room", new OrderRoomScreen());
        }
    }
}
