using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
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
            _game = new GameData(
                ImmutableArray<Enemy>.Empty,
                ImmutableArray<Tower>.Empty,
                ImmutableArray<Projectile>.Empty,
                null,
                0
                );

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
            // TODO: Move performance measurements to a debug screen of some kind.
            int frames = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (_isRunning)
            {
                _platform.PollInput();

                _game = _game with {Tick = _game.Tick + 1};
                foreach (ISimulator simulator in _simulators)
                {
                    _game = simulator.Tick(_game);
                }
                watch.Stop();
                Thread.Sleep(1000/20);
                watch.Start();

                foreach (IRenderer renderer in _renderers)
                {
                    renderer.Render(_game);
                }

                if (frames++ >= 100)
                {
                    frames = 0;
                    Console.WriteLine(watch.ElapsedTicks / (float)Stopwatch.Frequency * 1000);
                    watch.Restart();
                }
            }
        }

        public void Exit()
        {
            _platform.Dispose();
        }
    }
}
