using DuAnTruongTim.Helpers;
using DuAnTruongTim.Models;
using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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


    public string content(string content)
    {
        var content_ = "<div class=\"section-mail\">\r\n    <div class=\"box-mail\" style=\"max-width: 400px;\r\n    width: 100%;\r\n    background: #1c95fa;\r\n    text-align: center;\r\n    color: #000000;\r\n    padding: 50px 20px;\r\n    border-radius: 20px;\">\r\n        <div class=\"box-items\">\r\n            <div class=\"head\">\r\n\r\n                <h2 class=\"title\" style=\"font-size: 40px;\r\n                font-weight: 900;\">Notification</h2>\r\n            </div>\r\n            <div class=\"content\" style=\" background: #fff;\r\n            padding: 20px;\">\r\n                <h3>Vetify code</h3>\r\n                <div class=\"vetify-code\" style=\"display: block;\r\n                width: 50%;\r\n                margin: 0 auto;\r\n                font-size: 30px;\r\n                font-weight: 600;\">\r\n                    <p>"+content+"</p>\r\n                </div>\r\n            </div>\r\n            <div class=\"footer\">\r\n                <p class=\"name\" style=\"  font-size: 30px;\r\n                font-weight: 700;\r\n                color: #fff;\">TTTN University</p>\r\n                <div class=\"icon-social\" style=\"width: 100%;\r\n                display: flex;\r\n                justify-content: space-around;\">\r\n                    <a href=\"#\">\r\n                        <img src=\"https://cdn2.iconfinder.com/data/icons/social-media-2285/512/1_Facebook_colored_svg_copy-1024.png\" alt=\"\" style=\" width: 40px;\r\n                        height: 40px;\r\n                        filter: grayscale(1);\r\n                        transition: .3s;\">\r\n                    </a>\r\n                    <a href=\"#\">\r\n                        <img src=\"https://cdn2.iconfinder.com/data/icons/social-media-2285/512/1_Instagram_colored_svg_1-1024.png\" alt=\"\" style=\" width: 40px;\r\n                        height: 40px;\r\n                        filter: grayscale(1);\r\n                        transition: .3s;\">\r\n                    </a>\r\n                    <a href=\"#\">\r\n                        <img src=\"https://cdn2.iconfinder.com/data/icons/social-media-2285/512/1_Youtube_colored_svg-1024.png\" alt=\"\" style=\" width: 40px;\r\n                        height: 40px;\r\n                        filter: grayscale(1);\r\n                        transition: .3s;\">\r\n                    </a>\r\n                    <a href=\"#\">\r\n                        <img src=\"https://cdn2.iconfinder.com/data/icons/social-media-2285/512/1_Twitter3_colored_svg-1024.png\" alt=\"\" style=\" width: 40px;\r\n                        height: 40px;\r\n                       \r\n                        transition: .3s;\">\r\n                    </a>\r\n                </div>\r\n                <div class=\"copyright\">\r\n                    <p>Copyright © 2023 by TTTN-OHD. All rights reserved.</p>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>";

        return content_;
    }

    [HttpGet("sendChangPass/{username}")] 
     public async Task<ActionResult> sendChangPass(string username)
    {
      
        try
        {

            var data = GenerateRandomString(6).ToLower();

            if (ModelState.IsValid)
            {

                if (username != null)
                {
                    
                    var content__ = content(data);
                    var mailHelper = new MailHelper(configuration);
                    mailHelper.Send(configuration["Gmail:Username"], username, "Notification", content__);
                    return Ok(new { code = "Check your mail!" })
;                }
                ModelState.AddModelError("", "Deposit failed.");
            }
            //var account1 = accountservice.getaccountbyid(id);
            return Ok();
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
