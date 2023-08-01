using DuAnTruongTim.Models;
using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Newtonsoft.Json;


namespace DuAnTruongTim.Middleware;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class AddAccountLoginMiddleware 
{
    private readonly RequestDelegate _next;
   
    public AddAccountLoginMiddleware(RequestDelegate next)
    {
        _next = next;
      
    }

    public   async Task Invoke(HttpContext httpContext,AccountService _accountService)
    {
            
        var username = (string?)httpContext.Request.Headers["username"];
        var password = (string?)httpContext.Request.Headers["password"];
        if ((username != null && username!="") && (password != null&& password!=""))
        {
           var account = await _accountService.login(username, password);

           if (account != null) httpContext.Items["account"] = JsonConvert.SerializeObject(account);
            var acc = account;
           
          
        }

     

         await _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class AddAccountLoginMiddlewareExtensions
{
    public static IApplicationBuilder UseAddAccountLoginMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AddAccountLoginMiddleware>();
    }
}
