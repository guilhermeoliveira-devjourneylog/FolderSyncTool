﻿using System;
using System.Diagnostics;
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
                while (string.IsNullOrWhiteSpace(sourceFolder = Console.ReadLine()))
                {
                    Console.WriteLine("Invalid input! Please enter a valid source folder path:");
                }

                if (!EnsureSourceFolderExists(sourceFolder))
                {
                    return;
                }

                Console.WriteLine("Enter the replica folder path:");
                while (string.IsNullOrWhiteSpace(replicaFolder = Console.ReadLine()))
                {
                    Console.WriteLine("Invalid input! Please enter a valid replica folder path:");
                }

                if (!EnsureReplicaFolderExists(replicaFolder))
                {
                    return;
                }

                Console.WriteLine("Enter the synchronization interval (seconds):");
                while (!int.TryParse(Console.ReadLine(), out interval) || interval <= 0)
                {
                    Console.WriteLine("Invalid value! Please enter a positive number:");
                }
            }

            if (!EnsureSourceFolderExists(sourceFolder))
            {
                return;
            }

            if (!EnsureReplicaFolderExists(replicaFolder))
            {
                return;
            }

            Console.WriteLine($"Starting synchronization: {sourceFolder} → {replicaFolder}");
            await RunSyncLoop(sourceFolder, replicaFolder, interval);
        }

        private static bool EnsureSourceFolderExists(string sourceFolder)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"Error: The source folder '{sourceFolder}' does not exist.");
                return false;
            }

            return true;
        }

        private static bool EnsureReplicaFolderExists(string replicaFolder)
        {
            if (!Directory.Exists(replicaFolder))
            {
                Console.WriteLine($"Error: The replica folder '{replicaFolder}' does not exist.");
                return false;
            }

            return true;
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
            foreach (var file in Directory.GetFiles(source))
            {
                string destFile = Path.Combine(replica, Path.GetFileName(file));

                if (!File.Exists(destFile) || GetMD5(file) != GetMD5(destFile))
                {
                    File.Copy(file, destFile, true);
                    Console.WriteLine($"Copied/Updated: {file} -> {destFile}");
                }
            }

            foreach (var file in Directory.GetFiles(replica))
            {
                string sourceFile = Path.Combine(source, Path.GetFileName(file));
                if (!File.Exists(sourceFile))
                {
                    File.Delete(file);
                    Console.WriteLine($"Removed: {file}");
                }
            }
        }

        private static string GetMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
        }
    }
}

