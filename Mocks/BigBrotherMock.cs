using Eshopworld.Core;
using EShopworld.WorkerProcess.Telemetry;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WorkerProcessTest.Mocks
{
    public class BigBrotherMock : IBigBrother
    {
        public IBigBrother DeveloperMode()
        {
            return null;
        }

        public void Flush()
        {
        }

        public void Publish<[NullableAttribute(0)] T>(T @event, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1) where T : TelemetryEvent
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (@event is LeaseAcquisitionEvent leaseEvent)
            {
                Console.WriteLine($"LOG[{DateTime.UtcNow}] {callerMemberName} {leaseEvent.Reason}\t EventType: {@event.GetType()}");
            }
            else if (@event is LeaseReleaseEvent leaseReleaseEvent)
            {
                Console.WriteLine($"LOG[{DateTime.UtcNow}] {callerMemberName} {leaseReleaseEvent.Reason}\t EventType: {@event.GetType()}");
            }
            else if (@event is ExceptionEvent exceptionEvent)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR[{DateTime.UtcNow}] {exceptionEvent.Exception.Message}\n {exceptionEvent.Exception.InnerException}");
            }
        }

        public void Publish(object @event, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (@event is LeaseAcquisitionEvent leaseEvent)
            {
                Console.WriteLine($"LOG[{DateTime.UtcNow}] {callerMemberName} {leaseEvent.Reason}\t EventType: {@event.GetType()}");
            }
            else if (@event is LeaseReleaseEvent leaseReleaseEvent)
            {
                Console.WriteLine($"LOG[{DateTime.UtcNow}] {callerMemberName} {leaseReleaseEvent.Reason}\t EventType: {@event.GetType()}");
            }
            else if (@event is ExceptionEvent exceptionEvent)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR[{DateTime.UtcNow}] {exceptionEvent.Exception.Message}\n {exceptionEvent.Exception.InnerException}");
            }
        }

        public IKustoClusterBuilder UseKusto()
        {
            return null;
        }
    }
}
