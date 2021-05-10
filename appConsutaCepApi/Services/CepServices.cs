using appConsutaCepApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace appConsutaCepApi.Services
{
    public class CepServices : ICepServices
    {
        private static string apiUri = "https://viacep.com.br/ws/{0}/json/";
        public async Task<CepModel> SearchCep(string cep)
        {
            var response = new CepModel();

            try
            {
                string searchCep = string.Format(apiUri, cep);

                WebClient client = new WebClient();
                var result = client.DownloadString(searchCep);

                var streetCep = JsonConvert.DeserializeObject<CepModel>(result);

                if (streetCep.cep == null) return null;

                else
                {
                    response.logradouro = streetCep.logradouro;
                    response.logradouro = streetCep.logradouro;
                    response.logradouro = streetCep.logradouro;
                    response.logradouro = streetCep.logradouro;

                    return response;

                }

            }
            catch (System.Exception)
            {
                Console.WriteLine("Ops! Cidade Inexistente :(");
                return response;
            }
            
        }

    }
}
