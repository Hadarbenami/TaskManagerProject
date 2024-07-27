using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ITaskManager
    {
        public Task <IEnumerable<MyTask>> GetTasks();
        public Task<MyTask> AddTask(MyTask task);

       public Task<bool> DeleteTask(int id);
    }
}
