using Azure.Core;
using DuAnTruongTim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DuAnTruongTim.Services;

public class AccountServiceImpl : AccountService
{
    private CheckQlgiaoVuContext db;
    public AccountServiceImpl(CheckQlgiaoVuContext _db)
    {
        db = _db;
    }

    public dynamic getAccount()
    {
        return db.Accounts.Select(account => new
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
        }).ToList();
    }

    public async Task<dynamic> getAllAccount()
    {
        var account = await db.Accounts.AsNoTracking().Include(a=>a.IdDepartmentNavigation).Include(a=>a.IdRoleNavigation).ToListAsync<Account>();
   
        return this.GetDynamicList(account) ; 
        //return await db.Accounts.AsNoTracking().Include(a=>a.IdDepartmentNavigation).Include(a=>a.IdRoleNavigation).Select(account => new
        //{
        //    id = account.Id,
        //    username = account.Username,
        //    password ="",
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

        //    Class =account.Class,
        //    schoolyear = account.Schoolyear,
        //    degree = account.Degree,
        //    academicrank = account.Academicrank,
        //    role = account.Role,
        //    idDepartmentNavigation = new
        //    {
        //        account.IdDepartmentNavigation.Id,
        //        account.IdDepartmentNavigation.TenDepartment,
        //        account.IdDepartmentNavigation.Describe,
        //        account.IdDepartmentNavigation.Address,
        //        account.IdDepartmentNavigation.Status,
        //    },
        //    idRoleNavigation = new {
        //        account.IdRoleNavigation.Id,
        //        account.IdRoleNavigation.Name,
        //        account.IdRoleNavigation.Describe,
        //    },
        //    //requetIdComplainNavigations = account.RequetIdComplainNavigations.Select(requestComplainNavigation => new
        //    //{
        
        //    //    id = requestComplainNavigation.Id,
        //    //    idComplain = requestComplainNavigation.IdComplain,
        //    //    idDepartment = requestComplainNavigation.IdDepartment,
        //    //    idHandle = requestComplainNavigation.IdHandle,
        //    //    title = requestComplainNavigation.Title,
        //    //    status = requestComplainNavigation.Status,
        //    //    level = requestComplainNavigation.Level,
        //    //    sentDate = requestComplainNavigation.Sentdate,
        //    //    endDate = requestComplainNavigation.Enddate,
        //    //    priority = requestComplainNavigation.Priority,
        //    //    complainUsername = requestComplainNavigation.IdComplainNavigation,
        //    //    departmentName = requestComplainNavigation.IdDepartmentNavigation,
        //    //    handleUsername = requestComplainNavigation.IdHandleNavigation,

        //    //}),
        //    //requetIdHandleNavigations = account.RequetIdHandleNavigations.Select(requestHandleNavigation => new
        //    //{
        //    //    id = requestHandleNavigation.Id,
        //    //    idComplain = requestHandleNavigation.IdComplain,
        //    //    idDepartment = requestHandleNavigation.IdDepartment,
        //    //    idHandle = requestHandleNavigation.IdHandle,
        //    //    title = requestHandleNavigation.Title,
        //    //    status = requestHandleNavigation.Status,
        //    //    level = requestHandleNavigation.Level,
        //    //    sentDate = requestHandleNavigation.Sentdate,
        //    //    endDate = requestHandleNavigation.Enddate,
        //    //    priority = requestHandleNavigation.Priority,
        //    //    complainUsername = requestHandleNavigation.IdComplainNavigation,
        //    //    departmentName = requestHandleNavigation.IdDepartmentNavigation,
        //    //    handleUsername = requestHandleNavigation.IdHandleNavigation,
        //    //}),
        //    //idRoleClaim = account.IdRoleClaims.Select(idRoleClaim => new
        //    //{
        //    //    id = idRoleClaim.Id,
        //    //    name = idRoleClaim.Name,
        //    //    describe = idRoleClaim.Describe,
        //    //    claim = idRoleClaim.Claim,
        //    //}),

        //}).ToListAsync();
    }
    
    public async Task<bool> checkEmailExists(string email,int? id)

    {
        
        return id==null?await db.Accounts.AnyAsync(account=>account.Username.Equals(email) )
            :await db.Accounts.AnyAsync(account=>account.Username.Equals(email) && account.Id!=id) ;
    }

    public dynamic GetDynamic(Account? account)
    {
        if (account == null)
        {
            return null ; 
        }
        return new
        {
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
            idRoleClaim = account.IdRoleClaims.Select(idRoleClaim => new
            {
                id = idRoleClaim.Id,
                name = idRoleClaim.Name,
                describe = idRoleClaim.Describe,
                claim = idRoleClaim.Claim,
            }),
        };
       //return account.(account => new
       // {
       //     id = account.Id,
       //     username = account.Username,
       //     password ="",
       //     idRole = account.IdRole,
       //     idDepartment = account.IdDepartment,
       //     fullname = account.Fullname,
       //     emailaddress = account.Emailaddress,
       //     phonenumber = account.Phonenumber,
       //     address = account.Address,
       //     citizenidentification = account.Citizenidentification,
       //     dateofbirth = account.Dateofbirth,
       //     sex = account.Sex,
       //     status = account.Status,

       //     Class =account.Class,
       //     schoolyear = account.Schoolyear,
       //     degree = account.Degree,
       //     academicrank = account.Academicrank,
       //     role = account.Role,
       //     idDepartmentNavigation = new
       //     {
       //         account.IdDepartmentNavigation.Id,
       //         account.IdDepartmentNavigation.TenDepartment,
       //         account.IdDepartmentNavigation.Describe,
       //         account.IdDepartmentNavigation.Address,
       //         account.IdDepartmentNavigation.Status,
       //     },
       //     idRoleNavigation = new {
       //         account.IdRoleNavigation.Id,
       //         account.IdRoleNavigation.Name,
       //         account.IdRoleNavigation.Describe,
       //     }, 
       //});
    
    }

    public dynamic GetDynamicDetail(Account? account)
    {
        throw new NotImplementedException();
    }

    public dynamic GetDynamicList(List<Account?> DataAccount)

    {
        if(DataAccount == null) DataAccount = new List<Account?>();
       return DataAccount.Select(account => new
        {
            id = account.Id,
            username = account.Username,
            password ="",
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

            Class =account.Class,
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
            //requetIdComplainNavigations = account.RequetIdComplainNavigations.Select(requestComplainNavigation => new
            //{
        
            //    id = requestComplainNavigation.Id,
            //    idComplain = requestComplainNavigation.IdComplain,
            //    idDepartment = requestComplainNavigation.IdDepartment,
            //    idHandle = requestComplainNavigation.IdHandle,
            //    title = requestComplainNavigation.Title,
            //    status = requestComplainNavigation.Status,
            //    level = requestComplainNavigation.Level,
            //    sentDate = requestComplainNavigation.Sentdate,
            //    endDate = requestComplainNavigation.Enddate,
            //    priority = requestComplainNavigation.Priority,
            //    complainUsername = requestComplainNavigation.IdComplainNavigation,
            //    departmentName = requestComplainNavigation.IdDepartmentNavigation,
            //    handleUsername = requestComplainNavigation.IdHandleNavigation,

            //}),
            //requetIdHandleNavigations = account.RequetIdHandleNavigations.Select(requestHandleNavigation => new
            //{
            //    id = requestHandleNavigation.Id,
            //    idComplain = requestHandleNavigation.IdComplain,
            //    idDepartment = requestHandleNavigation.IdDepartment,
            //    idHandle = requestHandleNavigation.IdHandle,
            //    title = requestHandleNavigation.Title,
            //    status = requestHandleNavigation.Status,
            //    level = requestHandleNavigation.Level,
            //    sentDate = requestHandleNavigation.Sentdate,
            //    endDate = requestHandleNavigation.Enddate,
            //    priority = requestHandleNavigation.Priority,
            //    complainUsername = requestHandleNavigation.IdComplainNavigation,
            //    departmentName = requestHandleNavigation.IdDepartmentNavigation,
            //    handleUsername = requestHandleNavigation.IdHandleNavigation,
            //}),
            //idRoleClaim = account.IdRoleClaims.Select(idRoleClaim => new
            //{
            //    id = idRoleClaim.Id,
            //    name = idRoleClaim.Name,
            //    describe = idRoleClaim.Describe,
            //    claim = idRoleClaim.Claim,
            //}),

        }).ToList();
    }
    public Account? getAccountLogin()
    {
        var account = new Account();
        account.Id=3;
        account.IdRole=1;
        account.Username = "Fadasdad1234@gmail.com";
       
        return account; 
    }
    public async Task<dynamic> GetAccount(int Id)
    {
        var account = await db.Accounts.Include(a=>a.IdRoleNavigation).Include(a=>a.IdDepartmentNavigation).Include(a=>a.IdRoleClaims).AsNoTracking().FirstOrDefaultAsync(x=>x.Id == Id);
      return this.GetDynamic(account) ;

         //throw new NotImplementedException();
    }
    

}
