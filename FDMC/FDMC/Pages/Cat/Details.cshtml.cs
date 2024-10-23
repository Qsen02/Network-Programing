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
    public class DetailsModel : PageModel
    {
        private readonly FDMC.Models.CatContext _context;

        public DetailsModel(FDMC.Models.CatContext context)
        {
            _context = context;
        }

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
    }
}
