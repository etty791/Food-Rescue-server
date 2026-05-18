using FoodRescue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Core.Repositories
{
	public interface ICharityRepository
	{
		public Task<IEnumerable<Charity>> GetAllAsync();
		public Task<Charity> GetByIdAsync(int id);
		public Task<Charity> GetByNameAsync(string name);
		public Task<Charity> GetByUserIdAsync(int userId);

		public Task AddAsync(Charity val);
		public Task DeleteAsync(int id);
		public Task UpdateAsync(int id, Charity value);
		public Task SaveAsync();

	}
}
