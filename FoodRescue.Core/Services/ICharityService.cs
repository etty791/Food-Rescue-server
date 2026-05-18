using FoodRescue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Core.Services
{
	public interface ICharityService
	{
		public Task<IEnumerable<Charity>> GetCharitiesAsync();
		public Task<Charity> GetCharityByIdAsync(int id);
		public Task<Charity> GetCharityByNameAsync(string name);
		public Task AddCharityAsync(Charity val);
		public Task DeleteCharityAsync(int id);
		public Task UpdateCharityAsync(int id, Charity val);
		public Task<Charity> GetCharityByUserIdAsync(int userId);

	}
}
