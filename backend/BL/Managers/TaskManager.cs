using BL.DB;
using BL.Interfaces;
using BL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly Func<MvcTaskManagerContext> _contextFactory;

        public TaskManager(Func<MvcTaskManagerContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<MyTask> AddTask(MyTask task)
        {
            using (var context = _contextFactory())
            {
                context.Tasks.Add(task);
                await context.SaveChangesAsync();
                return task;
            }
        }

        public async Task<bool> DeleteTask(int id)
        {
            using (var context = _contextFactory())
            {
                var deleteTask = await context.Tasks.FindAsync(id);
                if (deleteTask == null)
                {
                    return false;
                }

                context.Tasks.Remove(deleteTask);
                await context.SaveChangesAsync();
                return true;
            }

        }

        public async Task<IEnumerable<MyTask>> GetTasks()
        {
            using (var context = _contextFactory())
            {
                return await context.Tasks.ToListAsync();
            }
        }
    }
}
