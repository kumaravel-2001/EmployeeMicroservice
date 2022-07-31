using Employee.Domain.Aggregates.EmployeeAggregate;
using EmpManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Infrastructure.Data.contexts
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        {

        }
        
        public DbSet<ToDoList> ToDoLists { get; set; }
       
        public virtual DbSet<LeaveDetails> LeaveDetails { get; set; }
        
        public virtual DbSet<RaiseTicket> RaiseTicket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);


        }
    }
}
