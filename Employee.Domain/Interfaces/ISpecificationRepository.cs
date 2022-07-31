using Employee.API.DTO;
using Employee.Domain.Aggregates.EmployeeAggregate;
using EmpManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Domain.Interfaces
{
    public interface ISpecificationRepository<T> where T : class 
    {

        IEnumerable<GetTasksByID> GetTasksById(int id);

        IEnumerable<TasksReport> GenerateReport(int id);

        Task<LeaveDetails> ApplyLeaveAsync(LeaveDetails leaveRequest);
        //Task<List<LeaveDetails>> GetLeavesAsync();

        Task<IEnumerable<LeaveDetails>> GetLeaveDetails();

        Task<IEnumerable<RaiseTicket>> GetTicketsAsync();

        Task<RaiseTicket> RaiseTicketAsync(RaiseTicket _ticket);

        Task<LeaveDetails> DeleteLeaveRequestAsync(int LeaveId);
    }
}
