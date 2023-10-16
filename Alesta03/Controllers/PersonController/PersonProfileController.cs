using Alesta03.Model;
using Alesta03.Request.AddRequest;
using Alesta03.Request.UpdateRequest;
using Alesta03.Response.AddResponse;
using Alesta03.Response.GetResponse;
using Alesta03.Response.UpdateResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alesta03.Controllers.PersonController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonProfileController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonProfileController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet,Authorize(Roles = Role.User)]
        public async Task<ActionResult<List<Person>>> GetSinglePerson()
        {
            var mail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == mail);
            var id = user?.ID;
            if (user == null)
                return NotFound("Kişi Bulunumadı!");

            var model = _context.People.FirstOrDefault(x => x.UsersId == id);
            if (model == null) return NotFound("Kişi Bilgisi Bulunamadı!");

            GetPersonProfileResponse response = new GetPersonProfileResponse();

            response.Name = model.Name;
            response.Surname = model.Surname;
            response.Birthday = model.Birthday;
            response.Phone = model.Phone;
            response.Location = model.Location;


            return Ok(response);
        }

        [HttpPut,Authorize(Roles = Role.User)]
        public async Task<ActionResult<Person>> UpdateProfile(UpdatePProfileRequest request)
        {
            var userMail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email == userMail);
            var id = user?.ID;

            if (user == null)
                return NotFound("Kişi Bulunumadı!");

            var model = _context.People.FirstOrDefault(x => x.UsersId == id);
            if (model == null) return NotFound("Kişi Bilgisi Bulunamadı!");

            model.Name = request.Name;
            model.Surname= request.Surname;
            model.Birthday = request.Birthday;
            model.Phone = request.Phone;
            model.Location = request.Location;

            UpdatePProfileResponse response = new UpdatePProfileResponse();

            response.Name = model.Name;
            response.Surname = model.Surname;
            response.Birthday = model.Birthday;
            response.Phone = model.Phone;
            response.Location = model.Location;

            await _context.SaveChangesAsync();

            return Ok(response);
        }

        [HttpPost, Authorize(Roles = Role.User)]

        public async Task<ActionResult<Person>> AddProfileInfo(AddPProfileRequest request)
        {
            var userMail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == userMail);
            var id = user?.ID;

            if (user == null)
                return NotFound("Kişi Bulunumadı!");


            Person model = new Person();
                                   
            model.UsersId = id;
            model.Name = request.Name;
            model.Surname = request.Surname;
            model.Birthday = request.Birthday;
            model.Phone = request.Phone;
            model.Location = request.Location;

            _context.People.Add(model);
            await _context.SaveChangesAsync();

            AddPProfileResponse response = new AddPProfileResponse();

            response.Name = model.Name;
            response.Surname = model.Surname;
            response.Birthday = model.Birthday;
            response.Phone = model.Phone;
            response.Location = model.Location;
            response.UsersId = model.UsersId;

            return Ok(response);
        }
    }
}
