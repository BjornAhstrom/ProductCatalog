using Resources.Enums;

namespace Resources.Services
{
    public interface IFileService
    {
        string GetFromFile();
        StatusCodes SaveToFile(string content);
    }
}