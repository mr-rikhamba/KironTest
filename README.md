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
   - Alternatively run in Vscode (on Mac) or Visual Studio (on Windows)

4. **Access the API**:
   - Open your browser and navigate to the following URL to view API information:
     ```
     http://localhost:5070/scalar/v1#tag/
     ```

### Notes
- If you encounter issues, ensure dependencies are restored correctly with `dotnet restore`.
- Coint stats api is no longer public, I registered an account and acquired an api key