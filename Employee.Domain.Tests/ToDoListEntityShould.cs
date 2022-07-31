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
    public class ToDoListEntityShould
    {
        [Test]
        public void createNewToDoListViaConstructor()
        {
            int Id = 0;
            int EmployeeID = 1;
            int ManagerID = 2;
            string Pending_task = "New task used for testing";
            DateTime Due_Date = new DateTime(2022, 07, 10);
            string Work_assigned_by = "Manager";
            string status = "Not Completed";
            ToDoList todo = new ToDoList(EmployeeID, ManagerID, Pending_task, Due_Date, Work_assigned_by, status);
            Assert.That(todo, Is.Not.Null);
            Assert.That(todo, Is.InstanceOf<ToDoList>());
            Assert.That(todo.EmployeeID, Is.EqualTo(EmployeeID));
            Assert.That(todo.ManagerID, Is.EqualTo(ManagerID));
            Assert.That(todo.Pending_task, Is.EqualTo(Pending_task));
            Assert.That(todo.Due_Date, Is.EqualTo(Due_Date));
            Assert.That(todo.Work_assigned_by, Is.EqualTo(Work_assigned_by));
            Assert.That(todo.status, Is.EqualTo(status));


        }
    }
}
