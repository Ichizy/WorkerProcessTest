using EShopworld.WorkerProcess;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerProcessTest.Mocks
{
    public class AllocationDelayMock : IAllocationDelay
    {
        public TimeSpan Calculate(int priority, TimeSpan leaseInterval)
        {
            return new TimeSpan(0, 1, 0);
        }
    }
}
