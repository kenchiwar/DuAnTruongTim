using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnTruongTim.Models;
using Newtonsoft.Json;
using DuAnTruongTim.Services;
using Newtonsoft.Json.Converters;
using DuAnTruongTim.Helpers;
using System.Diagnostics;

namespace DuAnTruongTim.Controllers
{
    [Route("api/Requets")]
    //[ApiController]
    public class RequetsController : ControllerBase
    {
        private readonly CheckQlgiaoVuContext _context;
        private RequestService requestService;
        private RequestFileServicecs requestFileService;
        private AccountService accountService;

        private IWebHostEnvironment webHostEnvironment;
        public RequetsController(
            CheckQlgiaoVuContext context,
            RequestService _requestService,
            RequestFileServicecs _requestFileService,
            AccountService _accountService,
            IWebHostEnvironment _webHostEnvironment
            )
        {
            _context = context;
            requestService = _requestService;
            requestFileService = _requestFileService;
            accountService = _accountService;
            webHostEnvironment = _webHostEnvironment;
        }

        // GET: api/Requets
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Requet>>> GetRequets()
        //{
        //  if (_context.Requets == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Requets.ToListAsync();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestFile>>> GetRequestFiles()
        {
            var requestFiles = await _context.RequestFiles.ToListAsync();
            return Ok(requestFiles);
        }

        // GET: api/Requets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requet>> GetRequet(int id)
        {
          if (_context.Requets == null)
          {
              return NotFound();
          }
            var requet = await _context.Requets.FindAsync(id);

            if (requet == null)
            {
                return NotFound();
            }

            return requet;
        }

        // PUT: api/Requets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequet(int id, Requet requet)
        {
            if (id != requet.Id)
            {
                return BadRequest();
            }

            _context.Entry(requet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequetExists(id))
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

        // POST: api/Requets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Requet>> PostRequet(Requet requet)
        {
          if (_context.Requets == null)
          {
              return Problem("Entity set 'CheckQlgiaoVuContext.Requets'  is null.");
          }
            _context.Requets.Add(requet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequet", new { id = requet.Id }, requet);
        }

        // DELETE: api/Requets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequet(int id)
        {
            if (_context.Requets == null)
            {
                return NotFound();
            }
            var requet = await _context.Requets.FindAsync(id);
            if (requet == null)
            {
                return NotFound();
            }

            _context.Requets.Remove(requet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequetExists(int id)
        {
            return (_context.Requets?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Produces("application/json")]
        [HttpGet("getRequest")]
        public IActionResult GetRequest()
        {
            try
            {
                return Ok(requestService.getRequest());
            }
            catch { return BadRequest(); }
        }

        [Produces("application/json")]
        [HttpGet("getRequestById/{id}")]
        public IActionResult GetRequestById(int id)
        {
            try
            {
                return Ok(requestService.getRequestById(id));
            }
            catch { return BadRequest(); }
        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("created")]
        public IActionResult CreatedRequest(string strRequest)
        {
            try
            {
                var request = JsonConvert.DeserializeObject<Requet>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                //var idAcc = accountService.getAccountLogin();
                //Debug.WriteLine(idAcc);
                request.Sentdate = DateTime.Now;
                //request.IdComplain = idAcc.Id;
                bool result = requestService.createdRequest(request);
                return Ok(new
                {
                    Result = result
                });
            }
            catch { return BadRequest(); }
        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("requestFile")]
        public IActionResult CreatedRequstFile(IFormFile name,string strRequestFile)
        {
            try
            {
                var fileName = FileHelper.generateFileName(name.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "RequestFile", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    name.CopyTo(fileStream);
                };
                var requestFile = JsonConvert.DeserializeObject<RequestFile>(strRequestFile, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                requestFile.IdRequest = 1;
                requestFile.Name = fileName;
                //Debug.WriteLine("ágagsagasgas");
                //Debug.WriteLine(fileName);
                bool result = requestFileService.CreatedRequestFile(requestFile);
                return Ok(new
                {
                    Result = result
                });
            }
            catch { return BadRequest(); }
        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPut("update")]
        public IActionResult UpdateRequst(string strRequest)
        {
            try
            {
                var request = JsonConvert.DeserializeObject<Requet>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                bool result = requestService.updatedRequest(request);
                return Ok(new
                {
                    Result = result
                });
            }
            catch { return BadRequest(); }
        }
    }
}
