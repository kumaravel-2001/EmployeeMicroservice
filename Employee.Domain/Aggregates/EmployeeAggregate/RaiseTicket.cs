using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmpManagement.Domain.Models
{
    public partial class RaiseTicket
    {
        [Key]
        public int TicketId { get; set; }
        public int EmployeeId { get; set; }
        public string QueryRelatedTo { get; set; }
        public DateTime? QueryDate { get; set; }
        public string Responses { get; set; }
    }
}
