﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.Common
{
    public interface IPlatform : IDisposable
    {
        void ImplementActivities(ActivityList activities);
        
        void InitializeRendering();
        
        void PollInput();

        void SwapBuffers();
    }
}
