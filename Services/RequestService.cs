using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RequestService
{
    public dynamic getRequest();
    public dynamic getRequestById(int id);
    public bool createdRequest(Requet request);
    public bool createdRequestDetail(Requetsdetailed requestDetail);
    public bool updatedRequest(Requet request);
    public bool updatedRequestDetail(Requetsdetailed requestDetail);

    public dynamic getRequestDetail(int id);
}
