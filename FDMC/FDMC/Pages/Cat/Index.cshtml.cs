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
    public class IndexModel : PageModel
    {
        private readonly FDMC.Models.CatContext _context;

        public IndexModel(FDMC.Models.CatContext context)
        {
            _context = context;
        }

        public IList<Models.Cat> Cat { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cats != null)
            {
                Cat = await _context.Cats.ToListAsync();
            }
        }
    }
}
