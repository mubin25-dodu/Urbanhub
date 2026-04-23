using System;
using System.Collections.Generic;

namespace UrbanHub.Data.Entities;

public partial class Email
{
    public int Id { get; set; }

    public string MailTo { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime Date { get; set; }

    public int LogId { get; set; }
}
