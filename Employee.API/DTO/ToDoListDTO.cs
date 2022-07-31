using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.API.DTO
{
    public class ToDoListDTO
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public int ManagerID { get; set; }
        [Required]
        public string Pending_task { get; set; }
        [Required]
        public DateTime Due_Date { get; set; }
        [Required]
        public string Work_assigned_by { get; set; }
        [Required]
        public string status { get; set; }

    }
}
