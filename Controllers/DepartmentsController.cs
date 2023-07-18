using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnTruongTim.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using DuAnTruongTim.Services;

namespace DuAnTruongTim.Controllers
{
    [Route("api/Departments")]
    //[ApiController]
    public class DepartmentsController : ControllerBase
    {
        private CheckQlgiaoVuContext _context;
        private DepartmentService departmentService;
        public DepartmentsController(
            CheckQlgiaoVuContext context,
            DepartmentService _departmentService
            )
        {
            _context = context;
            departmentService= _departmentService;
        }

        // GET: api/Departments
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
          if (_context.Departments == null)
          {
              return NotFound();
          }


            
            return Ok( departmentService.getAllDepartment());


      

        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
          if (_context.Departments == null)
          {
              return NotFound();
          }
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Department>> PostDepartment(Department department)
        //{
        //    if (_context.Departments == null)
        //    {
        //        return Problem("Entity set 'CheckQlgiaoVuContext.Departments'  is null.");
        //    }
        //    _context.Departments.Add(department);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        //}

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Produces("application/json")]
        [HttpGet("getDepartment")]
        public IActionResult GetDepartment()
        {
            try
            {
                return Ok(departmentService.getAllDepartment());
            }
            catch { return BadRequest(); }
        }

        [Produces("application/json")]
        [HttpGet("getDepartmentById/{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                return Ok(departmentService.getDepartmentById(id));
            }
            catch { return BadRequest(); }
        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("created")]
        public IActionResult CreatedDepartment(string strDepartment)
        {
            try
            {
                var department = JsonConvert.DeserializeObject<Department>(strDepartment, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });
                bool result = departmentService.CreatedDeparment(department);
                return Ok(new
                {
                    Result = result,
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPut("updated")]
        public IActionResult UpdatedDepartment(string strDepartment)
        {
            try
            {
                var department = JsonConvert.DeserializeObject<Department>(strDepartment, new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });

                bool result = departmentService.UpdateDepartment(department);
                return Ok(new
                {
                    Result = result,
                });
            }
            catch { return BadRequest(); }
        }
        
    }
}
