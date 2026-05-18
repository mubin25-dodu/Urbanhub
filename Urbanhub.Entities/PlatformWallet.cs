using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanHub.Entities
{
    [Table("PlatformWallet")]
    public class PlatformWallet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public decimal BankBalance { get; set; } 
        public decimal PlatformFee { get; set; } 
        public decimal EarnedPlatformFee { get; set; } 

    }
}
