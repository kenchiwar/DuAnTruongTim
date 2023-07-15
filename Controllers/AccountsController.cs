using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnTruongTim.Models;
<<<<<<< HEAD
using DuAnTruongTim.Services;
=======
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using DuAnTruongTim.Middleware;

>>>>>>> 0ae4ab1522a832e53f74dd72f6baff50844bb15e

namespace DuAnTruongTim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly CheckQlgiaoVuContext _context;
<<<<<<< HEAD
        private AccountService accountService;

        public AccountsController(
            CheckQlgiaoVuContext context,
            AccountService _accountService
            )
=======
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AccountService _accountService;
        public AccountsController(CheckQlgiaoVuContext context,IWebHostEnvironment webHostEnvironment,AccountService accountService)
>>>>>>> 0ae4ab1522a832e53f74dd72f6baff50844bb15e
        {
            _accountService = accountService;
            _context = context;
<<<<<<< HEAD
            accountService= _accountService;
=======
            _webHostEnvironment = webHostEnvironment;
>>>>>>> 0ae4ab1522a832e53f74dd72f6baff50844bb15e
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
        [AuthorizeAccount]
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

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
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
<<<<<<< HEAD

        [HttpGet("getAccount")]
        public IActionResult getAccount()
        {
            try
            {
                return Ok(accountService.getAccount());
            }
            catch { return BadRequest(); }
        }
=======
        public  string GenerateRandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
>>>>>>> 0ae4ab1522a832e53f74dd72f6baff50844bb15e
    }
    
}
