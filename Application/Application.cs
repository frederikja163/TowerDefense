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
        private long _ticksAtLastUpdate;
        private readonly Stopwatch _gameWatch;

        public Application()
        {
            _game = new GameData(
                ImmutableArray<Enemy>.Empty,
                ImmutableArray<Tower>.Empty,
                ImmutableArray<Projectile>.Empty,
                null,
                0);

            _gameWatch = new Stopwatch();
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
            _gameWatch.Start();
            Thread simulationThread = new Thread(SimulationThreadRun);
            simulationThread.Start();
            Thread renderingThread = new Thread(RenderingThreadRun);
            renderingThread.Start();
            
            while (_isRunning)
            {
                _platform.PollInput();
            }
            _gameWatch.Stop();
        }

        private void RenderingThreadRun()
        {
            _platform.InitializeRendering();
            GameData lastTick = _game;
            GameData nextTick = _game;
            while (_isRunning)
            {
                float percentage;
                lock (_game)
                {
                    if (nextTick.Tick != _game.Tick)
                    {
                        lastTick = nextTick;
                        nextTick = _game;
                    }
                    percentage = ((_gameWatch.ElapsedTicks - _ticksAtLastUpdate) / (Stopwatch.Frequency * 0.05f));
                }

                foreach (IRenderer renderer in _renderers)
                {
                    renderer.Render(lastTick, nextTick, percentage);
                }
                _platform.SwapBuffers();
            }
        }

        private void SimulationThreadRun()
        {
            while (_isRunning)
            {
                GameData intermidiateData = _game with {Tick = _game.Tick + 1};
                foreach (ISimulator simulator in _simulators)
                {
                    intermidiateData = simulator.Tick(intermidiateData);
                }

                lock (_game)
                {
                    _game = intermidiateData;
                    _ticksAtLastUpdate = _gameWatch.ElapsedTicks;
                }
                Thread.Sleep(1000/20);
            }
        }

        public void Exit()
        {
            _platform.Dispose();
        }
    }
}
