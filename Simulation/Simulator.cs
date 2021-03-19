using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Common;

namespace TowerDefense.Simulation
{
    public static class Simulator
    {
        public static IReadOnlyCollection<ISimulator> GetSimulators(ActivityList activities)
        {
            return new ISimulator[]
            {
                new EnemySimulator(),
                new TowerSimulator(activities),
            };
        }
    }
}
