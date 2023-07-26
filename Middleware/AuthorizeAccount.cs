using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DuAnTruongTim.Middleware;

public class AuthorizeAccount : Attribute,IAuthorizationFilter
{
    
    //public AccountService AccountService { get; set; }
    public void OnAuthorization(AuthorizationFilterContext context )
    {
        //if (account.getAccountLogin() == null)
        //{
        //   context.Result = new UnauthorizedResult();;]
        //}
        string sessionId = context.HttpContext.Request.Headers["session-id"];

// Sử dụng sessionId để truy xuất thông tin phiên làm việc
     

    }
}

