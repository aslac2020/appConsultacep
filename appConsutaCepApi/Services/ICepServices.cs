using appConsutaCepApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appConsutaCepApi.Services
{
    public interface ICepServices
    {
        public Task<CepModel> SearchCep(string cep);
    }
}
