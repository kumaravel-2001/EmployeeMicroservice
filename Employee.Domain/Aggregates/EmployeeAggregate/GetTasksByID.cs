using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.API.DTO
{
    public class GetTasksByID
    {
        public int Id { get; set; }

        public string Pending_task { get; set; }

        public DateTime Due_Date { get; set; }

        public string Work_assigned_by { get; set; }

        public string status { get; set; }


    }
}
