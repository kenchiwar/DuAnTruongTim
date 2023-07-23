using DuAnTruongTim.Helpers;
using DuAnTruongTim.Models;
using DuAnTruongTim.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace DuAnTruongTim.Controllers;
[Route("api/requestFile")]
public class RequestFileController : Controller
{
    private CheckQlgiaoVuContext db;
    private IWebHostEnvironment webHostEnvironment;
    private AccountService accountService;
    private RequestFileServicecs requestFileService;
    public RequestFileController(
        CheckQlgiaoVuContext _db,
        IWebHostEnvironment _webHostEnvironment,
        RequestFileServicecs _requestFileServicecs,
    AccountService _accountService
        )
    {
        db = _db;
        webHostEnvironment = _webHostEnvironment;
        accountService = _accountService;
        requestFileService = _requestFileServicecs;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<RequestFile>>> GetRequets()
    {
        if (db.RequestFiles == null)
        {
            return NotFound();
        }
        return await db.RequestFiles.ToListAsync();
    }

    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPost("requestFile")]
    public IActionResult CreatedRequstFile(FormFile name, string strRequestFile)
    {
        try
        {
            var fileName = FileHelper.generateFileName(name.FileName + "/id=" + accountService.getAccountLogin().Id);
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
            bool result = requestFileService.CreatedRequestFile(requestFile);
            return Ok(new
            {
                Result = result
            });
        }
        catch { return BadRequest(); }
    }
}
