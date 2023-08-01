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
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Hosting;

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
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRequet(int id, Requet request)
        //{
        //    //var request = JsonConvert.DeserializeObject<Requet>(strRequest, new IsoDateTimeConverter
        //    //{
        //    //    DateTimeFormat = "dd/MM/yyyy"
        //    //});
        //    if (id != request.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(request).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RequetExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, Requet strRequest, Requetsdetailed requestDetail_)
        {
            try
            {
                var request = await _context.Requets.FindAsync(id);
                var requestDetail = await _context.Requetsdetaileds.FirstOrDefaultAsync(r => r.IdRequest == id);
                //Debug.WriteLine(requestDetail);
                if (request == null)
                {
                    return NotFound();
                }
                var idHandel = accountService.getAccountLogin();
                if (idHandel == null)
                {
                    return NotFound();
                }
                
                request.IdHandle = idHandel.Id;
                request.Status = 1;
                requestDetail.Status = request.Status;
                // Cập nhật thông tin của đối tượng Request từ updatedRequest
                _context.Requets.Update(request);
                _context.Requetsdetaileds.Update(requestDetail);
               bool result = await _context.SaveChangesAsync() > 0;

                return Ok(new
                {
                    Result = result,
                });
            }
            catch (Exception)
            {

                return NotFound("fff11");
            }
        }

        [HttpPut("reprocess/{id}")]
        public async Task<IActionResult> UpdateRequestReprocess(int id, Requet strRequest, Requetsdetailed requestDetail_)
        {
            try
            {
                var request = await _context.Requets.FindAsync(id);
                var requestDetail = await _context.Requetsdetaileds.FirstOrDefaultAsync(r => r.IdRequest == id);
                //Debug.WriteLine(requestDetail);
                if (request == null)
                {
                    return NotFound();
                }
                var idHandel = accountService.getAccountLogin();
                if(idHandel == null)
                {
                    return NotFound();
                }
                
                request.Status = 1;
                if (requestDetail.IdRequest != null)
                {
                    requestDetail.Status = request.Status;
                }
                // Cập nhật thông tin của đối tượng Request từ updatedRequest
                _context.Requets.Update(request);
                _context.Requetsdetaileds.Update(requestDetail);
               bool result = await _context.SaveChangesAsync() >0;

                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception)
            {

                return NotFound("fff11");
            }
            //var strRequest = JsonConvert.DeserializeObject<Requet>(strRequest);

        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPut("updateDetail")]
        public async Task<IActionResult> UpdateRequestDetail([FromForm]string requestDetail_)
        {
            try
            {
                var requestDetail = JsonConvert.DeserializeObject<Requetsdetailed>(requestDetail_);

                bool result = requestService.updatedRequestDetail(requestDetail);

                //_context.Update(requestDetail_);
                var request = await _context.Requets.FindAsync(requestDetail.IdRequest);
                //var requestDetail = await _context.Requetsdetaileds.FirstOrDefaultAsync(r => r.IdRequest == id);
                //Debug.WriteLine(requestDetail);
                var idHandle = accountService.getAccountLogin();
                if (request == null)
                {
                    return NotFound();
                }
                
                request.Status = requestDetail.Status;
                if(requestDetail.Status == 4)
                {
                    requestDetail.Payday = DateTime.Now;
                    request.Enddate = requestDetail.Payday;
                }
               
                _context.Requets.Update(request);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Result = result
                }
                    );
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("updateUserRequest")]
        public async Task<IActionResult> UpdateUserRequest([FromForm]string requestDetail_, List<IFormFile> files)
        {
            try
            {
                var requestDetail = JsonConvert.DeserializeObject<Requetsdetailed>(requestDetail_);
                //_context.Update(requestDetail_);
                var request = await _context.Requets.FirstOrDefaultAsync(r => r.Id == requestDetail.IdRequest); ;
                if (request == null)
                {
                    return NotFound();
                }
                requestDetail.Sentdate = request.Sentdate;
                

                request.Status = requestDetail.Status;
                _context.Requets.Update(request);
                await _context.SaveChangesAsync();

                //_context.Requetsdetaileds.Add(requestDetail);
                bool result = requestService.createdRequestDetail(requestDetail);
                //var a = 
                //xu ly file
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var acc = accountService.getAccountLogin();
                        //    //// Xử lý tệp tin (file)
                        var fileName = GenerateRandomString(10);
                        fileName = Path.Combine(fileName + "_id=" + requestDetail.Id + Path.GetExtension(file.FileName));

                        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "RequestFile", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
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
                }
                return Ok(new
                {
                    Result = result,
                }
                    );
            }
            catch (Exception)
            {

                return NotFound();
            }
            //var strRequest = JsonConvert.DeserializeObject<Requet>(strRequest);

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
                var a = requestService.getRequest();
                var b = JsonConvert.SerializeObject(a);
                return Ok(a);
            }
            catch(Exception e) { 
                return BadRequest(e); }
         
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

        [Produces("application/json")]
        [HttpGet("getRequestDetailFile/{id}")]
        public IActionResult GetRequestDetailFile(int id)
        {
            try
            {
                return Ok(requestService.getFileByIdDetail(id));
            }
            catch { return BadRequest(); }
        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("createRequestWithFile")]
        public IActionResult CreateRequestWithFile(string strRequest, List<IFormFile> files)
        {
            try
            {
                // Giải mã chuỗi JSON để lấy thông tin về yêu cầu (request)
                var request = JsonConvert.DeserializeObject<Requet>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                var idComplain = accountService.getAccountLogin().Id;
                request.IdComplain = idComplain;
                request.Sentdate = DateTime.Now;
                bool result = requestService.createdRequest(request);

                var requestDetail = JsonConvert.DeserializeObject<Requetsdetailed>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                
                requestDetail.IdRequest = request.Id;
                requestDetail.Sentdate = request.Sentdate;
                bool result_ = requestService.createdRequestDetail(requestDetail);

                if (files != null)
                {
                    foreach (var file in files)
                   {
                 
                        //    //// Xử lý tệp tin (file)
                        var fileName = GenerateRandomString(10);
                        fileName = Path.Combine(fileName+"_id="+requestDetail.Id + Path.GetExtension(file.FileName));

                        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "RequestFile", fileName);
                        //using var fileStream = new FileStream(filePath, FileMode.Create);
                //        await file.CopyToAsync(fileStream);

                //        var fileName = FileHelper.generateFileName("absasb"+file.FileName);
                //var path = Path.Combine(webHostEnvironment.WebRootPath, "RequestFile", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
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
                }
                

                return Ok(new
                {
                    result = result,
                    result_ = result_
                });
            }
            catch
            {
                return BadRequest("met ghe ");
            }
        }


        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("createRequestNoAcc")]
        public IActionResult CreateRequestNoAcc(string strRequest)
        {
            try
            {
                // Giải mã chuỗi JSON để lấy thông tin về yêu cầu (request)
                var request = JsonConvert.DeserializeObject<Requet>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                request.Priority = 1;
                //var idComplain = accountService.getAccountLogin();
                //request.IdComplain = idComplain.Id;
                request.Sentdate = DateTime.Now;
                bool result = requestService.createdRequest(request);

                var requestDetail = JsonConvert.DeserializeObject<Requetsdetailed>(strRequest, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });

                requestDetail.IdRequest = request.Id;
                requestDetail.Sentdate = request.Sentdate;
                bool result_ = requestService.createdRequestDetail(requestDetail);

                return Ok(new
                {
                    result = result,
                    result_ = result_
                });
            }
            catch
            {
                return BadRequest("met ghe ");
            }
        }


        [HttpGet("requestFile/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(requestService.getFileById(id));
            }
            catch { return BadRequest(); }
        }

        [HttpGet("requestDetail/{id}")]
        public async Task<IActionResult> GetRequestDetailById(int id)
        {
            try
            {
                return Ok(requestService.getFileByIdDetail(id));
            }
            catch { return BadRequest(); }
        }

        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }


    
}
