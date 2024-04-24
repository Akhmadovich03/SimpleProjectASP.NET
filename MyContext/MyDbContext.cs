using Amaliy_ish.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Amaliy_ish.MyContext
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Player> Players { get; set; }
	}
}
