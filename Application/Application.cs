using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform;
using TowerDefense.Platform.Glfw;
using TowerDefense.Simulation;

namespace TowerDefense
{
    internal sealed class Application
    {
        private const int TicksPerSecond = 20;
        private readonly GlfwPlatform _platform;
        private readonly Simulator _simulator;
        private readonly ActivityList _activities;

        private GameData _game;
        private bool _isRunning = true;
        private long _ticksAtLastUpdate;
        private readonly Stopwatch _gameWatch;

        public Application()
        {
            Log.Info("Initializing.");
            
            _game = new GameData(
                ImmutableArray<Enemy>.Empty,
                ImmutableArray<Tower>.Empty,
                ImmutableArray<Projectile>.Empty,
                null,
                0);

            _gameWatch = new Stopwatch();

            _activities = new ActivityList();
            _platform = new GlfwPlatform(_activities);
            
            _simulator = new Simulator(_activities);
            
            _activities[Activities.ExitApplication].Callback += OnExitApplication;
        }

        private void OnExitApplication(Activities activities)
        {
            _isRunning = false;
        }

        public void Run()
        {
            Log.Info("Initializing threads.");
            _gameWatch.Start();
            Thread simulationThread = new Thread(SimulationThreadRun);
            simulationThread.Start();
            Thread renderingThread = new Thread(RenderingThreadRun);
            renderingThread.Start();
            
            Log.Info("Initializing input thread.");
            while (_isRunning)
            {
                _platform.PollInput();
            }
            _gameWatch.Stop();
        }

        private void RenderingThreadRun()
        {
            Log.Info("Initializing rendering thread.");
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
                    percentage = ((_gameWatch.ElapsedTicks - _ticksAtLastUpdate) / ((float)Stopwatch.Frequency / TicksPerSecond));
                }

                _platform.Render(lastTick, nextTick, percentage);
                _platform.SwapBuffers();
            }
        }

        private void SimulationThreadRun()
        {
            Log.Info("Initializing simulation thread.");
            while (_isRunning)
            {
                GameData tempData = _game;
                tempData = _simulator.Tick(tempData);

                lock (_game)
                {
                    _game = tempData;
                    _ticksAtLastUpdate = _gameWatch.ElapsedTicks;
                }
                Thread.Sleep(1000/TicksPerSecond);
            }
        }

        public void Exit()
        {
            _platform.Dispose();
        }
    }
}
