using DuAnTruongTim.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace DuAnTruongTim.Services;

public class RoleServiceImpl : RoleService
{
    private CheckQlgiaoVuContext db;
    public RoleServiceImpl(CheckQlgiaoVuContext _Db)
    {
        db = _Db;
    }
    public async Task<dynamic> getAllRole()
    {
        return await db.Roles.Select(role => new
        {
            id = role.Id,
            name = role.Name,
            describe = role.Describe,

            accounts = role.Accounts.Select(account => new {
                id = account.Id,
                username = account.Username,
                password = account.Password,
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
                _class = account.Class,
                schoolyear = account.Schoolyear,
                degree = account.Degree,
                academicrank = account.Academicrank,
                role = account.IdRoleNavigation,
                department = account.IdDepartmentNavigation,
            }),
        }).ToListAsync();

    }
}
