using FoodRescue.Core.DTO;
using FoodRescue.Core.Entities;

namespace Food_Rescue.Models
{
	public class DonationPostModel
	{
		public string FoodType { get; set; }
		public double Quantity { get; set; } // כמות ב-ק"ג
		public DateTime dateTime { get; set; }
		public eDonationStatus Status { get; set; }
	}
}
