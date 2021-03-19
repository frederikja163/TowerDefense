using System.Collections.Generic;
using TowerDefense.Common;
using TowerDefense.Platform;
using TowerDefense.Simulation;

namespace TowerDefense
{
    internal sealed class Application
    {
        private readonly IPlatform _platform;
        private readonly IReadOnlyCollection<IRenderer> _renderers;
        private readonly IReadOnlyCollection<ISimulator> _simulators;

        private GameData _game;
        private bool _isRunning = true;

        public Application()
        {
            _game = new GameData();

            _platform = Platformer.GetPlatform();

            _renderers = Platformer.GetRenderers();

            _simulators = Simulator.GetSimulators();
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
