using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RoleClaimService
{
    public Task<dynamic> getAllRoleClaim();
    public dynamic GetRoleClaimById(int id);
    public bool CreatedRoleClaim(RoleClaim roleClaim);
    public bool UpdatedRoleClaim(RoleClaim roleClaim);
}
