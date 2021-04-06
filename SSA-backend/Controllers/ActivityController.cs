using Microsoft.AspNetCore.Mvc;
using SSA.ApplicationService.Activities;
using SSA.ApplicationService.DTO.Activity;

namespace SSA_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityAction funcionalidad;

        public ActivityController()
        {
            funcionalidad = new ActivityAction();
        }

        [Route("getProjectInfo/{id}")]
        public IActionResult GetAll(int id)
        {
            ActivitySummaryService servicio = new ActivitySummaryService();
            var information = servicio.GetProjectActivities(id);
            return Ok(information);
        }

        [Route("GetActiviyInfo")]
        [HttpGet("{id:int}")]
        public IActionResult GetActiviyInfo(int id)
        {
            ActivitySummaryService servicio = new ActivitySummaryService();
            var information = servicio.GetActivityInfo(id);
            return Ok(information);
        }

        [HttpPost]
        public IActionResult Add([FromBody]ActivityDTO record)
        {
            int newId = 0;
            if (new ActivityService(funcionalidad).Create(record, ref newId))
            {
                record.Id = newId;
                return Ok(record);
            }
            return BadRequest("there was some internal error :(");
        }
    }
}