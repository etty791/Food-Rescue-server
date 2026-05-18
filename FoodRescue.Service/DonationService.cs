using FoodRescue.Core.Entities;
using FoodRescue.Core.Repositories;
using FoodRescue.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Service
{
	public class DonationService : IDonationService
	{
		private readonly IDonationRepository _donationRepository;
		public DonationService(IDonationRepository donationRepository)
		{
			_donationRepository = donationRepository;
		}
		public async Task AddDonationAsync(Donation val)
		{
			await _donationRepository.AddAsync(val);
			await _donationRepository.SaveAsync();
		}

		public async Task DeleteDonationAsync(int id)
		{
			await _donationRepository.DeleteAsync(id);
			await _donationRepository.SaveAsync();
		}

		public async Task<Donation> GetDonationByIdAsync(int id)
		{
			return await _donationRepository.GetByIdAsync(id);
		}

		public async Task<IEnumerable<Donation>> GetDonationsAsync()
		{
			return await _donationRepository.GetAllAsync();
		}

		//public List<Donation> GetDonations()
		//{
		//	return _donationRepository.GetAll();
		//}

		public async Task UpdateDonationAsync(int id, Donation val)
		{
			if (val.Status==eDonationStatus.Available) {
				await _donationRepository.UpdateAsync(id, val);
				await _donationRepository.SaveAsync();
			}
		}
		public async Task ClaimDonationAsync(int donationId,int charityId)
		{
			Donation d = await _donationRepository.GetByIdAsync(donationId);
			d.Status=eDonationStatus.Claimed;
			d.CharityId = charityId;
			await _donationRepository.UpdateAsync(donationId, d);
			await _donationRepository.SaveAsync();
		}
		public async Task CollectDonationAsync(int id)
		{
			Donation d = await _donationRepository.GetByIdAsync(id);
			d.Status = eDonationStatus.Collected;
			await _donationRepository.UpdateAsync(id, d);
			await _donationRepository.SaveAsync();
		}
		public async Task<IEnumerable<Donation>> GetDonationsByBusinessIdAsync(int id)
		{
			return await _donationRepository.GetByBusinessIdAsync(id);
		}

		public async Task AddDonationToBusinessAsync(int businessId, Donation val)
		{
			val.BusinessID = businessId;
			await _donationRepository.AddAsync(val);
			await _donationRepository.SaveAsync();
		}

	}
}
