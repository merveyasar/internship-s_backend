using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alesta03.Controllers.PersonController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonApprovalController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonApprovalController(DataContext context)
        {
            _context = context;
        }

        [HttpPut("{backworkId}"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<ApprovalStatus>> AppWork(int backworkId)
        {
            var model = _context.ApprovalStatuses.FirstOrDefault(x => x.BackWorkId == backworkId);

            if (model == null) return NotFound("Kişi Onay Durumu Bulunamadı!");

            if (model.Status is "b") return BadRequest("Başvurunuz Tarafımıza Daha Öncesinde İletilmiştir.");
            else if (model.Status is "r") return BadRequest("Başvurunuz Reddeilmiştir.");
            else if (model.Status is "o") return BadRequest("Başvurunuz Onaylanmıştır.");
            else if (model.Status is "")
            {
                model.Status = "b";
            }
            

            await _context.SaveChangesAsync();

            return Ok("İş Deneyiminiz Onaya Gönderildi");
        }

        [HttpGet("{backworkId}"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<string>> AppWorkControl(int backworkId)
        {
            var model = _context.ApprovalStatuses.FirstOrDefault(x => x.BackWorkId == backworkId);

            if (model == null) return NotFound("Kişi Onay Durumu Bulunamadı!");

            return Ok(model.Status);
        }

    }
}
