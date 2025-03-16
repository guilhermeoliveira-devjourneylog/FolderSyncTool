# **Unit Test Cases**

## **Folder Existence Verification Tests**
These tests ensure that the program correctly detects whether the source and replica folders exist or not.

### **1. EnsureSourceFolderExists_ShouldReturnFalse_WhenFolderDoesNotExist**
**Description:** Tests if the `EnsureSourceFolderExists` function returns `false` when the source folder does not exist.

**Steps:**
1. Define a path for a non-existent folder.
2. Call `EnsureSourceFolderExists()`.
3. Verify that the return value is `false`.

### **2. EnsureSourceFolderExists_ShouldReturnTrue_WhenFolderExists**
**Description:** Tests if the `EnsureSourceFolderExists` function returns `true` when the source folder exists.

**Steps:**
1. Create the source folder.
2. Call `EnsureSourceFolderExists()`.
3. Verify that the return value is `true`.

### **3. EnsureReplicaFolderExists_ShouldReturnFalse_WhenFolderDoesNotExist**
**Description:** Tests if the `EnsureReplicaFolderExists` function returns `false` when the replica folder does not exist.

**Steps:**
1. Define a path for a non-existent folder.
2. Call `EnsureReplicaFolderExists()`.
3. Verify that the return value is `false`.

### **4. EnsureReplicaFolderExists_ShouldReturnTrue_WhenFolderExists**
**Description:** Tests if the `EnsureReplicaFolderExists` function returns `true` when the replica folder exists.

**Steps:**
1. Create the replica folder.
2. Call `EnsureReplicaFolderExists()`.
3. Verify that the return value is `true`.

---

## **Log Tests**
These tests ensure that the logging system functions correctly.

### **5. LogEvent_ShouldCreateLogFile_WhenNotExist**
**Description:** Tests if the `LogEvent` function creates a log file when it does not exist.

**Steps:**
1. Ensure the log file does not exist.
2. Call `LogEvent()`.
3. Verify that the log file has been created.

### **6. LogEvent_ShouldAppendEntryToLog**
**Description:** Tests if the `LogEvent` function correctly appends a new entry to the log file.

**Steps:**
1. Write a message to the log.
2. Read the log content.
3. Verify that the message is present in the log.

---

## **File Synchronization Tests**
These tests ensure that files are copied, updated, and removed correctly between the source and replica folders.

### **7. SyncFolders_ShouldCopyNewFileToReplica**
**Description:** Tests if a new file added to the source folder is copied to the replica folder.

**Steps:**
1. Create a file in the source folder.
2. Call `SyncFolders()`.
3. Verify that the file has been copied to the replica folder.

### **8. SyncFolders_ShouldUpdateModifiedFileInReplica**
**Description:** Tests if a modified file in the source folder is updated in the replica folder.

**Steps:**
1. Create an initial file in the source folder.
2. Execute `SyncFolders()`.
3. Modify the file in the source folder.
4. Execute `SyncFolders()` again.
5. Verify that the content of the file in the replica folder has been updated.

### **9. SyncFolders_ShouldRemoveDeletedFileFromReplica**
**Description:** Tests if a file deleted from the source folder is also removed from the replica folder.

**Steps:**
1. Create a file in the source folder.
2. Execute `SyncFolders()`.
3. Delete the file from the source folder.
4. Execute `SyncFolders()` again.
5. Verify that the file has been removed from the replica folder.

---

## **Log Rotation Test**
This test verifies whether the log rotation system works correctly.

### **10. RotateLogFile_ShouldArchiveOldLogFile**
**Description:** Tests if the `RotateLogFile` function correctly archives an old log and creates a new log file.

**Steps:**
1. Create an old log file (with a last modified date older than 3 days).
2. Call `RotateLogFile()`.
3. Verify that the old log file has been archived (e.g., `sync_log_YYYYMMDDHHMMSS.txt`).
4. Verify that a new log file has been created (`sync_log.txt`).
