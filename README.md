# **FolderSyncTool 📂**
A tool for one-way folder synchronization, ensuring that a **replica folder** remains an identical copy of the **source folder**.

## **📖 Project Documentation**
The documentation for this project is available in the following locations:

- **FolderSyncTool Documentation:**  
  📄 [`FolderSyncTool/doc.md`](FolderSyncTool/doc.md)

- **FolderSyncTests Documentation:**  
  📄 [`FolderSyncTests/doc.md`](FolderSyncTests/doc.md)

## **🚀 Features**
✔ One-way synchronization from source to replica.  
✔ Periodic execution based on a configurable interval.  
✔ Logging system that records file operations.  
✔ Supports CLI and future UI implementation.  
✔ Log rotation mechanism for efficient log management.  

## **🔧 How to Use**
To run the **FolderSyncTool** from the command line:

```sh
dotnet run -- "C:\path\to\source" "C:\path\to\replica" 30
```
