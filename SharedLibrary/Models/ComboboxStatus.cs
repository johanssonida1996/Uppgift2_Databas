using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class ComboboxStatus : ObservableCollection<string>
    {
        public ComboboxStatus()
        {
            Add("Regesterd");
            Add("Active");
            Add("Closed");
        }

    }
}