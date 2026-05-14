using AutoMapper;
using Food_Rescue.Models;
using FoodRescue.Core.DTO;
using FoodRescue.Core.Entities;
using FoodRescue.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodRescue.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DonationController : ControllerBase
	{
		private readonly IDonationService _donationService;
		private readonly IMapper _mapper;
		private readonly IBusinessService _businessService;
		public DonationController(IDonationService donationService, IMapper mapper, IBusinessService businessService)
		{
			_donationService = donationService;
			_mapper = mapper;
			_businessService = businessService;
		}
		// GET: api/<BusinessesController>
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var s = await _donationService.GetDonationsAsync();
			return Ok(_mapper.Map<IEnumerable<DonationDTO>>(s));
		}

		// GET api/<BusinessesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			var d =await _donationService.GetDonationByIdAsync(id);
			if (d == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<DonationDTO>(d));
		}

		// POST api/<BusinessesController>
		[HttpPost]
		[Authorize(Roles = "Business")]
		public async Task<ActionResult> Post([FromBody] DonationPostModel value)
		{
			// 1. חילוץ ה-UserId מהטוקן
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId)) return Unauthorized();

			// 2. שליפה נקייה ויעילה של העסק הספציפי!
			var myBusiness = await _businessService.GetBusinessByUserIdAsync(userId);
			if (myBusiness == null) return BadRequest("לא נמצא עסק התואם למשתמש המחובר.");

			// 3. קישור התרומה לעסק ושמירה
			var donation = _mapper.Map<Donation>(value);
			donation.BusinessID = myBusiness.Id;
			await _donationService.AddDonationAsync(donation);

			return Ok(donation);
		}

		// PUT api/<BusinessesController>/5
		[HttpPut("{id}")]
		[Authorize(Roles = "Business")]
		public async Task<ActionResult> Put(int id, [FromBody] DonationPostModel value)
		{
			var donation = _mapper.Map<Donation>(value);
			donation.Id = id;
			var s =await _donationService.GetDonationByIdAsync(id);
			if (s == null)
			{
				return NotFound();
			}
			await _donationService.UpdateDonationAsync(id, donation);
			return Ok(s);
		}

		[HttpPut("claim/{id}")]
		[Authorize(Roles = "Charity")]
		public async Task<ActionResult> Put(int id)
		{
			var donation = await _donationService.GetDonationByIdAsync(id);
			
			if (donation == null)
			{
				return NotFound();
			}
			await _donationService.ClaimDonationAsync(id);
			return Ok(donation);
		}

		// DELETE api/<BusinessesController>/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "Business")]
		public async Task<ActionResult> Delete(int id)
		{
			var s =await _donationService.GetDonationByIdAsync(id);
			if (s == null)
			{
				return NotFound();
			}
			await _donationService.DeleteDonationAsync(id);
			return Ok(s);

		}

		[HttpGet("my")]
		[Authorize(Roles = "Business")]
		public async Task<ActionResult> GetMyDonations()
		{
			// 1. חילוץ מזהה המשתמש מהטוקן
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
			{
				return Unauthorized("לא ניתן לזהות את המשתמש מהטוקן.");
			}

			// 2. שליפה נקייה ויעילה של העסק ישירות ממסד הנתונים!
			var myBusiness = await _businessService.GetBusinessByUserIdAsync(userId);

			if (myBusiness == null)
			{
				return BadRequest("לא נמצא עסק התואם למשתמש המחובר.");
			}

			// 3. שליפת התרומות של העסק הזה והמרתן ל-DTO
			var myDonations = await _donationService.GetDonationsByBusinessIdAsync(myBusiness.Id);

			return Ok(_mapper.Map<IEnumerable<DonationDTO>>(myDonations));
		}
	}
}
