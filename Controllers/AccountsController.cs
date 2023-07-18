using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnTruongTim.Models;
using DuAnTruongTim.Services;

using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Http.HttpResults;
using DuAnTruongTim.Middleware;



namespace DuAnTruongTim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAccount]
    public class AccountsController : ControllerBase
    {
        private readonly CheckQlgiaoVuContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AccountService _accountService;
        public AccountsController(CheckQlgiaoVuContext context,IWebHostEnvironment webHostEnvironment,AccountService accountService)

        {
            _accountService = accountService;
            _context = context;

          

            _webHostEnvironment = webHostEnvironment;

        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _accountService.getAllAccount();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]

        public async Task<ActionResult<dynamic>> GetAccount(int id)
        {
            var accountLogin = _accountService.getAccountLogin();
            
            //Kiêm tra coi đủ phầm quyền ko 
           if (!(accountLogin.IdRole<=2 || accountLogin.Id==id))    return Unauthorized();
            

            
           var account = await _accountService.GetAccount(id);

            //if (account == null)
            //{
            //    return NotFound();
            //}

            return account!=null?Ok(account):NotFound();
        }
         [HttpGet("detail/{id}")]

        public async Task<ActionResult<dynamic>> GetAccountDetail(int id)
        {
            var accountLogin = _accountService.getAccountLogin();
            
            //Kiêm tra coi đủ phầm quyền ko 
           if (!(accountLogin.IdRole<=2))    return Unauthorized();
            

            
           var account = await _accountService.GetAccountDetail(id);

            //if (account == null)
            //{
            //    return NotFound();
            //}

            return account!=null?Ok(account):NotFound();
        }
        


        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
          [Consumes("multipart/form-data")]	
        [Produces("application/json")]
        public async Task<IActionResult> PutAccount(int id,[FromForm] string  dataAccount ,IFormFile? file,[FromForm] string dataRoleClaim)
        {
          
            try
            {
                var account = JsonConvert.DeserializeObject<Account>(dataAccount);
                       

                if (account != null)
                {
                    //Kiểm tra id với account có trùng nhau ko 
                       if (id != account.Id)    return Ok(new ResultApi(false, "Some thing wrong with your data "));
                       
                        var databaseAccount = await this._context.Accounts.Include(a=>a.IdRoleClaims).FirstOrDefaultAsync(a=>a.Id==id);
                    //Kiểm tra sự tồn tại của database 
                        if(databaseAccount == null)  return Ok(new ResultApi(false, "Account dosent 'esxist  "));
                   
                        var accountLogin = _accountService.getAccountLogin();
                      //Kiểm tra coi account có đủ phẩm quyền ko 
                    if(!(accountLogin.Id==id || accountLogin.IdRole<databaseAccount.IdRole)) return Ok(new ResultApi(false, "Account dosent 'esxist  "));
                    //Khúc này thì thêm thuộc tính mới vào 
                    databaseAccount.Fullname = account.Fullname;
                    ;databaseAccount.Username = account.Username
  
                    ;databaseAccount.IdRole = account.IdRole
                 ;databaseAccount.IdDepartment = account.IdDepartment

                 ;databaseAccount.Emailaddress = account.Emailaddress
                 ;databaseAccount.Phonenumber = account.Phonenumber
                 ;databaseAccount.Address = account.Address
   
                 ;   databaseAccount.Dateofbirth = account.Dateofbirth
                     ;databaseAccount.Sex = account.Sex
                         ;databaseAccount.Status = account.Status
                ;databaseAccount.Role = account.Role
                ;databaseAccount.Class = account.Class
                ;databaseAccount.Schoolyear= account.Schoolyear
                ;databaseAccount.Degree = account.Degree
                    ;databaseAccount.Academicrank = account.Academicrank;
                    //Kiểm tra database account pass word thôi mà 
                    if(account.Password!=null) databaseAccount.Password = BCrypt.Net.BCrypt.HashString(account.Password);
                    
                    if (file != null)
                    {
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


                    }
                    //coi có đủ thẩm quyền để thay đổi role claim ko đó mà 
                  if(accountLogin.IdRole==1 && account.IdRole<=2)
                    {
                        var roleClaim = JsonConvert.DeserializeObject<List<int>>(dataRoleClaim);
                        if(roleClaim.Count>0)
                        {
                            var databaseRoleClaim = await _context.RoleClaims.
                           Where(a =>
                            roleClaim.Contains(a.Id) 
                           ).ToListAsync();

                          
                            databaseAccount.IdRoleClaims = databaseRoleClaim;
                           
                           

                        }
                        else
                        {
                          databaseAccount.IdRoleClaims =  new List<RoleClaim>();
                        }
                       
                    }



                    _context.Accounts.Update(databaseAccount);

                    if (await _context.SaveChangesAsync() > 0) return Ok(new ResultApi(true, "Add new Account Success"));
                    return Ok(new ResultApi(true, "Add new Account Error "));
                }

            }
            catch (Exception)
            {
                return Ok(new ResultApi(false, "Add new Account Error "));
            }
            return Ok(new ResultApi(false,"Add new Account Error "));
            
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("multipart/form-data")]	
        [Produces("application/json")]
        public async Task<IActionResult> PostAccount([FromForm] string  dataAccount ,IFormFile? file)
        {
            //if (_context.Accounts == null)
            //{
            //    return Problem("Entity set 'CheckQlgiaoVuContext.Accounts'  is null.");
            //}
            //_context.Accounts.Add(account);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAccount", new { id = account.Id }, account);


             try
            {
                 var account =  JsonConvert.DeserializeObject<Account>(dataAccount);
              
            if(account!=null)
                {
                      if(file!= null)
                {
                    var  fileName = GenerateRandomString(10);
                        fileName = Path.Combine("account",fileName+Path.GetExtension(file.FileName));
                     
                      var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                        using var fileStream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(fileStream);


                         account.Citizenidentification = fileName;

               
                }
                      account.Password=BCrypt.Net.BCrypt.HashString("@123456");
                    _context.Accounts.Add(account);
                    if (await _context.SaveChangesAsync() > 0) return Ok(new ResultApi(true, "Add new Account Success"));
                    return Ok(new ResultApi(true, "Add new Account Error "));
                }
              
            }
            catch (Exception)
            {
                return Ok(new ResultApi(false,"Add new Account Error "));
            }
               return Ok(new ResultApi(false,"Add new Account Error "));
            

           // _context.Accounts.Add(account);
          
          //  return CreatedAtAction("GetAccount", new { id = account.Id }, account);

        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
         [Produces("application/json")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            var accountlogin = this._accountService.getAccountLogin();
            if(!((accountlogin?.IdRole??3)<account.IdRole)) return Unauthorized();
            account.Status=false;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return Ok("ffff");
        }
          [HttpGet("checkEmailExists/{account}/{id?}")]
     [Produces("application/json")]
        public async Task<IActionResult> checkEmailExist(string account ,int? id)
        {
            
            var result = id == null ? await _accountService.checkEmailExists(account, id) : await _accountService.checkEmailExists(account, id);
            return Ok(result);

            //return id==null?Ok(await _accountService.checkEmailExists(account,id)):Ok(_accountService.checkEmailExists(account,id));
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpGet("getAccount")]
        public IActionResult getAccount()
        {
            try
            {
                return Ok(_accountService.getAccount());
            }
            catch { return BadRequest(); }
        }

        public  string GenerateRandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }

    }
    
}
