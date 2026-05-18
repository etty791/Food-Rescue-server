using FoodRescue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Core.Services
{
	public interface IDonationService
	{
		public Task<IEnumerable<Donation>> GetDonationsAsync();
		public Task<Donation> GetDonationByIdAsync(int id);
		public Task AddDonationAsync(Donation val);
		public Task DeleteDonationAsync(int id);
		public Task UpdateDonationAsync(int id, Donation val);
		public Task CollectDonationAsync(int id);
		public Task ClaimDonationAsync(int donationId, int charityId);
		public Task AddDonationToBusinessAsync(int businessId, Donation val);

		public Task<IEnumerable<Donation>> GetDonationsByBusinessIdAsync(int id);
	}
}
