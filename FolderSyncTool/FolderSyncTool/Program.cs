using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FolderSyncConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourceFolder, replicaFolder;
            int interval;

            if (args.Length >= 3)  
            {
                sourceFolder = args[0];
                replicaFolder = args[1];

                if (!int.TryParse(args[2], out interval) || interval <= 0)
                {
                    Console.WriteLine("Invalid interval! Please enter a positive number.");
                    return;
                }
            }
            else  
            {
                Console.WriteLine("Enter the source folder path:");
                sourceFolder = Console.ReadLine();

                Console.WriteLine("Enter the replica folder path:");
                replicaFolder = Console.ReadLine();

                Console.WriteLine("Enter the synchronization interval (seconds):");
                while (!int.TryParse(Console.ReadLine(), out interval) || interval <= 0)
                {
                    Console.WriteLine("Invalid value! Please enter a positive number:");
                }
            }

            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"Error: The source folder '{sourceFolder}' does not exist.");
                return;
            }

            if (!Directory.Exists(replicaFolder))
            {
                Directory.CreateDirectory(replicaFolder);
                Console.WriteLine($"Created replica folder: {replicaFolder}");
            }

            Console.WriteLine($"Starting synchronization: {sourceFolder} → {replicaFolder}");
            await RunSyncLoop(sourceFolder, replicaFolder, interval);
        }

        private static async Task RunSyncLoop(string source, string replica, int interval)
        {
            while (true)
            {
                try
                {
                    SyncFolders(source, replica);
                    Console.WriteLine($"Synchronization completed. Next execution in {interval} seconds...");
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
