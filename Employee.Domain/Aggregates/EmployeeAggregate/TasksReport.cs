using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Domain.Aggregates.EmployeeAggregate
{
    public class TasksReport
    {
        public int Id { get; set; }

        public string Pending_task { get; set; }

        public DateTime Due_Date { get; set; }

        public string status { get; set; }

    }
}
