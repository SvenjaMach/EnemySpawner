using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnemySpawner
{
  public class SpawnPointGenerator
  {
    int id;
    public List<Enemy> generateEnemies(Maze maze)
    {
      id = 0;
      bool[,] blocks = new bool[17, 9];
      var rng = new Random();

      for (int i=0; i<17;i++)
      {
        for(int j=0; j<9;j++)
        {
          blocks[i, j] = false;
        }
      }

      foreach(BlockPosition block in maze.maze)
      {
        blocks[block.x/3, block.y/3] = true;
      }

      List<Enemy> enemies = new List<Enemy>();
      enemies.AddRange(generateSideways(blocks, rng));
      //enemies.AddRange(generateUpDown(blocks, rng));
      return enemies;
    }

    private List<Enemy> generateSideways(bool[,] maze, Random rng)
    {
      List<int> lines = new List<int>();
      List<Enemy> sidewaysEnemies = new List<Enemy>();
      for(int i=0; i<17;i++)
      {
        for (int j = 0; j < 9; j++)
        {
          if(!lines.Contains(j) && maze[i,j]==false && (i==0 || maze[i-1, j] == true))
          {
            //if(rng.Next(0, 5) == 0)
            //{

              sidewaysEnemies.Add(new Enemy { EnemyID = 0, startX = i * 3, startY = j * 3, endX=i*3+3, endY=j*3+3, Id = id });
              id++;
              lines.Add(j);
            //}
          }
        }
      }

      return sidewaysEnemies;
    }

    private List<Enemy> generateUpDown(bool[,] maze, Random rng)
    {
      List<int> column = new List<int>();
      List<Enemy> updownEnemies = new List<Enemy>();
      for (int j = 0; j < 9; j++)
      {
        for (int i = 0; i < 17; i++)
        {
          if (!column.Contains(i) && maze[i, j] == false && (j == 0 || maze[i, j-1] == true))
          {
            if (rng.Next(0, 9) == 0)
            {
              updownEnemies.Add(new Enemy { EnemyID = 0, startX = i * 3, startY = j * 3, endX = i * 3 + 3, endY = j * 3 + 3, Id = id });
              id++;
              column.Add(i);
            }
          }
        }
      }

      return updownEnemies;
    }
  }
}
