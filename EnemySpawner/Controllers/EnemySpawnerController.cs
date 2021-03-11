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
  public class EnemySpawnerController : ControllerBase
  {
    private readonly ILogger<EnemySpawnerController> _logger;
    private SpawnPointGenerator spawnPointGenerator;

    public EnemySpawnerController(ILogger<EnemySpawnerController> logger)
    {
      _logger = logger;
      spawnPointGenerator = new SpawnPointGenerator();
    }

    [HttpGet]
    public EnemyPositions GetEnemies()
    {
      Maze maze = APICaller.GetMaze();
      EnemyPositions cont = new EnemyPositions();
      cont.Enemies = spawnPointGenerator.generateEnemies(maze);
      APICaller.SaveEnemies(cont);
      return cont;
    }
  }
}
