# Application Setup Instructions

### Prerequisites
- Ensure you have **.NET 9** installed on your system.

### Steps to Run the Application

1. **Restore Dependencies**:
   - Run the following command in the root directory to restore required packages:
     ```bash
     dotnet restore
     ```

2. **Navigate to the KironTest Folder**:
   - Change directory to the `KironTest` subfolder:
     ```bash
     cd KironTest
     ```

3. **Run the API**:
   - Start the application by executing:
     ```bash
     dotnet run
     ```

4. **Access the API**:
   - Open your browser and navigate to the following URL to view API information:
     ```
     http://localhost:5070/scalar/
     ```

### Notes
- If you encounter issues, ensure dependencies are restored correctly with `dotnet restore`.