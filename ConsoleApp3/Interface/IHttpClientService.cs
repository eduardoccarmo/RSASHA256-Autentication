using ConsoleApp3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Interface
{
    public interface IHttpClientService
    {
        Task<BearerToken> GenerateToken(string assertion);
    }
}
