
using AutoMapper;
using Food_Rescue.Models;
using FoodRescue.Core.Entities;
using FoodRescue.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Food_Rescue.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;
		private readonly IBusinessService _businessService;
		private readonly ICharityService _charityService;
		private readonly IMapper _mapper;

		// הזרקת כל התלויות שאנחנו צריכים
		public AuthController(IConfiguration configuration,IUserService userService,IBusinessService businessService,ICharityService charityService,IMapper mapper)
		{
			_configuration = configuration;
			_userService = userService;
			_businessService = businessService;
			_charityService = charityService;
			_mapper = mapper;
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] LoginModel login)
		{
			// 1. אימות המשתמש מול בסיס הנתונים
			var user = await _userService.GetUserByCredentialsAsync(login.UserName, login.Password);

			if (user != null)
			{
				// 2. יצירת רשימת ה"טענות" (Claims)
				var claims = new List<Claim>
				{
					new Claim("Name", user.UserName),
					new Claim("Role", user.Role.ToString()), // התפקיד (Business/Charity)
                    new Claim("UserId", user.Id.ToString())
				};

				// 3. יצירת מפתח הצפנה
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
				var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				// 4. בניית הטוקן
				var token = new JwtSecurityToken(
					issuer: _configuration["JWT:Issuer"],
					audience: _configuration["JWT:Audience"],
					claims: claims,
					expires: DateTime.Now.AddHours(3),
					signingCredentials: sc
				);

				// 5. שליחת הטוקן חזרה
				return Ok(new JwtSecurityTokenHandler().WriteToken(token));
			}

			return Unauthorized("שם משתמש או סיסמה שגויים");
		}

		//[HttpPost("register")]
		//public async Task<ActionResult> Register([FromBody] User value) {
		//	if (await _userService.IsUserNameTakenAsync(value.UserName))
		//	{
		//		return Conflict("User name already exists");
		//	}

		//	var user = new User { UserName = value.UserName, Password = value.Password, Role = eRole.Business };
		//	var createdUser = await _userService.AddUserAsync(user);

		//	var business = _mapper.Map<Business>(value);
		//	business.User = user;
		//	await _businessService.AddBusinessAsync(business);

		//	return Ok();
		//}

		[HttpPost("register/business")]
		public async Task<ActionResult> RegisterBusiness([FromBody] BusinessLoginModel value)
		{
			if (await _userService.IsUserNameTakenAsync(value.User.UserName))
			{
				return Conflict("User name already exists");
			}

			var user = new User { UserName = value.User.UserName, Password = value.User.Password, Role = eRole.Business };
			var createdUser = await _userService.AddUserAsync(user);

			var business = _mapper.Map<Business>(value.Business);
			business.User = user;
			await _businessService.AddBusinessAsync(business);

			return Ok();
		}


		[HttpPost("register/charity")]
		public async Task<ActionResult> RegisterCharity([FromBody] CharityLoginModel value)
		{
			if (await _userService.IsUserNameTakenAsync(value.User.UserName))
			{
				return Conflict("User name already exists");
			}

			var userEntity = new User { UserName = value.User.UserName, Password = value.User.Password, Role = eRole.Charity };
			var createdUser = await _userService.AddUserAsync(userEntity);

			var charity = _mapper.Map<Charity>(value.Charity);
			charity.User = createdUser;

			await _charityService.AddCharityAsync(charity);

			return Ok();
		}
	}
}