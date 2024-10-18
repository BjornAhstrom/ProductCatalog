using Resources.Enums;

namespace Resources.Interfaces
{
    public interface IFileService
    {
        string GetFromFile();
        StatusCodes SaveToFile(string content);
    }
}