using Azure.Core;
using DuAnTruongTim.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnTruongTim.Services;

public class AccountServiceImpl : AccountService
{
    private CheckQlgiaoVuContext db;
    public AccountServiceImpl(CheckQlgiaoVuContext _db)
    {
        db = _db;
    }
    public async Task<dynamic> getAllAccount()
    {
        return await db.Accounts.Select(account => new
        {
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
            role = account.Role,
            idDepartmentNavigation = new
            {
                account.IdDepartmentNavigation.Id,
                account.IdDepartmentNavigation.TenDepartment,
                account.IdDepartmentNavigation.Describe,
                account.IdDepartmentNavigation.Address,
                account.IdDepartmentNavigation.Status,
            },
            idRoleNavigation = new {
                account.IdRoleNavigation.Id,
                account.IdRoleNavigation.Name,
                account.IdRoleNavigation.Describe,
            },
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
                complainUsername = requestComplainNavigation.IdComplainNavigation,
                departmentName = requestComplainNavigation.IdDepartmentNavigation,
                handleUsername = requestComplainNavigation.IdHandleNavigation,

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
                complainUsername = requestHandleNavigation.IdComplainNavigation,
                departmentName = requestHandleNavigation.IdDepartmentNavigation,
                handleUsername = requestHandleNavigation.IdHandleNavigation,
            }),
            idRoleClaim = account.IdRoleClaims.Select(idRoleClaim => new
            {
                id = idRoleClaim.Id,
                name = idRoleClaim.Name,
                describe = idRoleClaim.Describe,
                claim = idRoleClaim.Claim,
            }),

        }).ToListAsync();
    }
}
