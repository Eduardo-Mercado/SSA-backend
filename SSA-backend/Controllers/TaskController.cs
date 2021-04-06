using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSA.ApplicationService.DTO.Task;
using SSA.ApplicationService.Tasks;
using SSA.Infrastructure.Repository;
using SSA.Security.Security.Autentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace SSA_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private int IdUserAuthenticated;
        private readonly AuthenticatonAction _autenticatonAction;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TaskController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this._autenticatonAction = new AuthenticatonAction();
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            IdUserAuthenticated = new AutenticationService(this._autenticatonAction).GetInfoUserByName(userName).Id;
        }
        /// <summary>
        /// Get all Task relative to a id of Activity
        /// </summary>
        /// <param name="id">id Activity</param>
        /// <returns>TaskSummaryService</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetSummaryInfoTasks(int id)
        {
            TaskSummaryService servicio = new TaskSummaryService();
            var information = servicio.GetSummaryTasks(id);
            return Ok(information);
        }


        [Route("getTaskInfo/idActivity={idActivity}&idTask={idTask}")]
        public IActionResult getInfoTask(int idActivity, int idTask)
        {
            var activity = new ActivityRepository().GetActivity(idActivity);
            var funcionalidad = new TaskAction(activity);
            var result = new TaskService(funcionalidad).GetInfoTask(idTask);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] TaskDTO record)
        {
            var activity = new ActivityRepository().GetActivity(record.IdActivity);
            activity = new ActivityRepository().GetAllTasks(activity);
            var funcionalidad = new TaskAction(activity);
            if (new TaskService(funcionalidad).UpdateTask(record, isInsert: true, IdUserAuthenticated))
            {
                return Ok();
            }
            return BadRequest("there was some internal error :(");

        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromBody] TaskDTO record)
        {

            var activity = new ActivityRepository().GetActivity(record.IdActivity);
            activity = new ActivityRepository().GetAllTasks(activity);
            var funcionalidad = new TaskAction(activity);
            if (new TaskService(funcionalidad).UpdateTask(record, isInsert: false, IdUserAuthenticated))
            {
                return Ok();
            }
            return BadRequest("there was some internal error :(");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id, [FromBody] int idActivity)
        {
            var activity = new ActivityRepository().GetActivity(idActivity);
            activity = new ActivityRepository().GetAllTasks(activity);
            var funcionalidad = new TaskAction(activity);
            if (new TaskService(funcionalidad).RemoveTask(id, IdUserAuthenticated))
            {
                return Ok();
            }
            return BadRequest("There was some internal error :(");
        }

        [HttpPut]
        [Route("authorization/{idActivity}")]
        public IActionResult Authorization(int idActivity, [FromBody] List<TaskDTO> tasks)
        {
            var activity = new ActivityRepository().GetActivity(idActivity);
            activity = new ActivityRepository().GetAllTasks(activity);
            var funcionalidad = new TaskAction(activity);
            if (new TaskService(funcionalidad).AuthorizeTasks(tasks, IdUserAuthenticated))
            {
                return Ok();
            }
            return BadRequest("there was some internal error :(");
        }
    }
}