using Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Domain.Aggregates.EmployeeAggregate
{
    public class Update 
    {
        
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Update(string Address,string phoneNumber)
        {
        
            this.Address = Address;
            this.PhoneNumber = phoneNumber;
        }
        public Update()
        {

        }
    }
}
