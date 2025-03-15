using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FolderSyncTests
{
    [TestFixture]
    public class FolderSyncTests
    {
        private string _testSourceFolder;
        private string _testReplicaFolder;
        private string _testLogFile;

        [SetUp]
        public void Setup()
        {
            _testSourceFolder = Path.Combine(Path.GetTempPath(), "TestSource");
            _testReplicaFolder = Path.Combine(Path.GetTempPath(), "TestReplica");
            _testLogFile = Path.Combine(Path.GetTempPath(), "sync_log.txt");

            Directory.CreateDirectory(_testSourceFolder);
            Directory.CreateDirectory(_testReplicaFolder);

            if (File.Exists(_testLogFile))
            {
                File.Delete(_testLogFile);
            }
        }

        [TearDown]
        public void Cleanup()
        {
            Directory.Delete(_testSourceFolder, true);
            Directory.Delete(_testReplicaFolder, true);

            if (File.Exists(_testLogFile))
            {
                File.Delete(_testLogFile);
            }
        }

        [Test]
        public void EnsureSourceFolderExists_ShouldReturnFalse_WhenFolderDoesNotExist()
        {
            string nonExistentFolder = Path.Combine(Path.GetTempPath(), "NonExistentSource");
            bool result = FolderSyncConsole.Program.EnsureSourceFolderExists(nonExistentFolder);
            Assert.IsFalse(result);
        }

        [Test]
        public void EnsureSourceFolderExists_ShouldReturnTrue_WhenFolderExists()
        {
            bool result = FolderSyncConsole.Program.EnsureSourceFolderExists(_testSourceFolder);
            Assert.IsTrue(result);
        }

        [Test]
        public void EnsureReplicaFolderExists_ShouldReturnFalse_WhenFolderDoesNotExist()
        {
            string nonExistentFolder = Path.Combine(Path.GetTempPath(), "NonExistentReplica");
            bool result = FolderSyncConsole.Program.EnsureReplicaFolderExists(nonExistentFolder);
            Assert.IsFalse(result);
        }

        [Test]
        public void EnsureReplicaFolderExists_ShouldReturnTrue_WhenFolderExists()
        {
            bool result = FolderSyncConsole.Program.EnsureReplicaFolderExists(_testReplicaFolder);
            Assert.IsTrue(result);
        }

        [Test]
        public void LogEvent_ShouldCreateLogFile_WhenNotExist()
        {
            FolderSyncConsole.Program.LogEvent(_testLogFile, "Test log entry");
            Assert.IsTrue(File.Exists(_testLogFile));
        }

        [Test]
        public void LogEvent_ShouldAppendEntryToLog()
        {
            string testMessage = "Test log entry";
            FolderSyncConsole.Program.LogEvent(_testLogFile, testMessage);

            string logContent = File.ReadAllText(_testLogFile);
            Assert.IsTrue(logContent.Contains(testMessage));
        }

        [Test]
        public void SyncFolders_ShouldCopyNewFileToReplica()
        {
            string testFile = Path.Combine(_testSourceFolder, "test.txt");
            File.WriteAllText(testFile, "Test content");

            FolderSyncConsole.Program.SyncFolders(_testSourceFolder, _testReplicaFolder);

            string replicaFile = Path.Combine(_testReplicaFolder, "test.txt");
            Assert.IsTrue(File.Exists(replicaFile));
        }

        [Test]
        public void SyncFolders_ShouldUpdateModifiedFileInReplica()
        {
            string testFile = Path.Combine(_testSourceFolder, "test.txt");
            File.WriteAllText(testFile, "Initial content");

            FolderSyncConsole.Program.SyncFolders(_testSourceFolder, _testReplicaFolder);

            File.WriteAllText(testFile, "Updated content");
            FolderSyncConsole.Program.SyncFolders(_testSourceFolder, _testReplicaFolder);

            string replicaFile = Path.Combine(_testReplicaFolder, "test.txt");
            string content = File.ReadAllText(replicaFile);
            Assert.AreEqual("Updated content", content);
        }

        [Test]
        public void SyncFolders_ShouldRemoveDeletedFileFromReplica()
        {
            string testFile = Path.Combine(_testSourceFolder, "test.txt");
            File.WriteAllText(testFile, "Test content");

            FolderSyncConsole.Program.SyncFolders(_testSourceFolder, _testReplicaFolder);

            File.Delete(testFile);
            FolderSyncConsole.Program.SyncFolders(_testSourceFolder, _testReplicaFolder);

            string replicaFile = Path.Combine(_testReplicaFolder, "test.txt");
            Assert.IsFalse(File.Exists(replicaFile));
        }

        [Test]
        public async Task RotateLogFile_ShouldArchiveOldLogFile()
        {
            File.WriteAllText(_testLogFile, "Old log entry");
            File.SetLastWriteTime(_testLogFile, DateTime.Now.AddDays(-4));

            FolderSyncConsole.Program.RotateLogFile(_testLogFile);

            string[] logFiles = Directory.GetFiles(Path.GetDirectoryName(_testLogFile), "sync_log_*.txt");

            Assert.IsTrue(logFiles.Length > 0, "Old log file was not archived.");
            Assert.IsTrue(File.Exists(_testLogFile), "New log file was not created.");
        }
    }
}
