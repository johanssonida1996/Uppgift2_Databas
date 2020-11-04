using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class SupportCases
    {

        public SupportCases()
        {

        }

        public SupportCases(string description, string title, string category)
        {
            Status = "Registered";
            Description = description;
            Title = title;
            Category = category;
            Time = DateTime.Now;
        }

        public SupportCases(int casenumber, int customernumber, string status, string description, string title, string category, DateTime time)
        {
            CaseNumber = casenumber;
            CustomerNumber = customernumber;
            Status = status;
            Description = description;
            Title = title;
            Category = category;
            Time = time;
        }


        public int CaseNumber { get; set; }
        public int CustomerNumber { get; private set; }
        public string Status { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public string Category { get; private set; }

        public DateTime Time { get; private set; }
        public string Summary => $"{CaseNumber}, {CustomerNumber}, {Status}, {Description}, {Title}, {Category}, {Time}";
    }

}
