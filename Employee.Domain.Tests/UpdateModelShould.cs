using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Employee.Domain.Entities;
using Employee.Domain.Aggregates;
using Employee.Domain.Aggregates.EmployeeAggregate;

namespace Employee.Domain.Tests
{
    [TestFixture]
    public class UpdateModelShould
    {
        [Test]
        public void createNewToDoListViaConstructor()
        {
            
          
            string Address = "Test Address";
            string phoneNumber = "7896541230";
            Update update = new Update(Address,phoneNumber);
            Assert.That(update, Is.Not.Null);
            Assert.That(update, Is.InstanceOf<Update>());
            Assert.That(update.Address, Is.EqualTo(Address));
            Assert.That(update.PhoneNumber, Is.EqualTo(phoneNumber));
         


        }
    }
}
