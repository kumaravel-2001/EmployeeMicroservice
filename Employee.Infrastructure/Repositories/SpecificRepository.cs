using Employee.API.DTO;
using Employee.Domain.Aggregates.EmployeeAggregate;
using Employee.Domain.Interfaces;
using Employee.Infrastructure.Data.contexts;
using EmpManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Repositories
{
    public class SpecificRepository<T> : ISpecificationRepository<T> where T : class
    {
        
        private readonly EmployeeContext context;
        public SpecificRepository(EmployeeContext context)
        {
          
            this.context = context;
        }

        public IEnumerable<GetTasksByID> GetTasksById(int id)
        {
            var query = context.ToDoLists.Where(s => (s.EmployeeID == id & s.status == "Not Completed")).ToList().Select(s => new GetTasksByID
            {
                Id = s.Id,
                Pending_task = s.Pending_task,
                Due_Date = s.Due_Date,
                Work_assigned_by = s.Work_assigned_by,
                status = s.status
            }).OrderBy(c => c.Due_Date);
            return query;
        }

        public IEnumerable<TasksReport> GenerateReport(int id)
        {
            var query = context.ToDoLists.Where(c => c.EmployeeID == id && c.Work_assigned_by == "Manager").Select(c => new TasksReport
            {
                Id=c.Id,
                Pending_task= c.Pending_task,
                Due_Date=c.Due_Date,
                status=  c.status
            }).OrderByDescending(c => c.Id).Take(10).ToArray();
            return query;
        }

        public async Task<LeaveDetails> ApplyLeaveAsync(LeaveDetails leaveRequest)
        {
            var leave = await context.LeaveDetails.AddAsync(leaveRequest);

            context.SaveChanges();
            return leave.Entity;
        }

        public async Task<IEnumerable<LeaveDetails>> GetLeaveDetails()
        {
            var leave = await context.LeaveDetails.ToListAsync();
            return leave;
        }

        public async Task<LeaveDetails> GetLeaveDetail(int EmployeeId)
        {
            var leave = await context.LeaveDetails.FirstOrDefaultAsync(x=>x.EmployeeId==EmployeeId);
            return leave;
        }
        public async Task<RaiseTicket> RaiseTicketAsync(RaiseTicket _ticket)
        {
            var ticket = await context.RaiseTicket.AddAsync(_ticket);
            await context.SaveChangesAsync();


            return ticket.Entity;


        }


        public async Task<IEnumerable<RaiseTicket>> GetTicketsAsync()
        {
            var leave = await context.RaiseTicket.ToListAsync();

            return leave;
            //throw new NotImplementedException();
        }

        public async Task<LeaveDetails> DeleteLeaveRequestAsync(int LeaveId)
        {
            var leaveRequest = await GetLeaveDetail(LeaveId);
            if(leaveRequest != null)
            {
                context.LeaveDetails.Remove(leaveRequest);
                await context.SaveChangesAsync();
                return leaveRequest;
            }
            return null;
        }

    }
}
