using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DuAnTruongTim.Middleware;

public class AuthorizeAccount : Attribute, IAuthorizationFilter
{
    //private readonly AccountService _accountService;
    //public AuthorizeAccount(AccountService accountService)
    //{
    //    _accountService = accountService;
    //}
    //public AccountService AccountService { get; set; }
    public void OnAuthorization(AuthorizationFilterContext context )
    {
        //if (account.getAccountLogin() == null)
        //{
        //   context.Result = new UnauthorizedResult();;
        //}

    }
}
