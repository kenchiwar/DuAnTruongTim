using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RequestService
{
    public Task<dynamic> getAllRequest();
    public bool createdRequest(Requet request);
    public bool updatedRequest(Requet request);
}
