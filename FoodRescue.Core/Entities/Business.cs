namespace FoodRescue.Core.Entities
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public List<Donation> donations { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
