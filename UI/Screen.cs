using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace UI
{
    /// <summary>
    /// A base to the screens
    /// </summary>
    public class Screen
    {
        public string Title { get; set; }
        public Screen(string title)
        {
            Title = title;
        }
        public virtual void Show()
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\t\t{Title}");
        }
    }
}
