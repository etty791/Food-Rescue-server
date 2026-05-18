using FoodRescue.Core.Entities;
using FoodRescue.Core.Repositories;
using FoodRescue.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Data
{
	public class BusinessRepository: IBusinessRepository
	{
		private readonly DataContext _context;
		public BusinessRepository(DataContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Business val)
		{
			await _context.businesses.AddAsync(val);
		}

		public async Task DeleteAsync(int id)
		{
			var bus = await GetByIdAsync(id);
			_context.businesses.Remove(bus);
		}

		public async Task<IEnumerable<Business>> GetAllAsync()
		{
			return await _context.businesses.ToListAsync();
		}

		public async Task<Business> GetByIdAsync(int id)
		{
			return await _context.businesses.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<Business> GetByUserIdAsync(int userId)
		{
			return await _context.businesses.FirstOrDefaultAsync(b => b.UserId == userId);
		}
		public async Task UpdateAsync(int id,Business value)
		{
			var s = await GetByIdAsync(value.Id);
			s.Email = value.Email;
			s.City = value.City;
			s.Name = value.Name;
		}
		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<Business> GetByNameAsync(string name)
		{
			return await _context.businesses.FirstOrDefaultAsync(x => x.Name == name);
		}
	}
}
