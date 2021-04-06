using Microsoft.AspNetCore.Mvc;
using SSA.ApplicationService.DTO.Project;
using SSA.ApplicationService.Projects;

namespace SSA_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ProjectAction funcionalidad = new ProjectAction();
            var result = new ProjectService(funcionalidad).GetAllProjects();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Add([FromBody]ProjectDTO record)
        {
            ProjectAction funcionalidad = new ProjectAction();
            if (new ProjectService(funcionalidad).CreateProject(ref record))
            {
                ProjectListDTO temp = new ProjectListDTO();
                temp = new ProjectService(funcionalidad).GetProject(record.Id);
                return Ok(temp);
            }

            return BadRequest("there was some internal error :(");
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody]ProjectDTO record)
        {
            //code here
            return Ok();
        }

        [HttpDelete]
        public IActionResult delete(int id)
        {
            if (true)
            {
                return Ok();
            }

            return BadRequest("there was some internal error :(");
        }
    }
}