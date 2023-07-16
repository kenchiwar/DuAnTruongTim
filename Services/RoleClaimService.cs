using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RoleClaimService
{
    public Task<dynamic> getAllRoleClaim();
    public bool CreatedRoleClaim(RoleClaim roleClaim);
    public bool UpdatedRoleClaim(RoleClaim roleClaim);
}
