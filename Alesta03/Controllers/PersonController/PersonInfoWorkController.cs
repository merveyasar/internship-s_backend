using Alesta03.Request.AddRequest;
using Alesta03.Response.AddResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alesta03.Controllers.PersonController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonInfoWorkController : ControllerBase
    {

        private readonly DataContext _context;
        public PersonInfoWorkController(DataContext context)
        {
            _context = context;
        }


        [HttpPost("{companyId}"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<BackWork>> AddWorkInfo(AddInfoWorkRequest request, int companyId)
        {
            var mail = User?.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == mail);
            var id = user?.ID;


            if (user == null)
                return NotFound("Kişi Bulunumadı!");

            var company = _context.Companies.FirstOrDefault(x => x.ID == companyId);
            var userId = company?.UsersId;
            var cuser = _context.Users.FirstOrDefault(x => x.ID == userId);
            var email = cuser?.Email;

            BackWork model = new BackWork();

            model.DepartmentName = request.DepartmentName;
            model.EmployeeID = request.EmployeeID;
            model.AppLetter = request.AppLetter;
            model.Start = request.Start;
            model.End = request.End;
            
            _context.BackWorks.Add(model);
            await _context.SaveChangesAsync();

            AddInfoWorkResponse response = new AddInfoWorkResponse();

            response.DepartmentName = model.DepartmentName;
            response.EmployeeID = model.EmployeeID;
            response.AppLetter = model.AppLetter;
            response.Start = model.Start;
            response.End = model.End;
            response.CompanyEmail = email;

            WorkStatus workStatus = new WorkStatus();

            var person = _context.People.FirstOrDefault(u => u.UsersId == id);
            var pid = person?.ID;

            workStatus.BackWorkId = model.ID;
            workStatus.PersonId = pid;

            _context.WorkStatuses.Add(workStatus);
            await _context.SaveChangesAsync();

            ApprovalStatus approval = new ApprovalStatus();

            var workSta = _context.WorkStatuses.FirstOrDefault(x => x.PersonId == pid);
            var pwid = workSta?.BackWorkId;
            var backWork = _context.BackWorks.FirstOrDefault(x => x.ID == pwid);
            var bpwid = backWork?.ID;

            approval.Status = string.Empty;
            approval.BackWorkId = bpwid;
            approval.CompanyId = companyId;
            approval.PersonId = pid;

            _context.ApprovalStatuses.Add(approval);
            await _context.SaveChangesAsync();
            
            return Ok(response);
        }

        [HttpGet("get all"), Authorize(Roles = Role.User)]
        public async Task<ActionResult<List<WorkStatus>>> GetAll()
        {
            //var mail = User?.Identity?.Name;
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == mail);
            //var id = user?.ID;

            //var person = await _context.People.FirstOrDefaultAsync(x => x.UsersId == id);
            //var pid = person?.ID;

            //var status = await _context.WorkStatuses
            //    .Where(x => x.PersonId == pid)
            //    .ToListAsync();

            var backEdu = await _context.BackWorks
                .ToListAsync();

            return Ok(backEdu);
        }
    }
}
