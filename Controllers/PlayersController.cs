using Amaliy_ish.Models;
using Amaliy_ish.Models.Data;
using Amaliy_ish.MyContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amaliy_ish.Controllers
{
	public class PlayersController : Controller
	{
		private readonly MyDbContext myDbContext;

		public PlayersController(MyDbContext myDbContext)
        {
			this.myDbContext = myDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> Update()
		{
			var players = await myDbContext.Players.ToListAsync();
			return View(players);
		}

		[HttpGet]

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Add(AddPlayersViewModel addPlayersRequest)
		{
			var player = new Player()
			{
				Id = Guid.NewGuid(),
				Name = addPlayersRequest.Name,
				Firstname = addPlayersRequest.Firstname,
				Age = addPlayersRequest.Age,
				Position = addPlayersRequest.Position,
				TShirtNumber = addPlayersRequest.TShirtNumber,
				Nationality = addPlayersRequest.Nationality,
			};

			await myDbContext.Players.AddAsync(player);
			await myDbContext.SaveChangesAsync();
			return View("Add");
		}

		[HttpGet]

		public async Task<IActionResult> View(Guid Id)
		{
			var player = await myDbContext.Players.FirstOrDefaultAsync(x => x.Id == Id);

			if(player is not null)
			{
				var viewModel = new UpdatePlayerViewModel()
				{
					Id = player.Id,
					Name = player.Name,
					Firstname = player.Firstname,
					Age = player.Age,
					Position = player.Position,
					TShirtNumber = player.TShirtNumber,
					Nationality = player.Nationality,
				};
				return await Task.Run(() => View("View", viewModel));
			}
			return RedirectToAction("Update");
		}

		[HttpPost]

		public async Task<IActionResult> View(UpdatePlayerViewModel model)
		{
			var player = await myDbContext.Players.FindAsync(model.Id);
			
			if(player is not null)
			{
				player.Name = model.Name;
				player.Firstname = model.Firstname;
				player.Age = model.Age;
				player.Position = model.Position;
				player.TShirtNumber = model.TShirtNumber;
				player.Nationality = model.Nationality;

				await myDbContext.SaveChangesAsync();
				
				return RedirectToAction("Update");
			}
			return RedirectToAction("Update");
		}

		[HttpPost]

		public async Task<IActionResult> Delete(UpdatePlayerViewModel model)
		{
			var player = await myDbContext.Players.FindAsync(model.Id);

			if(player is not null)
			{
				myDbContext.Players.Remove(player);
				await myDbContext.SaveChangesAsync();
				return RedirectToAction("Update");
			}
			return RedirectToAction("Update");
		}
	}
}
