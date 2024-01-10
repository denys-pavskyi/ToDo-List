using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using BLL.Validation;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ToDoList_WebAPI.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        // Get all tasks
        // GET: /getAllTasks
        [HttpGet("getAllTasks")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks()
        {
            var tasks = await _service.GetAllTasksAsync();

            if (tasks == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(tasks);
            }

        }

        //Get task by id
        // GET /getTaskById/{id}
        [HttpGet("getTaskById/{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskById(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(task);
            }
        }


        //Create new task
        // POST /createNewTask
        [HttpPost("createNewTask")]
        public async Task<ActionResult> CreateNewTask([FromBody] TaskModel task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddTaskAsync(task);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(task);

        }


        //Update task status by id
        // PUT /updateTaskStatusById/task/{id}/status/{statusId}
        [HttpPut("updateTaskStatusById/task/{id}/status/{statusId}")]
        public async Task<ActionResult> UpdateTaskStatusById(int id, int statusId)
        {
            try
            {
                if(!(await _service.UpdateTaskStatusByIdAsync(id, statusId))){
                    return NotFound("Task not found");
                }

            }
            catch (ToDoListException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }


        //Update task category by id
        // PUT /updateTaskCategoryById/task/{id}/category/{statusId}
        [HttpPut("updateTaskCategoryById/task/{id}/category/{categoryId}")]
        public async Task<ActionResult> UpdateTaskCategoryById(int id, int categoryId)
        {
            try
            {
                if (!(await _service.UpdateTaskCategoryInTaskByIdAsync(id, categoryId)))
                {
                    return NotFound("Task not found");
                }

            }
            catch (ToDoListException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        //Remove task by id
        // DELETE /removeTaskById/{id}
        [HttpDelete("removeTaskById/{id}")]
        public async Task<ActionResult> RemoveTaskById(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            try
            {
                await _service.DeleteTaskAsync(id);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(task);
        }
    }
}
