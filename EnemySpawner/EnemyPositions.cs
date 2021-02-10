using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnemySpawner
{
  [Serializable]
  public class EnemyPositions
  {
    public List<Enemy> Enemies {get;set;}
  }
}
