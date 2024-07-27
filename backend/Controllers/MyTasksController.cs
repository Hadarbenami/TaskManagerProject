using BL.Interfaces;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcTaskManager.Models;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace MvcTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyTaskController : ControllerBase
    {
        private readonly ITaskManager _taskManager;

        public MyTaskController(ITaskManager TaskManager)
        {
            _taskManager = TaskManager;
        }

        [HttpGet]



        public async Task<ActionResult<IEnumerable<BL.Models.MyTask>>> GetTasks()
        {
            var tasks = await _taskManager.GetTasks();
            List<BL.Models.MyTask> response = new List<BL.Models.MyTask>(tasks.Count());
            foreach (var task in tasks)
            {
                BL.Models.MyTask item = new  BL.Models.MyTask(task.Id, task.Title, task.Description, task.Priority, task.CreatedAt);
                response.Add(item);

            }
            return response;
        }

        [HttpPost] 
       // [Route("/add-task")]
        public async Task<ActionResult<BL.Models.MyTask>> PostTask(BL.Models.MyTask myTask)
        {
            var createdTask = await _taskManager.AddTask(myTask);
            return CreatedAtAction(nameof(GetTasks), new { id = createdTask.Id }, createdTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
           var result =   await _taskManager.DeleteTask(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
