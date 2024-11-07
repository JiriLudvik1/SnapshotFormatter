using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

int filesPrettified = 0;
int filesFailed = 0;
int filesSkipped = 0;

PrintInColor("Starting snapshot formatter", ConsoleColor.Cyan);

var snapFiles = GetSnapFilesInCurrentDirectory();
PrintInColor($"Found {snapFiles.Length} snap files", ConsoleColor.Cyan);

var tasks = snapFiles.Select(PrettyPrintJsonFile).ToList();
await Task.WhenAll(tasks);

Console.WriteLine();
PrintInColor("Finished:", ConsoleColor.Green);
PrintInColor($"Prettified: {filesPrettified} files", ConsoleColor.Green);
PrintInColor($"Skipped: {filesSkipped} files", ConsoleColor.Yellow);
PrintInColor($"Failed: {filesFailed} files", ConsoleColor.Red);

Console.ResetColor();
return;

static string[] GetSnapFilesInCurrentDirectory()
{
    var currentDirectory = Directory.GetCurrentDirectory();
    var snapFiles = Directory.GetFiles(currentDirectory, "*.snap");
    return snapFiles;
}

async Task PrettyPrintJsonFile(string filePath)
{
    try
    {
        var jsonContent = await File.ReadAllTextAsync(filePath);
        var parsedJson = JToken.Parse(jsonContent);
        var stringWriter = new StringWriter();
        var jsonTextWriter = new JsonTextWriter(stringWriter) 
        {
            Formatting = Formatting.Indented,
            Indentation = 2,
            IndentChar = ' '
        };
        parsedJson.WriteTo(jsonTextWriter);
        var prettyJson = stringWriter.ToString();

        if (jsonContent.Equals(prettyJson, StringComparison.Ordinal))
        {
            PrintInColor($"JSON content is already pretty printed. Skipping formatting. {filePath}", ConsoleColor.Yellow);
            filesSkipped++;
            return;
        }

        await File.WriteAllTextAsync(filePath, prettyJson);
        PrintInColor($"Formatted JSON written to {filePath}", ConsoleColor.Green);
        filesPrettified++;
    }
    catch (Exception ex)
    {
        PrintInColor($"Error processing file {filePath}: {ex.Message}", ConsoleColor.Red);
        filesFailed++;
    }
}

static void PrintInColor(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
}