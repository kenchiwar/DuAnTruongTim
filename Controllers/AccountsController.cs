using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnTruongTim.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DuAnTruongTim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
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

            return account;
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
        public  string GenerateRandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
    }
    
}
