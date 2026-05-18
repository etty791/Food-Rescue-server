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
	public class CharityRepository : ICharityRepository
	{
		private readonly DataContext _context;
		public CharityRepository(DataContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Charity val)
		{
			await _context.charities.AddAsync(val);
		}

		public async Task DeleteAsync(int id)
		{
			var ch = await GetByIdAsync(id);
			_context.charities.Remove(ch);
		}

		public async Task<IEnumerable<Charity>> GetAllAsync()
		{
			return await _context.charities.ToListAsync();
		}

		public async Task<Charity> GetByIdAsync(int id)
		{
			return await _context.charities.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task UpdateAsync(int id, Charity value)
		{
			var s =await GetByIdAsync(value.Id);
			s.Email = value.Email;
			s.City = value.City;
			s.Name = value.Name;
			s.FoodType=value.FoodType;
			s.Quantity = value.Quantity;
		}
		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<Charity> GetByNameAsync(string name)
		{
			return await _context.charities.FirstOrDefaultAsync(x => x.Name == name);
		}

		public async Task<Charity> GetByUserIdAsync(int userId)
		{
			return await _context.charities.FirstOrDefaultAsync(b => b.UserId == userId);
		}
	}
}
