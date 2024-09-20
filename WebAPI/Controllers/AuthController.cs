using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		private IAuthService _authService;
        private IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService; // Kullanıcı servisi başlatıldı.
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
		public ActionResult Register(UserForRegisterDto userForRegisterDto)
		{
			var userExists = _authService.UserExists(userForRegisterDto.Email);
			if (!userExists.Success)
			{
				return BadRequest(userExists.Message);
			}

			var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
			var result = _authService.CreateAccessToken(registerResult.Data);
			if (result.Success)
			{
				return Ok(result);
			}

			return BadRequest(result.Message);
		}
        [HttpGet("emails")]
        public ActionResult<List<string>> GetEmails()
        {
            var users = _userService.GetAll(); // Kullanıcıları almak için bir yöntem eklenmeli.
            var emails = users.Select(u => u.Email).ToList(); // E-posta adreslerini seç.
            return Ok(emails); // E-posta adreslerini döndür.
        }
        [HttpGet("user")]
        public ActionResult<User> GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Kullanıcı ID'sini al
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); // ID yoksa yetkisiz yanıtı ver
            }

            var user = _userService.GetUserById(int.Parse(userId)); // ID ile kullanıcıyı al
            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamadı
            }

            return Ok(user); // Kullanıcı bilgilerini döndür
        }




        [HttpPut("user")]
        public ActionResult UpdateUserData(UserForUpdateDto userForUpdateDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value; // Kullanıcı e-postasını al
            var user = _userService.GetByMail(email); // Kullanıcı verisini al
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            _userService.UpdateUser(userForUpdateDto, user.Id); // Güncellemeyi yap
            return NoContent(); // Güncelleme başarılı
        }


    }

}

