using Resources.Enums;
using Resources.Interfaces;

namespace Resources.Services;

public class FileService : IFileService
{
    private readonly string _filePath = "";

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public StatusCodes SaveToFile(string content)
    {
        try
        {
            using var sw = new StreamWriter(_filePath);
            sw.Write(content);
            return StatusCodes.Success;
        }
        catch
        {
            return StatusCodes.Failed;
        }
    }

    public string GetFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                return sr.ReadToEnd();
            }

            return null!;
        }
        catch
        {
            return null!;
        }
    }
}
