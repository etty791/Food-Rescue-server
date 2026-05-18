namespace FoodRescue.Core.Entities
{
	public class Charity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
		public string FoodType { get; set; }
		public double Quantity { get; set; } // כמות ב-ק"ג
        public List<Donation> Donations { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
