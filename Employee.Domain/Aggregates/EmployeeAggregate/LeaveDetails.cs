using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmpManagement.Domain.Models
{
    public partial class LeaveDetails
    {
        [Key]
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
  
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NumberOfDays { get; set; }
        public string Reason { get; set; }
        public string ApproveStatus { get; set; }
    }
}
