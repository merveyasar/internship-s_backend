using Alesta03.Request.DtoRequest;
using Alesta03.Request.RgeisterRequest;
using Alesta03.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alesta03.Controllers.GeneralController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        public static Company company = new Company();
        public static Person person = new Person();
        public static RegisterRequestUC RegisterRequest { get; set; }

        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AuthController(IConfiguration configuration,DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("registerCompany")]
        public async Task<ActionResult<User>> RegisterCompany(RegisterRequestUC request)
        {
            if (_context.Users.Any(u => u.Email == request.UserDto.Email))
            {
                return BadRequest("E-Posta daha önce kaydedilmiş!");
            }

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);

            Company company = new Company();
            User user = new User();

            company.Name = request.CompanyDto.Name;

            user.Email = request.UserDto.Email;
            user.UserType = "Company";
            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        [HttpPost("registerPerson")]
        public async Task<ActionResult<User>> RegisterPerson(RegisterRequestUP request)
        {
            if (_context.Users.Any(u => u.Email == request.UserDto.Email))
            {
                return BadRequest("E-Posta daha önce kaydedilmiş!");
            }

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);

            Person person = new Person();
            User user = new User();


            person.Name = request.PersonDto.Name;
            person.Surname = request.PersonDto.Surname;

            user.Email = request.UserDto.Email;
            user.UserType = "Person";
            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;

        }

        [HttpPost("loginPerson")]
        public ActionResult<string> LoginPerson(UserDto request)
        {
            var user = _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user.Result is null) 
                return BadRequest("Kullanıcı  Bulunamadı!"); 
            
            if (user is null)
                return BadRequest("Kullanıcı  Bulunamadı!");

            else if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Result.PasswordHash))
                return BadRequest("Yanlış Şifre!");
            
            else if (user.Result.UserType != user.Result.UserType)
                return BadRequest("Kullanıcı Türünüz Yanlış!");


            string token = CreateToken(user.Result);
            bool login = user.Result.IsFirstLogin;

            LoginResult loginResult = new LoginResult
            {
                token = token,
                control = user.Result.IsFirstLogin
            };

            string json = JsonConvert.SerializeObject(loginResult);
            return json;
        }

        [HttpPost("loginCompany")]
        public ActionResult<string> LoginCompany(UserDto request)
        {
            var user = _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user.Result is null)
                return BadRequest("Kullanıcı  Bulunamadı!");

            if (user is null)
                return BadRequest("Kullanıcı  Bulunamadı!");

            else if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Result.PasswordHash))
                return BadRequest("Yanlış Şifre!");

            else if (user.Result.UserType != user.Result.UserType)
                return BadRequest("Kullanıcı Türünüz Yanlış!");


            string token = CreateToken(user.Result);
            bool login = user.Result.IsFirstLogin;

            LoginResult loginResult = new LoginResult
            {
                token = token,
                control = user.Result.IsFirstLogin
            };

            string json = JsonConvert.SerializeObject(loginResult);
            return json;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role,user.UserType)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            
            return jwt;
        }
    }
}
