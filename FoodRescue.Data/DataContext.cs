using FoodRescue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRescue.Data
{
	public class DataContext:DbContext
	{
		public DbSet<Business> businesses { get; set; }
		public DbSet<Charity> charities { get; set; }
		public DbSet<Donation> donations { get; set; }
		public DbSet<User> users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

		{

			optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=FoodRescue_db;Trusted_Connection=True;TrustServerCertificate=True;");

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(b =>
			{
				b.Property(e => e.Role)
					.HasConversion(
						v => v.ToString(),
						v => Enum.Parse<eRole>(v));
			});
			modelBuilder.Entity<Donation>(d =>
			{
				d.Property(e => e.Status)
				 .HasConversion(
					 v => v.ToString(),
					 v => Enum.Parse<eDonationStatus>(v));
			});
			modelBuilder.Entity<Business>()
		.HasOne(b => b.User)
		.WithMany()
		.HasForeignKey(b => b.UserId)
		.OnDelete(DeleteBehavior.NoAction); // פה הפתרון!

			modelBuilder.Entity<Charity>()
				.HasOne(c => c.User)
				.WithMany()
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.NoAction); // וגם פה

			base.OnModelCreating(modelBuilder);
		}


	}
}
