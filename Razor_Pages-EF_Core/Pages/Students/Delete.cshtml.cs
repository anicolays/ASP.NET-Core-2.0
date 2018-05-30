using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_Pages_EF_Core.Data;
using Razor_Pages_EF_Core.Models;

namespace Razor_Pages_EF_Core.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolContext _context;

        public DeleteModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangeError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }

            if (saveChangeError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }

            try
            {
                throw new DbUpdateException("", new Exception());

                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("./Delete", new { id = id, saveChangeError = true });
            }

        }
    }
}
