﻿using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trabalhos.EventsEngine.Messages;

namespace Trabalhos.EventsEngine.ClientExample
{
    public class SenderHostedService : IHostedService
    {
        private readonly IBusControl eventsEngine;
        static Random rnd = new Random();
        private readonly ILogger<SenderHostedService> logger;

        public SenderHostedService(IBusControl eventsEngine, ILogger<SenderHostedService> logger)
        {
            this.eventsEngine = eventsEngine;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var products = new List<(string name, decimal price)>();
            var users = new List<Guid>(); 
            users.Add(Guid.Parse("016c9b45-72ae-4091-83d7-2b0da7ccae92"));
            users.Add(Guid.Parse("016c9b45-72ae-4091-83d7-2b0da7ccae92"));

            Console.WriteLine("Welcome to the Shop");
            Console.WriteLine("Press Q key to exit");
            Console.WriteLine("Press [0..9] key to order some products");
            Console.WriteLine(string.Join(Environment.NewLine, Products.Select((x, i) => $"[{i}]: {x.name} @ {x.price:C}")));
            for (;;)
            {
                int r = rnd.Next(users.Count);
                var consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }

                if (char.IsNumber(consoleKeyInfo.KeyChar))
                {
                    var product = Products[(int)char.GetNumericValue(consoleKeyInfo.KeyChar)];
                    products.Add(product);
                    Console.WriteLine($"Added {product.name}");
                }

                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    await eventsEngine.Publish<IDummyRequest>(new
                    {
                        ForEmployee = users[r],
                        requestedData = products.Select(x => new { Name = x.name, Price = x.price,}).ToList()
                    });

                    Console.WriteLine("Submitted Order");

                    products.Clear();
                }
            }
        }

        // public async Task StartAsync(CancellationToken cancellationToken)
        // {
        //     var random = new Random();
        //     Action<string> writeFancy = x =>
        //     {
        //         Console.BackgroundColor = (ConsoleColor)random.Next(15);
        //         Console.ForegroundColor = (ConsoleColor)random.Next(15);
        //         Console.WriteLine(x);
        //     };
        //     int count = 0;
        //     Console.Clear();
        //     writeFancy("Welcome to the Shop");
        //     writeFancy("Press Q key to exit");
        //     writeFancy("Press [0..9] key to order some products");
        //     Products.Select((x, i) => $"[{i}]: {x.name} @ {x.price:C}").ToList().ForEach((x) =>
        //     {
        //         writeFancy(x);
        //     });

        //     var products = new List<(string name, decimal price)>();
        //     for (;;)
        //     {
        //         for (int i = 0; i < random.Next(1); i++)
        //         {
        //             var product = Products[random.Next(9)];
        //             products.Add(product);
        //             writeFancy($"Added {product.name}");
        //         }

        //         await eventsEngine.Publish<IDummyRequest>(new
        //         {
        //             requestedData = products.Select(x => new { Name = x.name, Price = x.price }).ToList()
        //         });

        //         writeFancy($"Submitted Order: {count++}");

        //         products.Clear();
        //     }
        // }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private static readonly IReadOnlyList<(string name, decimal price)> Products = new List<(string, decimal)>
        {
            ("Bread", 1.20m),
            ("Milk", 0.50m),
            ("Rice", 1m),
            ("Buttons", 0.9m),
            ("Pasta", 0.9m),
            ("Cereals", 1.6m),
            ("Chocolate", 2m),
            ("Noodles", 1m),
            ("Pie", 1m),
            ("Sandwich", 1m),
        };
    }
}
