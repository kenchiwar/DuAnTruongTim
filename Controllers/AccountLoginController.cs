using DuAnTruongTim.Models;
using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace DuAnTruongTim.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountLoginController : ControllerBase

{
        private readonly AccountService _accountService;
     private readonly CheckQlgiaoVuContext _context;
    public AccountLoginController(AccountService accountService,CheckQlgiaoVuContext context)
    {
        _accountService = accountService;

        _context = context;
    }
      [HttpGet]
        public async Task<ActionResult> GetAccounts()
          {
                return Ok("true");
         }
    [HttpGet("login/{username}/{password}")]
    public async Task<ActionResult<dynamic>> login(string username ,string password )
    {
        var account = await _accountService.login(username, password);
        // var dataLogin = _accountService.getAccountLogin();
           // var data = this.HttpContext.Items["account"];
        if (account == null) return Unauthorized();
        var accountLogin= _accountService.getAccountLogin();

        var chan = HttpContext.Items["account"];


        return Ok(account);

    }
    [HttpGet("sendChangPass/{username}")] 
     public async Task<ActionResult> sendChangPass(string username)
    {
      var data =GenerateRandomString(6).ToLower();
        try
        {

        }
        catch (Exception)
        {
return NotFound();
        }
        // var dataLogin = _accountService.getAccountLogin();
           // var data = this.HttpContext.Items["account"];
       return Ok(new
       {
           code = data 
       });

    }
    [HttpGet("changePass/{username}/{password}")] 
     public async Task<ActionResult> changePass(string username ,string password)
    {
        

       return Ok(new ResultApi(await _accountService.changePass(username,password),"fssdfsf"));

    }
       public  string GenerateRandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
          }
    
}
