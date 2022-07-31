using Employee.Domain.Aggregates.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Infrastructure.Data.config
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.Property(t => t.EmployeeID).IsRequired(true);
            builder.Property(t => t.ManagerID).IsRequired(true);
            builder.Property(t => t.Pending_task).IsRequired(true);
            builder.Property(t => t.Work_assigned_by).IsRequired(true);
            builder.Property(t => t.Due_Date).IsRequired(true);
            builder.Property(t => t.status).IsRequired(true);
        }
    }
}
