using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace TowerDefense.Common
{
    public enum Activities
    {
        ExitApplication,
    }
    public enum MovementActivities
    {
        PlaceTower,
    }

    public delegate void ActivityEvent(Activities activities);
    public sealed class Activity
    {
        internal Activity(Activities type)
        {
            Type = type;
        }
            
        public Activities Type { get; }
            
        public void Call()
        {
            Callback?.Invoke(Type);
        }
        public event ActivityEvent? Callback;
    }

    public delegate void MovementActivityEvent(MovementActivities activities, Vector2 position);
    public sealed class MovementActivity
    {
        internal MovementActivity(MovementActivities type)
        {
            Type = type;
        }
            
        public MovementActivities Type { get; }
            
        public void Call(Vector2 position)
        {
            Callback?.Invoke(Type, position);
        }
        public event MovementActivityEvent? Callback;
    }
    
    public sealed class ActivityList
    {
        private readonly Dictionary<Activities, Activity> _activities;
        private readonly Dictionary<MovementActivities, MovementActivity> _movementActivities;
        
        public ActivityList()
        {
            Array activityValues = Enum.GetValues(typeof(Activities));
            _activities = new Dictionary<Activities, Activity>(activityValues.Length);
            foreach (Activities value in activityValues)
            {
                _activities.Add(value, new Activity(value));
            }
            
            Array movementValues = Enum.GetValues(typeof(MovementActivities));
            _movementActivities = new Dictionary<MovementActivities, MovementActivity>(movementValues.Length);
            foreach (MovementActivities value in movementValues)
            {
                _movementActivities.Add(value, new MovementActivity(value));
            }
        }

        public Activity this[Activities type] => _activities[type];
        public MovementActivity this[MovementActivities type] => _movementActivities[type];
    }
}