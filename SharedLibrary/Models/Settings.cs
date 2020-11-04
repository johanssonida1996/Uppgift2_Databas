using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Settings
    {
        public Settings()
        {

        }


       public Settings(int numberOfItems, string statusRegistered, string statusActive, string statusClosed)
        {
            NumberOfItems = numberOfItems;
            StatusRegistered = statusRegistered;
            StatusActive = statusActive;
            StatusClosed = statusClosed;
        }

        public int NumberOfItems { get; set; }
        public string StatusRegistered { get; set; }
        public string StatusActive { get; set; }
        public string StatusClosed { get; set; }             


        }
}
