using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Alesta03.Controllers.CompanyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListAppController : ControllerBase
    {
        private readonly DataContext _context;

        public ListAppController(DataContext context)
        {
            _context = context;
        }

        [HttpGet(), Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<List<BackWork>>> GetAllApp()
        {
            var mail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == mail);
            var id = user?.ID;

            var company = _context.Companies.FirstOrDefault(u => u.UsersId == id);
            var cid = company?.ID;
            var cname = company?.Name;           

            var appStatus = await _context.BackWorks.ToListAsync();
            


            return Ok(appStatus);
        }


        [HttpPut("deneyim onaylama,{deneyimId}"), Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ApprovalStatus>> PozitiveApp(int reviewId)
        {
            var expReview = _context.ExpReviews.FirstOrDefault(x => x.ID == reviewId);
            var pid = expReview?.PersonId;

            var appStatus = _context.ApprovalStatuses.FirstOrDefault(x => x.PersonId==pid);
            
            if (appStatus is null) return BadRequest("Deneyim bulunamadı");

            if (appStatus.Status == "b")
            {
                appStatus.Status = "o";
                
            }

            await _context.SaveChangesAsync();

            return Ok("Deneyimi Onyaladınız");
        }

        [HttpPut("deneyim reddetme,{deneyimId}"), Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ApprovalStatus>> NegativeApp(int reviewId)
        {
            var expReview = _context.ExpReviews.FirstOrDefault(x => x.ID == reviewId);
            var pid = expReview?.PersonId;

            var appStatus = _context.ApprovalStatuses.FirstOrDefault(x => x.PersonId == pid);
            var status = appStatus?.Status;

            if (appStatus is null) return BadRequest("Deneyim bulunamadı");

            if (appStatus.Status == "b")
            {
                appStatus.Status = "r";

            }
            await _context.SaveChangesAsync();

            return Ok("Deneyimi Reddeddiniz");
        }

    }
}
