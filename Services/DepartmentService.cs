using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface DepartmentService
{
    public Task<dynamic> getAllDepartment();
    public bool CreatedDeparment(Department department);
}
