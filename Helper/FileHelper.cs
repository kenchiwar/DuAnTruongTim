namespace DuAnTruongTim.Helpers;

public class FileHelper
{
   public static string generateFileName(string fileName)
    {
        var name = Guid.NewGuid().ToString().Replace("-", "");
        var lastIndexOf = fileName.LastIndexOf(".");
        var ext = fileName.Substring(lastIndexOf);
        return name + ext;
    }
}
