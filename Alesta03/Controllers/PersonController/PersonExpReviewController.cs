using Alesta03.Model;
using Alesta03.Request.AddRequest;
using Alesta03.Request.UpdateRequest;
using Alesta03.Response.AddResponse;
using Alesta03.Response.UpdateResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Xml.Linq;


namespace Alesta03.Controllers.PersonController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonController(DataContext context)
        {
            _context = context;
        }


        [HttpPost("CompanyId,BackWorkId"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<ExpReview>> AddReview(AddReviewRequest request,int companyId, int backWorkId)
        {
            var userMail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email == userMail);
            var id = user?.ID;

            var person = _context.People.FirstOrDefault(x => x.UsersId == id);
            var pid = person?.ID;
            var cid = companyId;
            var name = person?.Name;
            var surname = person?.Surname;


            var company = _context.Companies.FirstOrDefault(x => x.ID == companyId);
            var cname = company?.Name;

            var appStatus = await _context.ApprovalStatuses.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.BackWorkId == backWorkId);

            if (appStatus == null) return BadRequest("Deneyiminiz Yok Ya da Onaylı Değil");
            if(appStatus.Status is "r") return BadRequest("Deneyiminiz Reddedildi");
            if (appStatus.Status is "b") return BadRequest("Deneyiminiz Beklemede");


            ExpReview expReview = new ExpReview();

            expReview.CompanyId = companyId;
            expReview.PersonId = pid;
            expReview.Title = request.Title;
            expReview.Description = request.Description;
            expReview.Date = DateTime.Now;
            expReview.Name = name;
            expReview.Surname = surname;
            expReview.CompanyName = cname;
           
            _context.ExpReviews.Add(expReview);
            await _context.SaveChangesAsync();

            AddReviewResponse response = new AddReviewResponse();

            response.Title = expReview.Title;
            response.Description = expReview.Description;
            response.Date = DateTime.Now;
            response.Name = name;
            response.Surname = surname;
            response.CName = cname;

            return Ok(response);
        }

        [HttpGet("karışık"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<List<ExpReview>>> GetAllReviews()
        {
            var expReviws = await _context.ExpReviews.ToListAsync();

            return Ok(expReviws);
        }

        [HttpGet("Kişi"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<ExpReview>> GetSingleReviews()
        {
            var userMail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email == userMail);
            var id = user?.ID;

            var person = _context.People.FirstOrDefault(x => x.UsersId == id);
            var pid = person?.ID;

            var expReviews = await _context.ExpReviews
                 .Where(x => x.PersonId== pid)
                 .Select(x => new
                 {
                     title = x.Title,
                     description = x.Description,
                     Date = DateTime.Now,
                     name = x.Name,
                     surname = x.Surname,
                     cname = x.CompanyName

                 }).ToListAsync();

            return Ok(expReviews);

        }

    }
}
