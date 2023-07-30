using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RequestService
{
    public Task<dynamic> getAllRequest();
    public dynamic getRequest();
    public dynamic getRequestById(int id);
    public dynamic getFileById(int id);
    public dynamic getDetailById(int id);
    //public dynamic getRequestOrderBy();
    public bool createdRequest(Requet request);
    public bool createdRequestDetail(Requetsdetailed requestDetail);
    public bool updatedRequest(Requet request);
    public bool updatedRequestDetail(Requetsdetailed requestDetail);

    public dynamic getFileByIdDetail(int id);
}
