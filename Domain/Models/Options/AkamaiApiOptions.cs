using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Options
{
    public class AkamaiApiOptions
    {
        public Uri Url { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
