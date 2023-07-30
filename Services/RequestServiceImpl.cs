using DuAnTruongTim.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DuAnTruongTim.Services;



public class RequestServiceImpl : RequestService
{
    private CheckQlgiaoVuContext db;
    private IConfiguration configuration;
    public RequestServiceImpl(CheckQlgiaoVuContext _Db, IConfiguration _configuration)
    {
        db = _Db;
        configuration = _configuration;
    }

    public bool createdRequest(Requet request)
    {
        try
        {
           
            db.Requets.Add(request);
            return db.SaveChanges() > 0;
        }
        catch { return false; }
    }

    public bool createdRequestDetail(Requetsdetailed requestDetail)
    {
        try
        {
            db.Requetsdetaileds.Add(requestDetail);
            return db.SaveChanges() > 0;
        }
        catch { return false; }
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

    public dynamic getDetailById(int id)
    {
        return db.Requetsdetaileds.Where(re => re.IdRequest == id).Select(request => new
        {
            id = request.Id,
            sentDate = request.Sentdate,
            payday = request.Payday,
            reason = request.Reason,
            status = request.Status,
            reply = request.Reply,
            idRequest = request.IdRequest
        }).FirstOrDefault();
    }

   

    public dynamic getFileById(int id)
    {
        return db.RequestFiles.Where(idR => idR.IdRequest == id).Select(request => new
        {
            file = request.Name,
            name = configuration["BaseUrl"] + "RequestFile/" + request.Name,
        }).ToList();
    }

    public dynamic getFileByIdDetail(int id)
    {
        return db.Requetsdetaileds.Where(re => re.IdRequest == id).Select(request => new
        {
            id = request.Id,
            sentDate = request.Sentdate,
            payday = request.Payday,
            reason = request.Reason,
            status = request.Status,
            reply = request.Reply,
            idRequest = request.IdRequest
        }).ToList();
    }

    public dynamic getRequest()
    {
        return db.Requets
            .Include(a=>a.IdComplainNavigation)
            .Include(a=>a.IdDepartmentNavigation)
            .Include(a=>a.IdHandleNavigation)
            .Include(a=>a.RequestFiles)
            .Include(a=>a.Requetsdetaileds)
            .Select(request => new
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
            idComplainNavigation = new
            {
                request.IdComplainNavigation.Id,
                request.IdComplainNavigation.Fullname,
                request.IdComplainNavigation.Username,
            },
            idDepartmentNavigation = new
            {
                request.IdDepartmentNavigation.Id,
                request.IdDepartmentNavigation.TenDepartment,
                request.IdDepartmentNavigation.Describe,
                request.IdDepartmentNavigation.Address,
                request.IdDepartmentNavigation.Status,

            },
            idHandleNavigation = new
            {
                request.IdComplainNavigation.Id,
               
                request.IdComplainNavigation.Fullname,
              
              
                request.IdComplainNavigation.Academicrank,
                request.IdComplainNavigation.Address,
                request.IdComplainNavigation.Citizenidentification,
                request.IdComplainNavigation.Class,
                request.IdComplainNavigation.Dateofbirth,
                request.IdComplainNavigation.Degree,
                request.IdComplainNavigation.Emailaddress,
            
                request.IdComplainNavigation.IdRole,
                request.IdComplainNavigation.Role,
              
           
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
           
            }),
            requestDetails = request.Requetsdetaileds.Select(requestDetail => new {
                id = requestDetail.Id,
                sentDate = requestDetail.Sentdate,
                payday = requestDetail.Payday,
                reason = requestDetail.Reason,
                status = requestDetail.Status,
                reply = requestDetail.Reply,
                idRequest = requestDetail.IdRequest,
              

            })
        }).ToList<dynamic>();
    }

    public dynamic getRequestById(int id)
    {
        return db.Requets.Where(request => request.Id == id).Select(request => new
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
            idComplainNavigation = new
            {
                request.IdComplainNavigation.Id,
                request.IdComplainNavigation.Fullname,
                request.IdComplainNavigation.Username,
            },
            idDepartmentNavigation = new
            {
                request.IdDepartmentNavigation.Id,
                request.IdDepartmentNavigation.TenDepartment,
                request.IdDepartmentNavigation.Describe,
                request.IdDepartmentNavigation.Address,
                request.IdDepartmentNavigation.Status,

            },
            idHandleNavigation = new
            {
                request.IdComplainNavigation.Id,
                request.IdComplainNavigation.Fullname,
                request.IdComplainNavigation.Academicrank,
                request.IdComplainNavigation.Address,
                request.IdComplainNavigation.Citizenidentification,
                request.IdComplainNavigation.Class,
                request.IdComplainNavigation.Dateofbirth,
                request.IdComplainNavigation.Degree,
                request.IdComplainNavigation.Emailaddress,
                request.IdComplainNavigation.IdRole,
                request.IdComplainNavigation.Role,
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

            }),
            requestDetails = request.Requetsdetaileds.Select(requestDetail => new {
                id = requestDetail.Id,
                sentDate = requestDetail.Sentdate,
                payday = requestDetail.Payday,
                reason = requestDetail.Reason,
                status = requestDetail.Status,
                reply = requestDetail.Reply,
                idRequest = requestDetail.IdRequest,


            })
        }).FirstOrDefault();
    }


   

    public bool updatedRequest(Requet request)
    {
        try
        {
            db.Entry(request).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() >0;
        }
        catch
        {
            return false;
        }
    }

    public bool updatedRequestDetail(Requetsdetailed requestDetail)
    {
        try
        {
            db.Entry(requestDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch { return false; }
    }
}
