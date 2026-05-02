using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanHub.Entities
{
    public class Registration
    {
        public int Rid { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
