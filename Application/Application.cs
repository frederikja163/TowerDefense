using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform;
using TowerDefense.Simulation;

namespace TowerDefense
{
    internal sealed class Application
    {
        private readonly IPlatform _platform;
        private readonly IReadOnlyCollection<IRenderer> _renderers;
        private readonly IReadOnlyCollection<ISimulator> _simulators;
        private readonly ActivityList _activities;

        private GameData _game;
        private bool _isRunning = true;

        public Application()
        {
            _game = new GameData(ImmutableArray<Enemy>.Empty, ImmutableArray<Tower>.Empty, null);

            _platform = Platformer.GetPlatform();

            _renderers = Platformer.GetRenderers();

            _activities = new ActivityList();
            _platform.ImplementActivities(_activities);

            _simulators = Simulator.GetSimulators(_activities);
            
            _activities[Activities.ExitApplication].Callback += OnExitApplication;
        }

        private void OnExitApplication(Activities activities)
        {
            _isRunning = false;
        }

        public void Run()
        {
            _platform.InitializeRendering();
            while (_isRunning)
            {
                _platform.PollInput();

                foreach (ISimulator simulator in _simulators)
                {
                    _game = simulator.Tick(_game);
                }
                Thread.Sleep(1000/60);

                foreach (IRenderer renderer in _renderers)
                {
                    renderer.Render(_game);
                }
            }
        }

        public void Exit()
        {
            _platform.Dispose();
        }
    }
}
