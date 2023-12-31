﻿using DuAnTruongTim.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnTruongTim.Services;

public class RoleClaimServiceImpl : RoleClaimService
{
    private CheckQlgiaoVuContext db;
    public RoleClaimServiceImpl(CheckQlgiaoVuContext _db)
    {
        db = _db;
    }

    public bool CreatedRoleClaim(RoleClaim roleClaim)
    {
        try
        {
            db.RoleClaims.Add(roleClaim);
            return db.SaveChanges() >0;
        }
        catch { return false; }
    }

    public async Task<dynamic> getAllRoleClaim()
    {
        return await db.RoleClaims.Select(roleClaim => new
        {
            id = roleClaim.Id,
            name = roleClaim.Name,
            describe = roleClaim.Describe,
            claim = roleClaim.Claim,
            idAccount = roleClaim.IdAccounts.Select(account => new
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
            }),
        }).ToListAsync();
    }

    public dynamic GetRoleClaimById(int id)
    {
        return db.RoleClaims.Where(r => r.Id == id).Select(roleClaim => new
        {
            id = roleClaim.Id,
            name = roleClaim.Name,
            describe = roleClaim.Describe,
            claim = roleClaim.Claim,
            idAccount = roleClaim.IdAccounts.Select(account => new
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
            }),
        }).FirstOrDefault();
    }

    public bool UpdatedRoleClaim(RoleClaim roleClaim)
    {
        try
        {
            db.Entry(roleClaim).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch { return false; }
    }
}
