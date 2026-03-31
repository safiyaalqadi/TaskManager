using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Interfaces
{
    public interface IDBContext
    {
        DbSet<TaskItem> Tasks { get; set; }
        DbSet<UserItem> Users { get; set; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
