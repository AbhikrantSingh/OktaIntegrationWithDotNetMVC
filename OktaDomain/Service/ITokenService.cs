using OktaDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OktaDomain.Service
{
    public interface ITokenService
    {
        Task<OktaResponse> GetToken(string userName, string password);
    }
}
