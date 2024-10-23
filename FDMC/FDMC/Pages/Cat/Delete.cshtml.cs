using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FDMC.Models;

namespace FDMC.Pages.Cat
{
    public class DeleteModel : PageModel
    {
        private readonly FDMC.Models.CatContext _context;

        public DeleteModel(FDMC.Models.CatContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Cat Cat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FirstOrDefaultAsync(m => m.Id == id);

            if (cat == null)
            {
                return NotFound();
            }
            else 
            {
                Cat = cat;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }
            var cat = await _context.Cats.FindAsync(id);

            if (cat != null)
            {
                Cat = cat;
                _context.Cats.Remove(Cat);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
