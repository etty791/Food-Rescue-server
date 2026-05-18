namespace FoodRescue.Core.Entities
{
	public enum eDonationStatus
	{
		Available,
		Claimed,
		Collected
	}
	public class Donation
    {
        public int Id { get; set; }
        public int BusinessID { get; set; } 
        public Business Business { get; set; }
        public string FoodType { get; set; }
        public double Quantity { get; set; } // כמות ב-ק"ג
        public DateTime dateTime { get; set; }

public eDonationStatus Status { get; set; }
		public Charity? Charity { get; set; } 
		public int? CharityId { get; set; }


    }
}
