using DuAnTruongTim.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnTruongTim.Services;

public class RequestServiceImpl : RequestService
{
    private CheckQlgiaoVuContext db;
    public RequestServiceImpl(CheckQlgiaoVuContext _Db)
    {
        db = _Db;
    }
    public async Task<dynamic> getAllRequest()
    {
        return await db.Requets.Select(request => new
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
           idComplainNavigation = new {
               request.IdComplainNavigation.Id,
               request.IdComplainNavigation.IdDepartment,
               request.IdComplainNavigation.Sex,
               request.IdComplainNavigation.Fullname,
               request.IdComplainNavigation.RequetIdHandleNavigations,
               request.IdComplainNavigation.RequetIdComplainNavigations,
               request.IdComplainNavigation.IdDepartmentNavigation,
               request.IdComplainNavigation.Academicrank,
               request.IdComplainNavigation.Address,
               request.IdComplainNavigation.Citizenidentification,
               request.IdComplainNavigation.Class,
               request.IdComplainNavigation.Dateofbirth,
               request.IdComplainNavigation.Degree,
               request.IdComplainNavigation.Emailaddress,
               request.IdComplainNavigation.IdRoleNavigation,
               request.IdComplainNavigation.IdRole,
               request.IdComplainNavigation.Role,
               request.IdComplainNavigation.IdRoleClaims,
               request.IdComplainNavigation.Password,
               request.IdComplainNavigation.Phonenumber,
               request.IdComplainNavigation.Schoolyear,
               request.IdComplainNavigation.Status,
               request.IdComplainNavigation.Username
           },
            idDepartmentNavigation = new {
                request.IdDepartmentNavigation.Id,
                request.IdDepartmentNavigation.TenDepartment,
                request.IdDepartmentNavigation.Describe,
                request.IdDepartmentNavigation.Address,
                request.IdDepartmentNavigation.Status,

            },
            idHandleNavigation = new {
                request.IdComplainNavigation.Id,
                request.IdComplainNavigation.IdDepartment,
                request.IdComplainNavigation.Sex,
                request.IdComplainNavigation.Fullname,
                request.IdComplainNavigation.RequetIdHandleNavigations,
                request.IdComplainNavigation.RequetIdComplainNavigations,
                request.IdComplainNavigation.IdDepartmentNavigation,
                request.IdComplainNavigation.Academicrank,
                request.IdComplainNavigation.Address,
                request.IdComplainNavigation.Citizenidentification,
                request.IdComplainNavigation.Class,
                request.IdComplainNavigation.Dateofbirth,
                request.IdComplainNavigation.Degree,
                request.IdComplainNavigation.Emailaddress,
                request.IdComplainNavigation.IdRoleNavigation,
                request.IdComplainNavigation.IdRole,
                request.IdComplainNavigation.Role,
                request.IdComplainNavigation.IdRoleClaims,
                request.IdComplainNavigation.Password,
                request.IdComplainNavigation.Phonenumber,
                request.IdComplainNavigation.Schoolyear,
                request.IdComplainNavigation.Status,
                request.IdComplainNavigation.Username
            },
            requestFiles = request.RequestFiles.Select(requestF => new
            {
                id = requestF.Id,
                name = requestF.Name,
                idRequest = requestF.IdRequest,
                idRequestNavigation = requestF.IdRequestNavigation,
            }),
            requestDetails = request.Requetsdetaileds.Select(requestDetail => new {
                id = requestDetail.Id,
                sentDate = requestDetail.Sentdate,
                payday = requestDetail.Payday,
                reason = requestDetail.Reason,
                status = requestDetail.Status,
                reply = requestDetail.Reply,
                idRequest = requestDetail.IdRequest,
                idRequestNavigation = requestDetail.IdRequestNavigation,
                
            })
        }).ToListAsync();
    }
}
