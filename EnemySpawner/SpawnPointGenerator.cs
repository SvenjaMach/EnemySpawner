using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EnemySpawner
{
  public class SpawnPointGenerator
  {
    int id;
    public List<Enemy> generateEnemies(Maze maze)
    {
      List<Enemy> enemies = new List<Enemy>();
      StreamReader reader = new StreamReader("currentId.txt");
      id = Int32.Parse(reader.ReadLine());
      reader.Dispose();

      bool[,] blocks = new bool[18, 9];
      var rng = new Random();

      for (int i=0; i<18;i++)
      {
        for(int j=0; j<9;j++)
        {
          blocks[i, j] = false;

          if(i==17)
          {
            blocks[i, j] = true;
          }
        }
      }

      foreach(BlockPosition block in maze.maze)
      {
        blocks[block.x/3 , block.y/3] = true;
      }

      enemies.AddRange(generateSidewaysWalker(blocks, rng));
      enemies.AddRange(generateUpDownWalker(blocks, rng));

      StreamWriter writer = new StreamWriter("currentId.txt");
      writer.Write(id.ToString());
      writer.Dispose();
      return enemies;
    }

    private List<Enemy> generateSidewaysWalker(bool[,] maze, Random rng)
    {
      List<Enemy> sidewaysEnemies = new List<Enemy>();
      for(int i=0; i<18;i++)
      {
        for (int j = 0; j < 9; j++)
        {
          if(maze[i,j]==false && ((i==0|| (i>0 && maze[i-1, j] == true)) && (i<16 && maze[i+1, j] == false)))
          {
            int enemyXEnd = 0;
            int counterstart = i;
            while(!maze[counterstart+1, j])
            {
              counterstart++;
              enemyXEnd++;
            }

              sidewaysEnemies.Add(new Enemy { EnemyID = 1, startX = i * 3, startY = j * 3, endX=(i+enemyXEnd)*3, endY=j*3, Id = id });
              id++;
          }
        }
      }

      return sidewaysEnemies;
    }

    private List<Enemy> generateUpDownWalker(bool[,] maze, Random rng)
    {
      List<Enemy> upDownEnemies = new List<Enemy>();
      for (int i = 0; i < 17; i++)
      {
        for (int j = 1; j < 9; j++)
        {
          if (maze[i, j] == false && ((j == 0 || (j > 0 && maze[i, j-1] == true)) && (j < 7 && maze[i , j+1] == false)))
          {
            int enemyYEnd = 0;
            int counterstart = j;
            while (!maze[i, counterstart+1])
            {
              counterstart++;
              enemyYEnd++;
              if(counterstart>=8)
              {
                break;
              }
            }

            upDownEnemies.Add(new Enemy { EnemyID = 2, startX = i * 3, startY = j * 3, endX = i * 3, endY = (j-enemyYEnd) * 3, Id = id });
            id++;
          }
        }
      }

      return upDownEnemies;
    }
  }
}
