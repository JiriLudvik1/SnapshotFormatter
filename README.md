# Snapshot Formatter

Snapshot Formatter is a simple application to format JSON snapshot files. The application scans the current directory for `.snap` files and pretty-prints the JSON content.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.

## Building the Application

Follow these steps to build the Snapshot Formatter:

1. **Navigate to the Project Directory**:
   Open a terminal or command prompt and navigate to the directory containing your C# project (`.csproj`).

   ```sh
   cd path\to\your\project
   ```

2. **Publish the Application**:
   Run the following command to publish the application as a single file for `win-x64`.

   ```sh
   dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true
   ```

## Deploying the Application

add it to your path. you'll figure that out
