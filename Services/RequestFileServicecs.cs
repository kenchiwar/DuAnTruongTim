using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RequestFileServicecs
{
    public dynamic getRequestFile();
    public bool CreatedRequestFile(RequestFile requestFile);
}
