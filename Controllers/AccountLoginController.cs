using DuAnTruongTim.Helpers;
using DuAnTruongTim.Models;
using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Hosting;
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
    private IWebHostEnvironment _webHostEnvironment;
    private IConfiguration configuration;
    public AccountLoginController(
        AccountService accountService,
        CheckQlgiaoVuContext context,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration _configuration
        )
    {
        _accountService = accountService;
        configuration= _configuration;
        _context = context;
        _webHostEnvironment = webHostEnvironment;
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

    [ValidateAntiForgeryToken]
    [HttpGet("sendChangPass/{username}")] 
     public async Task<ActionResult> sendChangPass(string username)
    {
      
        try
        {
            var data = GenerateRandomString(6).ToLower();
            if(ModelState.IsValid)
            {
                return BadRequest();
            }
            var mailHeper = new MailHelper(configuration);
            mailHeper.Send(configuration["Gmail:Username"], username, "Notification", data);
            return Ok(new
            {
                code = data
            });
        }
        catch (Exception)
        {
            return NotFound();
        }
        // var dataLogin = _accountService.getAccountLogin();
           // var data = this.HttpContext.Items["account"];
       

    }
    [HttpGet("changePass/{username}/{password}")] 
     public async Task<ActionResult> changePass(string username ,string password)
    {
        

       return Ok(new ResultApi(await _accountService.changePass(username,password),"fssdfsf"));

    }
     [HttpPost("changeImage")] 
             [Consumes("multipart/form-data")]	
        [Produces("application/json")]
     public async Task<ActionResult> changeImage ([FromForm] IFormFile? file )

    {
        var account = _accountService.getAccountLogin();
        if(account == null) return Unauthorized();
        if (file != null)
                    {
            var databaseAccount = await _context.Accounts.FindAsync(account.Id); 
            if(databaseAccount == null) return NotFound();
                        var fileName = GenerateRandomString(10);
                        fileName = Path.Combine("account", fileName + Path.GetExtension(file.FileName));
                        //Nếu có đường dẫn thì xóa 
                        if (account.Citizenidentification != null)
            {
                            var filePath1 = Path.Combine(_webHostEnvironment.WebRootPath, databaseAccount.Citizenidentification ?? "abc");
                            using var stream = new FileStream(filePath1, FileMode.Open);

                            stream.Dispose();
            }

                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                        using var fileStream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(fileStream);
                        
                        //Nếu có file mói thì thêm đường dẫn mới vào 
                        databaseAccount.Citizenidentification = fileName;
                        _context.Accounts.Update(databaseAccount);
                        if(_context.SaveChanges()<=0) return NotFound();

              return Ok(filePath);    


         }



        return Ok();    

       //return Ok(new ResultApi(await _accountService.changePass(username,password),"fssdfsf"));

    }
    
       public  string GenerateRandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
          }
    
}
