using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public class RequestFileServiceImpl : RequestFileServicecs
{
    private CheckQlgiaoVuContext db;
    public RequestFileServiceImpl(CheckQlgiaoVuContext _db)
    {
        db = _db;
    }
    public bool CreatedRequestFile(RequestFile requestFile)
    {
        try
        {
            db.RequestFiles.Add(requestFile);
            return db.SaveChanges() >0;
        }
        catch
        {
            return false;
        }
        
    }

    public dynamic getRequestFile()
    {
        return db.RequestFiles.Select(re => new
        {
            id = re.Id,
            name = re.Name,
            idRequest = re.IdRequest,
        }).ToList();
    }
}
