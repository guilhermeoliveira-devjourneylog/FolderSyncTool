using System;
using System.Threading.Tasks;

namespace FolderSyncConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: FolderSyncConsole <source_folder> <replica_folder> <sync_interval_seconds>");
                return;
            }

            string sourceFolder = args[0];
            string replicaFolder = args[1];
            int interval;

            if (!int.TryParse(args[2], out interval) || interval <= 0)
            {
                Console.WriteLine("Invalid interval. Please enter a positive number.");
                return;
            }

            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"Error: Source folder '{sourceFolder}' does not exist.");
                return;
            }

            if (!Directory.Exists(replicaFolder))
            {
                Directory.CreateDirectory(replicaFolder);
                Console.WriteLine($"Created replica folder: {replicaFolder}");
            }

            Console.WriteLine($"Starting synchronization: {sourceFolder} -> {replicaFolder}");

            await RunSyncLoop(sourceFolder, replicaFolder, interval);
        }

        private static async Task RunSyncLoop(string source, string replica, int interval)
        {
            while (true)
            {
                try
                {
                    SyncFolders(source, replica);
                    Console.WriteLine($"Synchronization completed. Next run in {interval} seconds...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }

                await Task.Delay(interval * 1000);
            }
        }

        private static void SyncFolders(string source, string replica)
        {
            // Sync logic 
        }
    }
}
