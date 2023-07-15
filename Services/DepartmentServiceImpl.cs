using DuAnTruongTim.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnTruongTim.Services;

public class DepartmentServiceImpl : DepartmentService
{
    private CheckQlgiaoVuContext db;
    public DepartmentServiceImpl(CheckQlgiaoVuContext _db)
    {
        db = _db;
    }

    public bool CreatedDeparment(Department department)
    {
        try
        {
            db.Departments.Add(department);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic getAllDepartment()
    {
        return db.Departments.Select(department => new
        {
            id = department.Id,
            tenDepartment = department.TenDepartment,
            describe = department.Describe,
            address = department.Address,
            status = department.Status,
            //accounts = department.Accounts.Select(account => new
            //{
            //    id = account.Id,
            //    username = account.Username,
            //    password = account.Password,
            //    idRole = account.IdRole,
            //    idDepartment = account.IdDepartment,
            //    fullname = account.Fullname,
            //    emailaddress = account.Emailaddress,
            //    phonenumber = account.Phonenumber,
            //    address = account.Address,
            //    citizenidentification = account.Citizenidentification,
            //    dateofbirth = account.Dateofbirth,
            //    sex = account.Sex,
            //    status = account.Status,
            //    _class = account.Class,
            //    schoolyear = account.Schoolyear,
            //    degree = account.Degree,
            //    academicrank = account.Academicrank,
            //    role = account.IdRoleNavigation,
            //    department = account.IdDepartmentNavigation,
            //}),
            //requests = department.Requets.Select(request => new
            //{
            //    id = request.Id,
            //    idComplain = request.IdComplain,
            //    idDepartment = request.IdDepartment,
            //    idHandle = request.IdHandle,
            //    title = request.Title,
            //    status = request.Status,
            //    level = request.Level,
            //    sentDate = request.Sentdate,
            //    endDate = request.Enddate,
            //    priority = request.Priority,
            //    complainUsername = request.IdComplainNavigation,
            //    departmentName = request.IdDepartmentNavigation,
            //    handleUsername = request.IdHandleNavigation,
            //})
        }).ToList();
    }

    public dynamic getDepartmentById(int id)
    {
        return db.Departments.Where(de => de.Id == id).Select(department => new
        {
            id = department.Id,
            tenDepartment = department.TenDepartment,
            describe = department.Describe,
            address = department.Address,
            status = department.Status,
        }).FirstOrDefault();
    }

    public bool UpdateDepartment(Department department)
    {
        try
        {
            db.Entry(department).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
