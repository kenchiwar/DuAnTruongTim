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

    public dynamic getAccDepartmentById(int id)
    {
        return db.Accounts.Where(a => a.IdDepartment == id).Select(account => new {
            id = account.Id,
            username = account.Username,
            password = "",
            idRole = account.IdRole,
            idDepartment = account.IdDepartment,
            fullname = account.Fullname,
            emailaddress = account.Emailaddress,
            phonenumber = account.Phonenumber,
            address = account.Address,
            citizenidentification = account.Citizenidentification,
            dateofbirth = account.Dateofbirth,
            sex = account.Sex,
            status = account.Status,

            Class = account.Class,
            schoolyear = account.Schoolyear,
            degree = account.Degree,
            academicrank = account.Academicrank,
            role = account.Role,
            idDepartmentNavigation = new
            {
                account.IdDepartmentNavigation.Id,
                account.IdDepartmentNavigation.TenDepartment,
                account.IdDepartmentNavigation.Describe,
                account.IdDepartmentNavigation.Address,
                account.IdDepartmentNavigation.Status,
            },
            idRoleNavigation = new
            {
                account.IdRoleNavigation.Id,
                account.IdRoleNavigation.Name,
                account.IdRoleNavigation.Describe,
            },
            idRoleClaims = account.IdRoleClaims.Select(idRoleClaim => new
            {
                id = idRoleClaim.Id,
                name = idRoleClaim.Name,
                describe = idRoleClaim.Describe,
                claim = idRoleClaim.Claim,
            }),

            requetIdComplainNavigations = account.RequetIdComplainNavigations.Select(requestComplainNavigation => new
            {

                id = requestComplainNavigation.Id,
                idComplain = requestComplainNavigation.IdComplain,
                idDepartment = requestComplainNavigation.IdDepartment,
                idHandle = requestComplainNavigation.IdHandle,
                title = requestComplainNavigation.Title,
                status = requestComplainNavigation.Status,
                level = requestComplainNavigation.Level,
                sentDate = requestComplainNavigation.Sentdate,
                endDate = requestComplainNavigation.Enddate,
                priority = requestComplainNavigation.Priority,
                idDepartmentNavigation = new
                {
                    requestComplainNavigation.IdDepartmentNavigation.Id,
                    requestComplainNavigation.IdDepartmentNavigation.TenDepartment,
                    requestComplainNavigation.IdDepartmentNavigation.Describe,
                    requestComplainNavigation.IdDepartmentNavigation.Address,
                    requestComplainNavigation.IdDepartmentNavigation.Status,

                },


            }),
            requetIdHandleNavigations = account.RequetIdHandleNavigations.Select(requestHandleNavigation => new
            {
                id = requestHandleNavigation.Id,
                idComplain = requestHandleNavigation.IdComplain,
                idDepartment = requestHandleNavigation.IdDepartment,
                idHandle = requestHandleNavigation.IdHandle,
                title = requestHandleNavigation.Title,
                status = requestHandleNavigation.Status,
                level = requestHandleNavigation.Level,
                sentDate = requestHandleNavigation.Sentdate,
                endDate = requestHandleNavigation.Enddate,
                priority = requestHandleNavigation.Priority,
                idDepartmentNavigation = new
                {
                    requestHandleNavigation.IdDepartmentNavigation.Id,
                    requestHandleNavigation.IdDepartmentNavigation.TenDepartment,
                    requestHandleNavigation.IdDepartmentNavigation.Describe,
                    requestHandleNavigation.IdDepartmentNavigation.Address,
                    requestHandleNavigation.IdDepartmentNavigation.Status,

                },

            }),
        }).FirstOrDefault();
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
        return db.Departments.Where(department => department.Id == id).Select(department => new
        {
            id = department.Id,
            tenDepartment = department.TenDepartment,
            describe = department.Describe,
            address = department.Address,
            status = department.Status,
            accounts = department.Accounts.Select(account => new
            {
                id = account.Id,
                username = account.Username,
                password = account.Password,
                idRole = account.IdRole,
                fullname = account.Fullname,
                emailaddress = account.Emailaddress,
                phonenumber = account.Phonenumber,
                address = account.Address,
                citizenidentification = account.Citizenidentification,
                dateofbirth = account.Dateofbirth,
                sex = account.Sex,
                status = account.Status,
                _class = account.Class,
                schoolyear = account.Schoolyear,
                degree = account.Degree,
                academicrank = account.Academicrank,
               
            }),
            requests = department.Requets.Select(request => new
            {
                id = request.Id,
                idComplain = request.IdComplain,
                idDepartment = request.IdDepartment,
                idHandle = request.IdHandle,
                title = request.Title,
                status = request.Status,
                level = request.Level,
                sentDate = request.Sentdate,
                endDate = request.Enddate,
                priority = request.Priority,
            })
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
