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
            role = account.IdRoleNavigation,
            department = account.IdDepartmentNavigation,
            requetIdComplainNavigations = account.RequetIdComplainNavigations.Select(request => new
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
                complainUsername = request.IdComplainNavigation,
                departmentName = request.IdDepartmentNavigation,
                handleUsername = request.IdHandleNavigation,

            })
        }).ToListAsync();
    }
}
