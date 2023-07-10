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
            complainUsername = request.IdComplainNavigation,
            departmentName = request.IdDepartmentNavigation,
            handleUsername = request.IdHandleNavigation,
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
