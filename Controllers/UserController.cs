using ChatApplication.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult CreateUser()
        {
            if (HttpContext.Request.Query.ContainsKey("name"))
            {
                string name = HttpContext.Request.Query["name"];

                userService.Сreate(name);
                return Ok();
            }
            else return BadRequest();
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            var result = userService.Read();
            if(result != null)
            return Ok(result);
            else return BadRequest();
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var result = userService.ReadById(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            userService.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser()
        {
            if (HttpContext.Request.Query.ContainsKey("id") && HttpContext.Request.Query.ContainsKey("name"))
            {
                int id = int.Parse(HttpContext.Request.Query["id"].ToString());
                string name = HttpContext.Request.Query["name"];

                userService.Update(id, name);
                return Ok();
            }
            else return BadRequest();

        }
    }
}
