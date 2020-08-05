using Eshopworld.Core;
using EShopworld.WorkerProcess;
using EShopworld.WorkerProcess.Configuration;
using EShopworld.WorkerProcess.Stores;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using WorkerProcessTest.Mocks;

namespace WorkerProcessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddOptions();

            //TODO: Add creds to esw-pricing-test
            var client = new DocumentClient(
                new Uri(""), "",
                null, ConsistencyLevel.Strong);

            services.AddSingleton<IDocumentClient>(client);

            var builder = new ConfigurationBuilder().AddJsonFile("configuration.json");
            IConfiguration appConfiguration = builder.Build();
            services.Configure<WorkerLeaseOptions>(appConfiguration.GetSection("WorkerLeaseOptions"));
            services.Configure<CosmosDataStoreOptions>(appConfiguration.GetSection("CosmosDataStoreOptions"));
            services.AddWorkerLease();

            var bigBrother = new BigBrotherMock();
            services.AddSingleton<IBigBrother>(bigBrother);

            var serviceProvider = services.BuildServiceProvider();
            var leaseStore = serviceProvider.GetService<ILeaseStore>();
            leaseStore.InitialiseAsync().Wait();

            var workerLease = serviceProvider.GetService<IWorkerLease>();
            var leaseService = new LeaseService(workerLease);
            leaseService.StartLeasing();

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Thread.Sleep(100);
            }
        }
    }
}
