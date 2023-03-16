using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OktaDomain.Model
{
    public class OktaTokenSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Domain { get; set; }
        public string AuthorizationServerId { get; set; }
        public string Audience { get; set; }
    }
}
