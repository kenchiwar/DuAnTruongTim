using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface RoleService
{
    public Task<dynamic> getAllRole();
    public bool createdRole(Role role);
    public bool updateRole(Role role);
}
