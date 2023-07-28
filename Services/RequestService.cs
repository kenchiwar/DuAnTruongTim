using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RequestService
{
    public Task<dynamic> getAllRequest();
    public dynamic getRequest();
    public dynamic getRequestById(int id);
    //public dynamic getRequestOrderBy();
    public bool createdRequest(Requet request);
    public bool createdRequestDetail(Requetsdetailed requestDetail);
    public bool updatedRequest(Requet request);
}
