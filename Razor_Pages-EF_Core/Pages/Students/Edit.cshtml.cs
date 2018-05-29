using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Razor_Pages_EF_Core.Data;
using Razor_Pages_EF_Core.Models;

namespace Razor_Pages_EF_Core.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
            //Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()//int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // var studentToUpdate = await _context.Students.FindAsync(id);

            // if (await TryUpdateModelAsync<Student>(
            //     studentToUpdate,
            //     "student",
            //     s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            // {
            //     await _context.SaveChangesAsync();
            //     return RedirectToPage("./Index");
            // }

            // return Page();

            #region From template
            _context.Attach(Student).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return RedirectToPage("./Index");
            #endregion

        }
    }
}
