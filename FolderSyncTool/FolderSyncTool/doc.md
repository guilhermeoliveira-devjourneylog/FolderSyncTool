# **Double Diamond FolderSyncTool**

The **Double Diamond methodology** is a user-centered design model divided into four main phases:

- **Discover** – Research and understanding of the problem.  
- **Define** – Filtering ideas and defining the problem.  
- **Develop** – Generating and testing solutions.  
- **Deliver** – Final implementation and refinements.  

---

## **1. Discover - Understanding the Problem**

### **Research and Problem Identification**
- Companies need a **reliable system** to **automatically sync files**.  
- Non-technical users **struggle** with **manual synchronization**.  
- Existing tools can be **expensive, slow, or complicated**.  

### **User Interviews and Feedback**
- We asked users about **their difficulties** in managing backups and synchronization.  
- We identified that **visible logs** and an **intuitive interface** are essential.  

### **Benchmarking**
- **OneDrive and Google Drive** offer cloud synchronization but require an **internet connection**.  
- Tools like **Robocopy and rsync** are **powerful** but difficult for **non-technical users**.  

### **Conclusion**
We need a **simple, reliable, and accessible tool** to sync local folders **without requiring technical knowledge**.  

---

## **2. Define - Refining the Problem**

### **Problem Definition**
Users need a **simple and reliable tool** to **automatically sync folders** without requiring the internet or complex configurations.  

### **Key Requirements**
**One-way synchronization** (source → replica).  
**Easy setup**, allowing users to **choose folders and sync intervals**.  
**Visible logs** in the UI to track operations.  
**Automatic execution** without user intervention.  
**User-friendly GUI** (WinForms UI).  

### **Quick Prototype**
We created a UI draft to validate the user experience:

- **Textboxes** to select folders.  
- **Button** to start/stop synchronization.  
- **RichTextBox** to display logs in real time.  

---

## **3. Develop - Implementation and Testing**

### **Development**
#### **Implemented CLI Version**
Automatic synchronization via **command line**.  
**Logs stored in a file** for tracking.  

#### **Implemented UI Version (WinForms)**
User-friendly **interface**.  
Buttons to **start/stop synchronization**.  
**Real-time logs** displayed on screen.  

### **Testing and Iterations**
✔ **Performance Testing:** Validated synchronization with **thousands of files**.  
✔ **Stability Testing:** Ran the program for **hours** to detect failures.  
✔ **User Experience (UX) Testing:** Gathered **user feedback** and improved the interface.  

---

## **4. Deliver - Final Refinement and Deployment**

### **Final Optimizations**
- **Implemented multi-threading** to prevent UI freezing.  
- **Improved error handling**, such as folder permission issues.  
- **Added a button** to stop synchronization.  

### **Deployment and Distribution**
- **Packaged the app** as a **standalone Windows installer (.exe)**.  
- **Published source code** on **GitHub**.  
- **Documented installation and usage instructions**.  

### **Future Updates**
- **Network-based synchronization** support.  
- **Pause and resume sync** option.  
- **Dark mode** for the UI.  

---

# **Waterfall FolderSyncTool**

## **1. Requirements**
### **Project Objective**
Develop a program that **synchronizes two folders** (**source → replica**).  

### **Core Requirements**
- **One-way synchronization** between folders.  
- **Periodic execution** based on a user-defined interval.  
- **Detailed logging** of all operations (copying, deletion, etc.).  
- **Command-line interface (CLI) and graphical user interface (GUI).**  
- **Developed in C#** without external folder sync libraries.  

---

## **2. Design and Architecture**
### **UML Diagram**
The system is **structured into layers**:

- **User Interface Layer** → **WinForms** for folder selection and sync control.  
- **Business Logic Layer** → Implements synchronization logic.  
- **Data Layer** → Manages logs and persistent storage.  

---

## **3. Implementation**
### **CLI Version**
- **Terminal-based** execution using **command-line arguments**.  
- **Asynchronous processing** using `Task.Run()` for efficiency.  

### **GUI Version**
- **WinForms application** for user-friendly operation.  
- **Real-time logging** displayed in the UI.  
- **Start/Stop button** for sync control.  

---

## **4. Testing and Validation**
### **Planned Test Cases**
- **Basic Sync Test** → Ensure files **are copied** correctly.  
- **File Update Test** → Ensure **modified files** are replaced.  
- **File Deletion Test** → Ensure **deleted files** in `source` are **removed** from `replica`.  
- **Input Validation Test** → Test with **invalid paths and non-existent folders**.  
- **Performance Test** → Sync with **large volumes of data**.  

### **Tests Conducted**
- **Continuous execution test** at regular intervals.  
- **Log validation** to confirm correct logging of sync events.  
- **Stress test** with **thousands of files** to optimize performance.  

---

## **5. Deployment and Maintenance**
### **Packaging**
- **Standalone `.exe` package** for Windows.  

### **Deployment**
- **Installer created** using **Inno Setup**.  
- **Published on GitHub** for version control.  

### **Maintenance**
- **Log system implemented** for monitoring.  
- **Planned updates** (e.g., pause/resume sync, cloud integration).  

---

# **How to Run and Test the Program**
## **1. Build the Project**
If using **Visual Studio 2022**, compile the project:

- **Option 1:** Click **Build → Build Solution** (`Ctrl + Shift + B`).  
- **Option 2:** Open the **terminal** in the project directory and run:  

```sh
  dotnet build
```
This generates an executable in bin/Debug/net8.0/.

## **2. Run the Program**

You can run the program in two ways:

- With parameters (automatically syncs without user input).
- Interactive mode (asks for paths if no parameters are provided).

Run via Terminal

Windows (cmd or PowerShell):

```sh
bin\Debug\net8.0\FolderSyncConsole.exe "C:\Users\MyUser\sourceFolder" "C:\Users\MyUser\replicaFolder" 30
```
Linux/macOS:

```sh
./bin/Debug/net8.0/FolderSyncConsole "/home/user/sourceFolder" "/home/user/replicaFolder" 30
```
Run in Interactive Mode

```sh
dotnet run 
```
## **3. Testing the Synchronization Process**

- Add a new file → It should be copied to replicaFolder.\
- Modify a file → It should be updated in replicaFolder.\
- Delete a file → It should be removed from replicaFolder.\
- Stop sync by pressing Ctrl + C.

## **4. Check the Logs**
```sh
notepad sync_log.txt  # Windows
cat sync_log.txt      # Linux/macOS
```
Show last 20 log entries:
```sh
tail -n 20 sync_log.txt
```

