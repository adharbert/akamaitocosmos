using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Akamai
{
    public class AkamaiUserTotalCount
    {
        public int total_count { get; set; }
        public string stat { get; set; }
        public string error { get; set; }
        public int code { get; set; }
        public string argument_name { get; set; }
    }
}
