using Alesta03.Model;
using Alesta03.Request.AddRequest;
using Alesta03.Request.UpdateRequest;
using Alesta03.Response.AddResponse;
using Alesta03.Response.GetResponse;
using Alesta03.Response.UpdateResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Alesta03.Controllers.CompanyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;
        
        public CompanyController(DataContext context)
        {
            _context = context;
        }

        
        [HttpGet, Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<Company>> GetSingleProfiles()
        {
            var mail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == mail);
            var id = user?.ID;             
            if (user == null)
                return NotFound("Firma Bulunumadı!");

            var model =_context.Companies.FirstOrDefault(x => x.UsersId == id);
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

        [HttpPut, Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<Company>> UpdateProfile(UpdateCProfileRequest request)
        {
            var userMail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email == userMail);
            var id = user?.ID;

            if (user == null)
                return NotFound("Firma Bulunumadı!");

            var model = _context.Companies.FirstOrDefault(x => x.UsersId == id);
            if (model == null) return NotFound("Firma Bilgisi Bulunamadı!");

            model.Category = request.Category;
            model.Type = request.Type;
            model.Name = request.Name;
            model.Description = request.Description;
            model.FDate = request.FDate;
            model.TotalStaff = request.TotalStaff;
            model.Location = request.Location;
            model.Prof = request.Prof;
            model.Phone = request.Phone;
            model.Website = request.Website;

            UpdateCProfileResponse response = new UpdateCProfileResponse();

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


            await _context.SaveChangesAsync();

            return Ok(response);
        }

        [HttpPost, Authorize(Roles = Role.Admin)]

        public async Task<ActionResult<Company>> AddProfileInfo(AddCProfileRequest request)
        {
            var userMail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == userMail);
            var id = user?.ID;

            if (user == null)
                return NotFound("Firma Bulunumadı!");

            Company model = new Company();
           
            model.UsersId = id;
            model.Category = request.Category;
            model.Type = request.Type;
            model.Name = request.Name;
            model.Description = request.Description;
            model.FDate = request.FDate;
            model.TotalStaff = request.TotalStaff;
            model.Location = request.Location;
            model.Prof = request.Prof;
            model.Phone = request.Phone;
            model.Website = request.Website;

            _context.Companies.Add(model);
            await _context.SaveChangesAsync();

            AddCProfileResponse response = new AddCProfileResponse();
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
            response.UsersId = model.UsersId;

            return Ok(response);

        }

    }
}
