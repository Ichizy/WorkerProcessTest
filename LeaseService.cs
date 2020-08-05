using EShopworld.WorkerProcess;
using System;
using System.Threading;

namespace WorkerProcessTest
{
    public class LeaseService
    {
        private IWorkerLease _workerLease;

        public LeaseService(IWorkerLease workerLease)
        {
            _workerLease = workerLease;
        }

        public void StartLeasing()
        {
            _workerLease.LeaseAllocated += _workerLease_LeaseAllocated;
            _workerLease.LeaseExpired += _workerLease_LeaseExpired;

            _workerLease?.StartLeasing();
        }

        private bool DoLongTermJob(string leaseId)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Long term job started:");

            Thread.Sleep(100000);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Long term job finished:");
            return true;
        }

        private void _workerLease_LeaseAllocated(object sender, LeaseAllocatedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var lease = (WorkerLease)sender;
            Console.WriteLine($"[{DateTime.UtcNow}] Lease {lease.InstanceId} allocated until [{e.Expiry}]");

            if (DoLongTermJob(lease.InstanceId.ToString()))
            {
                lease.StopLeasing();
            }
        }

        private void _workerLease_LeaseExpired(object sender, EventArgs e)
        {
            var lease = (WorkerLease)sender;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.UtcNow}] Lease {lease.InstanceId} expired");
        }
    }
}
