using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface DepartmentService
{
    public dynamic getAllDepartment();
    public dynamic getDepartmentById(int id);
    public bool CreatedDeparment(Department department);
    public bool UpdateDepartment(Department department);
}
