using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSA.ApplicationService.shared;

namespace SSA_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        [Route("getCatalogwithColor")]
        public IActionResult GetCatalogwithColor()
        {
            var result = new DropDownsService().GetCatalogStatusAndCategories();
            return Ok(result);
        }

        [Route("getCoworker")]
        public IActionResult GetCoworker()
        {
            var result = new DropDownsService().GetListCoworker();
            return Ok(result);
        }

        [Route("getTeamProject/{id}")]
        public IActionResult GetTeamProject(int id)
        {
            var result = new DropDownsService().GetTeampProject(id);
            return Ok(result);
        }

        [Route("getCategoryActivities")]
        public IActionResult GetCategoryActivities()
        {
            var result = new DropDownsService().GetCategoryActivities();
            return Ok(result);
        }

        [Route("getCategoryTask")]
        public IActionResult GetCategoryTask()
        {
            var result = new DropDownsService().GetCategoryTasks();
            return Ok(result);
        }

        [Route("getRolCoworkerProject")]
        public IActionResult GetRolCoworkerProject()
        {
            var result = new DropDownsService().GetListRolCoworkerProject();
            return Ok(result);
        }

        [Route("getRolsApp")]
        public IActionResult GetRolsApp()
        {
            var result = new DropDownsService().GetListRolApp();
            return Ok(result);
        }
    }
}