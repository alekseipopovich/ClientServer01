using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] // Маршрут будет /Grades
    public class GradesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public GradesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: /Grades - Получить все оценки
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            return await _context.Grades.ToListAsync();
        }

        // GET: /Grades/5 - Получить оценку по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return grade;
        }

        // POST: /Grades - Создать новую оценку
        [HttpPost]
        public async Task<ActionResult<Grade>> CreateGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGrade), new { id = grade.GradeID }, grade);
        }

        // PUT: /Grades/5 - Обновить оценку
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, Grade grade)
        {
            if (id != grade.GradeID)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Grades.Any(g => g.GradeID == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: /Grades/5 - Удалить оценку
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}