using Employee.API.DTO;
using Employee.Domain.Aggregates.EmployeeAggregate;
using Employee.Domain.Interfaces;
using EmpManagement.Domain.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<ToDoList> repository;
      
        private readonly ISpecificationRepository<GetTasksByID> taskRepository;
        private readonly ISpecificationRepository<TasksReport> tasksReportRepository;
        private readonly HttpContext httpcontext;
        private readonly HttpClient Client;
        private readonly ISpecificationRepository<LeaveDetails> repo;
        private readonly ISpecificationRepository<RaiseTicket> ticketrepo;
        public EmployeeController(IRepository<ToDoList> repository,
            ISpecificationRepository<GetTasksByID> taskRepository, ISpecificationRepository<TasksReport> tasksReportRepository, ISpecificationRepository<LeaveDetails> repo, ISpecificationRepository<RaiseTicket> ticketrepo)
        {
          
            this.repository = repository;
         
            this.taskRepository = taskRepository;
            this.tasksReportRepository = tasksReportRepository;
            this.repo = repo;
            this.ticketrepo = ticketrepo;
            Client = new HttpClient();
            
        }
        
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Employee,Manager")]

        public async Task<IActionResult> Update(string id,UpdateDTO dto) 
        {

            var updatedEmployeeDetails = new Update(dto.Address, dto.PhoneNumber);
            var jsonData = JsonConvert.SerializeObject(updatedEmployeeDetails);
            StringValues value;
            var encodedData = new StringContent(jsonData, Encoding.UTF8, "application/json");
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out value))
            {
                var jwt = value.ToString().Split(' ')[1];
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                var response = Client.PutAsync("http://localhost:9279/api/Employee/" + id, encodedData);
                var responseData = response.Result.Content.ReadAsStringAsync();
                var b = JsonConvert.DeserializeObject(responseData.Result);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Server Down");
                }
            }
            else
                return Unauthorized();
           
        }
        [Authorize(Roles ="Employee,Manager")]
        [HttpPost]
        [ProducesResponseType(201)]
        [Route("/toDo")]
        
        public async Task<IActionResult> AddTask(ToDoListDTO dto)  
        {
            var todo = new ToDoList(dto.EmployeeID, dto.ManagerID, dto.Pending_task, dto.Due_Date, dto.Work_assigned_by, dto.status);
            repository.Add(todo);
            await repository.SaveAsync();
            return StatusCode(201);
        }

        [Authorize(Roles="Employee,Manager")]
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("/TaskById")]
        public IActionResult GetAllTaskById(int id) 
        {
            var tasks = taskRepository.GetTasksById(id);
            return Ok(tasks);
        }
        [Authorize(Roles="Employee,Manager")]
        [HttpPut]
        [ProducesResponseType(200)]
        [Route("/UpdateTask")]
        public async Task<IActionResult> UpdateTask(int taskid) 
        { 
                var task = repository.GetByIdAsync(taskid);
                task.Result.status = "Completed";
                await repository.SaveAsync();
                return Ok();

        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("/GetEmployeeByDepartment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetEmployeeByDepartment(int id)
        {
            List<ResponseEmployeeDTO> Employees = new List<ResponseEmployeeDTO>();
            string apiurl = "http://localhost:9279/";
            StringValues value;
            using (var client = new HttpClient())
            {
                if (HttpContext.Request.Headers.TryGetValue("Authorization", out value))
                {
                    var jwt = value.ToString().Split(' ')[1];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                    client.BaseAddress = new Uri(apiurl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/Employee?id=" + id);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Employees = JsonConvert.DeserializeObject<List<ResponseEmployeeDTO>>(json);



                    }
                    return Ok(Employees);

                }
                else
                {
                    return Unauthorized();
                }

                return Ok(Employees);
            }
        }
        [Authorize(Roles="Manager")]
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("/GenerateReport")]
        public async Task<IActionResult> GenerateReport(int id)
        {
            var query = tasksReportRepository.GenerateReport(id);
            return Ok(query);

        }
        
        [HttpGet]
        [Route("/pendingTaskNotification")]
        [Authorize(Roles="Employee,Manager")]
        public IActionResult GetNotification(int id)
        {
            var tasks = taskRepository.GetTasksById(id);
            List<string> tasksNotification = new List<string>();

            foreach (var item in tasks)
            {
                if ((DateTime.Now - item.Due_Date).TotalDays < 3)
                {
                    tasksNotification.Add("Upcoming task to be completed in less than 3 days: "+item.Pending_task);
                }
                
            }
            return Ok(tasksNotification);

        }


        [HttpGet("GetLeaveDetails")]
        [ProducesResponseType(200)]
        //[Route("[controller]")]
        public async Task<IActionResult> GetLeaveDetails(/*int ManagerId*/)
        {
            var leavedetails = await repo.GetLeaveDetails(/*ManagerId*/);

            return Ok(leavedetails);
        }
        [HttpDelete("DeleteLeaveRequest/{LeaveId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteLeaveRequest(int LeaveId)
        {
            if(LeaveId != null)
            {
                var leave = await repo.DeleteLeaveRequestAsync(LeaveId);
                return Ok(leave);
            }
            return NotFound();
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [Route("/SaveLeaveDetails")]
        public async Task<IActionResult> SaveLeaveDetails(LeaveDetails leaves)
        {
            var leave = await repo.ApplyLeaveAsync(leaves);

            return Ok(leave);
        }
        [HttpGet("GetTicketDetails")]
        [ProducesResponseType(200)]
        //[Route("[controller]")]
        public async Task<ActionResult> GetTicketsAsync()
        {

            var ticketList = await ticketrepo.GetTicketsAsync();

            return Ok(ticketList);

        }
        [HttpPost]
        [ProducesResponseType(200)]
        //[Route("[controller]/RaiseTicket")]
        public async Task<IActionResult> RaiseTicketAsync(RaiseTicket _ticket)
        {
            var ticket = await ticketrepo.RaiseTicketAsync(_ticket);

            return Ok(ticket);
        }


    }
}
