using FoodRescue.Core.Entities;
using FoodRescue.Core.Repositories;
using FoodRescue.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Service
{
	public class CharityService: ICharityService
	{
		private readonly ICharityRepository _charityRepository;
		
		public CharityService(ICharityRepository charityRepository)
		{
			_charityRepository = charityRepository;
		}

		public async Task AddCharityAsync(Charity val)
		{
			await _charityRepository.AddAsync(val);
			await _charityRepository.SaveAsync();
		}

		public async Task DeleteCharityAsync(int id)
		{
			await _charityRepository.DeleteAsync(id);
			await _charityRepository.SaveAsync();
		}

		public async Task<IEnumerable<Charity>> GetCharitiesAsync()
		{
			return await _charityRepository.GetAllAsync();
		}

		public async Task<Charity> GetCharityByIdAsync(int id)
		{
			return await _charityRepository.GetByIdAsync(id);
		}

		public async Task<Charity> GetCharityByNameAsync(string name)
		{
			return await _charityRepository.GetByNameAsync(name);
		}

		public async Task UpdateCharityAsync(int id, Charity val)
		{
			await _charityRepository.UpdateAsync(id, val);
			await _charityRepository.SaveAsync();
		}

		public async Task<Charity> GetCharityByUserIdAsync(int userId)
		{
			return await _charityRepository.GetByUserIdAsync(userId);
		}
	}
}
