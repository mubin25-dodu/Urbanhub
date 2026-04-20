using System;
using System.Collections.Generic;

namespace UrbanHub.Data.Entities;

public partial class User
{
    public int Uid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public int? Vid { get; set; }

    public int? Logid { get; set; }
}
