using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Api.Data;
using Student.Api.Data.Entities;

namespace Student.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly MyWorldDbContext _dbContext;
        public StudentController(MyWorldDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var student = await _dbContext.student.ToListAsync();
            var student = await _dbContext.student.FromSqlRaw("procGetStudentDetails").ToListAsync();
            return Ok(student);
        }
       
        [HttpPost]
        public async Task<IActionResult> Post(Student.Api.Data.Entities.Student payload)
        {
            //_dbContext.student.Add(payload);
            //await _dbContext.SaveChangesAsync();
            //return Ok(payload);
            string StoredProc = "exec procPostStudentDetails " +
                "@Name='" + payload.Name + "'," +
                "@Age=" + payload.Age + "," +
                "@Gender='" + payload.Gender + "'";
            var student = await _dbContext.student.FromSqlRaw(StoredProc).ToListAsync();
            return Ok(student);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Student.Api.Data.Entities.Student payload)
        { 
            _dbContext.student.Update(payload);
            await _dbContext.SaveChangesAsync();
            return Ok(payload);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var studentToRemove = await _dbContext.student.FindAsync(id);   
            if( studentToRemove == null)
            {
                return NotFound();
            }
            _dbContext.student.Remove(studentToRemove);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
