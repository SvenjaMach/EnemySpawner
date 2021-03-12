using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnemySpawner
{
  public class Maze
  {
    public List<BlockPosition> maze { get; set; }
    public Maze()
    {
      maze = new List<BlockPosition>();
    }
  }
  public class BlockPosition
  {
    public int x { get; set; }
    public int y { get; set; }
  }
}
