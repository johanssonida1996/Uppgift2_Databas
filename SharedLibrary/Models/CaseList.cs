using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
     public class CaseList
    {
        public CaseList()
        {

        }

        public CaseList(Customer customer, SupportCases supportcases)
        {
            Customer = customer;
            SupportCases = supportcases;

        }

        public Customer Customer { get; set; }
        public SupportCases SupportCases { get; set; }

        public string Summary => $"{SupportCases.CaseNumber}, {SupportCases.Title}, {SupportCases.Status}";
    }

    public class CaseListViewModel
    {
        public ObservableCollection<CaseList> Case { get; } = new ObservableCollection<CaseList>();

        public CaseListViewModel(string status1, string status2)
        {
            var result = DataAccess.GetAll(status1,status2);
            foreach (CaseList r in result)
            {
                var customer = r.Customer;
                var supportcases = r.SupportCases;

                Case.Add(new CaseList(customer, supportcases));
            }

        }
    }

}

