using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace EnemySpawner.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public EnemyPositions POST([FromBody]Maze mazejson)
    {
      EnemyPositions cont = new EnemyPositions();
      if (mazejson != null)
      {
        SpawnPointGenerator spawnPointGenerator = new SpawnPointGenerator();
        cont.Enemies = spawnPointGenerator.generateEnemies(mazejson);
      }
      else
      {
        cont.Enemies = new List<Enemy>();
        cont.Enemies.Add(new Enemy() { EnemyID = 1, Id = 1, startX = 1, startY = 1 });
      }
      return cont;
    }
  }
}
