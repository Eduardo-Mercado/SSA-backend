using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSA.ApplicationService.Coworkers;
using SSA.ApplicationService.DTO;

namespace SSA_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoworkerController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {

            var result = new CoworkerService().GetAll();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            CoworkerDTO temp = new CoworkerDTO();
            if (new CoworkerService().GetById(id, ref temp))
                return Ok(temp);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Add([FromBody]CoworkerDTO record)
        {
            if (new CoworkerService().Add(ref record))
                return Ok(record);

            return BadRequest("there was some error :(");
        }

        [HttpPut("{id:int}")]
        public IActionResult update(int id, [FromBody] CoworkerDTO record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }

            if (new CoworkerService().Update(record))
                return Ok(record);

            return BadRequest("there was some error :(");
        }

        [HttpDelete]
        public IActionResult delete(int id)
        {
            if (new CoworkerService().Delete(id))
                return Ok();

            return BadRequest("there was some error :(");
        }
    }
}