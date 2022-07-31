using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using Employee.API;
using Employee.API.Controllers;
using Employee.Domain.Entities;
using Employee.Domain.Interfaces;
using Moq;
using Employee.API.DTO;
using Employee.Domain.Aggregates.EmployeeAggregate;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpManagement.Domain.Models;

namespace Employee.API.Tests
{
    [TestFixture]
    public class EmployeeControllerShould
    {

        [Test]
        public async Task Return201StatusCode()
        {
            var dto = new ToDoListDTO()
            {
                EmployeeID = 1,
                ManagerID = 1,
                Pending_task = "Sample task",
                Due_Date = new DateTime(2022, 07, 10),
                Work_assigned_by = "Not Completed",
                status = "Not Completed",
            };
            var repository = new Mock<IRepository<ToDoList>>();
            var taskRepository = new Mock<ISpecificationRepository<GetTasksByID>>();
            var tasksReportRepository = new Mock<ISpecificationRepository<TasksReport>>();
            var leaverepo = new Mock<ISpecificationRepository<LeaveDetails>>();
            var ticketrepo = new Mock<ISpecificationRepository<RaiseTicket>>();
            repository.Setup(m => m.SaveAsync()).ReturnsAsync(1);
            var repoobj = repository.Object;
            var taskRepositoryObj = taskRepository.Object;
            var taskReportObj = tasksReportRepository.Object;
            var leaverepoObj = leaverepo.Object;
            var ticketrepoObj = ticketrepo.Object;
            var controller = new EmployeeController(repoobj, taskRepositoryObj,taskReportObj,leaverepoObj,ticketrepoObj);
            var result = (StatusCodeResult)await controller.AddTask(dto).ConfigureAwait(false);
            Assert.That(result.StatusCode, Is.EqualTo(201));



        }
        //public async Task Return200StatusCode()
        //{

        //    var repository = new Mock<IRepository<ToDoList>>();
        //    var taskRepository = new Mock<ISpecificationRepository<GetTasksByID>>();
        //    var tasksReportRepository = new Mock<ISpecificationRepository<TasksReport>>();
        //    repository.Setup(m => m.GetByIdAsync(1)).Returns(repository<ToDoList>.Object);
        //    var a = new ToDoList());
        //    var repoobj = repository.Object;
        //    var taskRepositoryObj = taskRepository.Object;
        //    var taskReportObj = tasksReportRepository.Object;
        //    var controller = new EmployeeController(repoobj, taskRepositoryObj, taskReportObj);
        //    var result = (StatusCodeResult)await controller.AddTask(dto).ConfigureAwait(false);
        //    Assert.That(result.StatusCode, Is.EqualTo(201));



        //}


    }
}
