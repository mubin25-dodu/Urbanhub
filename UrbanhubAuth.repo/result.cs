using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanHubManagement.repo
{
    public class result<Table>
    {
        public Table? data { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string AdditionalMessage { get; set; }

    }
}
