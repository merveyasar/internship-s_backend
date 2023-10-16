using Alesta03.Model;
using Alesta03.Request.DtoRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alesta03.Controllers.GeneralController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("logincontrol"),Authorize]
        public ActionResult<bool> LoginConrtol()
        {
            var mail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == mail);
            var id = user?.ID;

            var control = _context.Users.FirstOrDefault(x => x.ID == id);
            if (control is null) return null;
            bool cont = control.IsFirstLogin;
            
           
             control.IsFirstLogin = false;
            _context.SaveChanges();

            var result = control.IsFirstLogin;

            return Ok(result);
        }
    }
}
