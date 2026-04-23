using System;
using System.Collections.Generic;

namespace UrbanHub.Data.Entities;

public partial class Verification
{
    public int Id { get; set; }

    public int Uid { get; set; }

    public string Method { get; set; } = null!;

    public string Images { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime AppliedOn { get; set; }

    public int? LogId { get; set; }
}
