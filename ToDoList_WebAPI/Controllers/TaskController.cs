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
    [Route("api/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        // Get all tasks
        // GET: api/<TaskController>
        [HttpGet]
        [Route("tasks")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> Get()
        {
            var tasks = await _service.GetAllAsync();

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
        // GET api/<TaskController>/{id}
        [HttpGet("task/{id}")]
        public async Task<ActionResult<TaskModel>> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
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
        // POST api/TaskPhoto
        [HttpPost("task")]
        public async Task<ActionResult> Post([FromBody] TaskModel task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(task);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(task);

        }


        //Update task status by id
        // PUT api/<TaskController>/{id}/status/{statusId}
        [HttpPut("task/{id}/status/{statusId}")]
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
        // PUT api/<TaskController>/{id}/category/{statusId}
        [HttpPut("task/{id}/category/{categoryId}")]
        public async Task<ActionResult> UpdateTaskCategoryById(int id, int categoryId)
        {
            try
            {
                if (!(await _service.UpdateTaskCategoryByIdAsync(id, categoryId)))
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
        // DELETE api/<TaskController>/{id}
        [HttpDelete("task/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            try
            {
                await _service.DeleteAsync(id);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(task);
        }
    }
}
