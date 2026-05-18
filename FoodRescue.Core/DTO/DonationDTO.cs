using FoodRescue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Core.DTO
{
	public class DonationDTO
	{
		public int Id { get; set; }
		public string FoodType { get; set; }
		public double Quantity { get; set; } // כמות ב-ק"ג
		public DateTime dateTime { get; set; }
		public eDonationStatus Status { get; set; }
		public BusinessDTO Business { get; set; }

	}
}
