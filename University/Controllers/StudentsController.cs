using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
    public StudentsController(SchoolContext context)
    {
        _context = context;
    }
    
        public async Task<IActionResult> Index()
        {
            var result = await _context.Students.ToListAsync();
            return View(result);
        }
        public async Task<IActionResult>Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var Student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id ==id);

            if(Student == null)
            {
                return NotFound(); 
            }

            return View(Student);
        }
}

 }