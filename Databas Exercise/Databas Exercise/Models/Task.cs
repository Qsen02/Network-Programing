using System;
using System.Collections.Generic;

namespace Databas_Exercise.Models;

public partial class Task
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Status { get; set; }
}
