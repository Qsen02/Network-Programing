﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Databas_Exercise.Models;

namespace Databas_Exercise.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly Databas_Exercise.Models.TasksContext _context;

        public IndexModel(Databas_Exercise.Models.TasksContext context)
        {
            _context = context;
        }

        public IList<Models.Task> Task { get;set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            if (_context.Tasks != null)
            {
                Task = await _context.Tasks.ToListAsync();
            }
        }
    }
}
