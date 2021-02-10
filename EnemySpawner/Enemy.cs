using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnemySpawner
{
  [Serializable]
  public class Enemy
  {
    public int startX { get; set; }
    public int startY { get; set; }
    public int endX { get; set; }
    public int endY { get; set; }
    public int EnemyID { get; set; }
    public int Id { get; set; }
  }
}
