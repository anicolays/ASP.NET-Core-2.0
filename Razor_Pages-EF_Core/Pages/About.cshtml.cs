using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_Pages_EF_Core.Data;
using Razor_Pages_EF_Core.Models.SchoolViewModels;

namespace Razor_Pages_EF_Core.Pages
{
    public class AboutModel : PageModel
    {

        private readonly SchoolContext _context;

        public AboutModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<EnrollmentDateGroup> Student { get; set; }




        public async Task OnGet()
        {
            IQueryable<EnrollmentDateGroup> data = 
            from student in _context.Students
            group student by student.EnrollmentDate into dateGroup 
            select new EnrollmentDateGroup()
            {
                EnrollmentDate = dateGroup.Key,
                StudentCount = dateGroup.Count()
            };

            Student = await data.AsNoTracking().ToListAsync();
        }
    }
}
