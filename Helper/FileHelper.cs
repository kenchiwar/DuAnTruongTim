using DuAnTruongTim.Services;

namespace DuAnTruongTim.Helpers;

public class FileHelper
{
    private AccountService accountService;
    public FileHelper(AccountService _accountService) { accountService = _accountService; }
    public static string generateFileName(string fileName)
    {
        var name = Guid.NewGuid().ToString().Replace("-", "");
        var lastIndexOf = fileName.LastIndexOf(".");
        var ext = fileName.Substring(lastIndexOf);
        
        return name+ ext;
    }
}
