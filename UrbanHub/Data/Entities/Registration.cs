using System;
using System.Collections.Generic;

namespace UrbanHub.Data.Entities;

public partial class Registration
{
    public int Rid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
