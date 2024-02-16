using Microsoft.AspNetCore.Mvc;
using GameSalaryApi.Models;

namespace VideoGameSalariesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoGameSalariesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VideoGameSalary>> Get()
        {
            var videoGameSalaries = new List<VideoGameSalary>
            {
                new() { Title = "Minecraft", Salary = 200000000m },
                new() { Title = "Grand Theft Auto V", Salary = 135000000m },
                new() { Title = "Tetris", Salary = 100000000m },
                new() { Title = "Wii Sports", Salary = 82750000m },
                new() { Title = "PUBG", Salary = 70000000m },
                new() { Title = "Super Mario Bros.", Salary = 40240000m },
                new() { Title = "Pokémon Red/Green/Blue/Yellow", Salary = 47500000m },
                new() { Title = "FIFA 18", Salary = 26000000m },
                new() { Title = "Mario Kart Wii", Salary = 37200000m },
                new() { Title = "The Legend of Zelda: Breath of the Wild", Salary = 23000000m }
            };

            return Ok(videoGameSalaries);
        }
    }
}
