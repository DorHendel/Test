using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// Recieves a screen and starts it
    /// </summary>
    class UIMain : Screen
    {
        Screen initialScreen;
        public UIMain(Screen initial) : base(initial.Title)
        {
            this.initialScreen = initial;
        }
        public void ApplicationStart()
        {
            initialScreen.Show();
        }
    }
}
