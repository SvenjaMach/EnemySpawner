using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnemySpawner
{
  public static class APICaller
  {
    public static Maze GetMaze()
    {
      BoolHelper helper = new BoolHelper { isNewMaze = false };

      string newMaze2 = JsonSerializer.Serialize(helper);

      var maze =  CallRESTPost("https://localhost:44334/mazegenerator", newMaze2);
      if(maze == null || maze.Length<=1)
      {
        return new Maze();
      }

      return JsonSerializer.Deserialize<Maze>(maze);
    }

    private static string CallRESTPost(string url, string json)
    {
      int currentRetry = 0;
      int retryCount = 3;

      for (; ; )
      {
        try
        {
          var content = new StringContent(json, Encoding.UTF8, "application/json");
          HttpClient client = new HttpClient();
          HttpResponseMessage response = client.PostAsync(url, content).Result;
          response.EnsureSuccessStatusCode();
          return response.Content.ReadAsStringAsync().Result;
        }
        catch (Exception)
        {
          currentRetry++;

          if (currentRetry > retryCount)
          {
            return null;
          }
        }

        Task.Delay(100);
      }
    }

    public static void SaveEnemies(EnemyPositions positions)
    {
      string pos = JsonSerializer.Serialize(positions);
      CallRESTPost("https://localhost:44397/api/Damage/createenemies", pos);
    }
  }
}
