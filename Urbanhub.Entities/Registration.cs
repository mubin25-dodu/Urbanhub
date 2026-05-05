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
        [StringLength(50)]
        public string Name { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; } = null!;
    }
}
