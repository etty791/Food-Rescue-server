using FoodRescue.Core.Entities;
using FoodRescue.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Data
{
	public class DonationRepository: IDonationRepository
	{
		private readonly DataContext _context;
		public DonationRepository(DataContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Donation val)
		{
			await _context.donations.AddAsync(val);
		}

		public async Task DeleteAsync(int id)
		{
			var don = await GetByIdAsync(id);
			_context.donations.Remove(don);
		}

		public async Task<IEnumerable<Donation>> GetAllAsync()
		{
			return await _context.donations
							 .Include(d => d.Business)
							 .ToListAsync();
		}

		public async Task<Donation> GetByIdAsync(int id)
		{
			return await _context.donations
							 .Include(d => d.Business)
							 .FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task UpdateAsync(int id, Donation value)
		{
			var s =await GetByIdAsync(value.Id);
			s.dateTime = value.dateTime;
			s.Quantity = value.Quantity;
			s.FoodType = value.FoodType;
			s.Status = value.Status;
		}
		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Donation>> GetByBusinessIdAsync(int businessId)
		{
			return await _context.donations
						 .Where(d => d.BusinessID == businessId)
						 .ToListAsync();
		}
	}
}
