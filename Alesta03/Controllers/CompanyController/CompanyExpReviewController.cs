using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alesta03.Controllers.CompanyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyExpReviewController : ControllerBase
    {

        private readonly DataContext _context;
        public CompanyExpReviewController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Firma"), Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ExpReview>> GetSingleReviews()
        {
            var userMail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email == userMail);
            var id = user?.ID;

            var company = _context.Companies.FirstOrDefault(x => x.UsersId == id);
            var cid = company?.ID;

            var expReviews = await _context.ExpReviews
                 .Where(x => x.CompanyId == cid)
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
