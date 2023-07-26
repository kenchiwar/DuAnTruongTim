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
using Microsoft.AspNetCore.Rewrite;
using Azure.Core;

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

        //GET: api/Requets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requet>>> GetRequets()
        {
          if (_context.Requets == null)
         {
              return NotFound();
          }
            return await _context.Requets.ToListAsync();
        }

        [HttpGet("getRequestIndex")]
        public async Task<ActionResult<IEnumerable<Requet>>> GetRequets_()
        {
            if (_context.Requets == null)
            {
                return NotFound();
            }
            return await _context.Requets.Where(re => re.IdHandle == null).ToListAsync();
        }

        [HttpGet("getRequestDetail")]
        public async Task<ActionResult<IEnumerable<Requetsdetailed>>> GetRequetsDetail()
        {
            if (_context.Requetsdetaileds == null)
            {
                return NotFound();
            }
            return await _context.Requetsdetaileds.ToListAsync();
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Requet>>> GetRequestFiles()
        //{
        //    var requestFiles = await _context.Requets.ToListAsync();
        //    return Ok(requestFiles);
        //}

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
        public async Task<IActionResult> PutRequet(int id, string strRequest)
        {
            var request = JsonConvert.DeserializeObject<Requet>(strRequest, new IsoDateTimeConverter
            {
                DateTimeFormat = "dd/MM/yyyy"
            });
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

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
        [HttpPost("createRequestWithFile")]
        public IActionResult CreateRequestWithFile(string strRequest, IFormFile file)
        {
            try
            {
                // Giải mã chuỗi JSON để lấy thông tin về yêu cầu (request)
                var request = JsonConvert.DeserializeObject<Requet>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                request.Sentdate = DateTime.Now;
                bool result = requestService.createdRequest(request);
                if (file != null)
                {
                    //var addRequest = _context.Requets.Add(request);

                    var acc = accountService.getAccountLogin();
                    Console.WriteLine(acc); 
                    // Xử lý tệp tin (file)
                    var fileName = FileHelper.generateFileName(file.FileName);
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "RequestFile", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    };
                    var requestFile = new RequestFile
                    {
                        IdRequest = request.Id,
                        Name = fileName
                    };
                    requestFileService.CreatedRequestFile(requestFile);
                    
                }
                var requestDetail = JsonConvert.DeserializeObject<Requetsdetailed>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });

                requestDetail.IdRequest = request.Id;
                requestDetail.Sentdate = DateTime.Now;
               bool result_ = requestService.createdRequestDetail(requestDetail);
                
                return Ok(new
                    {
                        Result = result,
                        Result_ = result_
                   });
            }
            catch
            {
                return BadRequest();
            }
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

    //[HttpPut("accept")]
    //public IActionResult Accept(int id, string updatedRequest)
    //{
    //    var request = JsonConvert.DeserializeObject<Requet>(updatedRequest, new IsoDateTimeConverter
    //    {
    //        DateTimeFormat = "dd/MM/yyyy"
    //    });

    //    var existingRequest = _cont

    //    if (existingRequest == null)
    //    {
    //        return NotFound();
    //    }
    //    // Update existing request with new values
    //    existingRequest.Property1 = updatedRequest.Property1;
    //    existingRequest.Property2 = updatedRequest.Property2;
    //    // ...

    //    _context.SaveChanges();

    //    return Ok();
    //}
}
