using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(int SSN, string name, int phonenumber, string email)
        {
            SSNumber = SSN;
            Name = name;
            PhoneNumber = phonenumber;
            Email = email;
        }


        public int SSNumber { get; set; }
        public string Name { get; private set; }
        public int PhoneNumber { get; private set; }
        public string Email { get; private set; }


    }
}

