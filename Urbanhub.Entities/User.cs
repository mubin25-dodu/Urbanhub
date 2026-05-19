using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanHub.Entities;

[Table("User")]
public partial class User
{
    [Key , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Uid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string Role { get; set; } = "User";

    public string Address { get; set; } = null!;

    public string Status { get; set;  }= "Active";

    public DateTime JoinDate { get; set; }

    public int? Vid { get; set; }

    public int? Logid { get; set; }

    public string Phone { get; set; } = null!;

    [ForeignKey("Logid")]
    public virtual Log ID { get; set; } =null!;

}