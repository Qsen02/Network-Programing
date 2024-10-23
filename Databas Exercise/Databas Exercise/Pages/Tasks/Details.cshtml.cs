using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Databas_Exercise.Models;

namespace Databas_Exercise.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly Databas_Exercise.Models.TasksContext _context;

        public DetailsModel(Databas_Exercise.Models.TasksContext context)
        {
            _context = context;
        }

      public Models.Task Task { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            else 
            {
                Task = task;
            }
            return Page();
        }
    }
}
