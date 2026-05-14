using FoodRescue.Core.Entities;
using FoodRescue.Core.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FoodRescue.Core.DTO;
using Food_Rescue.Models;
using FoodRescue.Service;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodRescue.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class BusinessesController : ControllerBase
	{
		private readonly IBusinessService _businessService;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;


		public BusinessesController(IBusinessService businessService, IMapper map, IUserService userService)
		{
			_businessService = businessService;
			_mapper = map;
			_userService = userService;
		}
		// GET: api/<BusinessesController>
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var b = await _businessService.GetBusinessesAsync();
			return Ok(_mapper.Map<IEnumerable<BusinessDTO>>(b));
		}

		// GET api/<BusinessesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			var s =await _businessService.GetBusinessByIdAsync(id);
			if (s == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<BusinessDTO>(s));
		}


		// PUT api/<BusinessesController>/5
		[HttpPut("{id}")]
		[Authorize(Roles = "Business")]
		public async Task<ActionResult> Put(int id, [FromBody] BusinessPostModel value)
		{

			var business = _mapper.Map<Business>(value);
			business.Id = id;
			var b =await _businessService.GetBusinessByNameAsync(value.Name);
			if (b == null)
			{
				return NotFound();
			}
			var user = await _userService.GetUserByIdAsync(business.UserId);
			if (user != null)
			{
				user.UserName = value.UserName;
				user.Password = value.Password;
				await _userService.UpdateUserAsync(user.Id, user);
			}
			await _businessService.UpdateBusinessAsync(id, business);
			return Ok();
		}

		// DELETE api/<BusinessesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var b =await _businessService.GetBusinessByIdAsync(id);
			if (b == null)
			{
				return NotFound();
			}
			await _userService.DeleteUserAsync(b.UserId);
			await _businessService.DeleteBusinessAsync(id);
			return Ok();
		}

	}
}
