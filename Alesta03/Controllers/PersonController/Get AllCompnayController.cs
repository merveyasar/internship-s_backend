using Alesta03.Response.GetResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alesta03.Controllers.PersonController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Get_AllCompnayController : ControllerBase
    {
        private readonly DataContext _context;

        public Get_AllCompnayController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get all company"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<List<Company>>> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();

            return Ok(companies);
        }

        [HttpGet("{companyId}"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<Company>> GetSingleProfiles(int companyId)
        {
            var mail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == mail);
            var id = user?.ID;
            if (user == null)
                return NotFound("Firma Bulunumadı!");

            var model = _context.Companies.FirstOrDefault(x => x.UsersId == companyId);
            if (model == null) return NotFound("Firma Bilgisi Bulunamadı!");

            GetCompanyProfileResponse response = new GetCompanyProfileResponse();

            response.Category = model.Category;
            response.Type = model.Type;
            response.Name = model.Name;
            response.Description = model.Description;
            response.FDate = model.FDate;
            response.TotalStaff = model.TotalStaff;
            response.Location = model.Location;
            response.Prof = model.Prof;
            response.Phone = model.Phone;
            response.Website = model.Website;


            return Ok(response);
        }
    }
}
