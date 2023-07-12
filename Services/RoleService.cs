using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RoleService
{
    public Task<dynamic> getAllRole();
    public bool CreatedRole(Role role);
}
