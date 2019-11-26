using McMaster.Extensions.CommandLineUtils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalhos.EventsEngine.ClientExample
{
    public class Commands
    {
        [Option(Description = "The subject", ShortName = "s")]
        public string Subject { get; }

        private async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            if (app.Options.Any(x => !x.HasValue()))
                app.ShowHelp();

            var count = 1;

            for (var i = 0; i < count; i++)
            {
                Console.Write($"Hello");

                Console.WriteLine($" {Subject}!");
                await Task.Delay(5000);
            }

            return 1;
        }
    }
}
