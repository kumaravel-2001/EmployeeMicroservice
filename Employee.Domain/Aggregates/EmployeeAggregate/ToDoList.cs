using Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Domain.Aggregates.EmployeeAggregate
{
    public class ToDoList :EntityBase 
    {
     
        public int EmployeeID { get; set; }

        public int ManagerID { get; set; }
        public string Pending_task { get; set; }

        public DateTime Due_Date { get; set; }

        public string Work_assigned_by { get; set; }

        public  string status { get; set; }

        public ToDoList()
        {
        }
        public ToDoList(int EmployeeID,int ManagerID,string Pending_task,DateTime Due_Date,string Work_assigned_by,string status)
        {
            this.EmployeeID = EmployeeID;
            this.ManagerID = ManagerID;
            this.Pending_task = Pending_task;
            this.Due_Date = Due_Date;
            this.Work_assigned_by = Work_assigned_by;
            this.status = status;
        }



    }
}
