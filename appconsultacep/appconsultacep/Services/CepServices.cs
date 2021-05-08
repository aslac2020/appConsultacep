using appconsultacep.Interfaces;
using appconsultacep.Models;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace appconsultacep.Services
{
    public class CepServices
    {
        private static string apiUri = "https://viacep.com.br/ws/{0}/json/";
        private IMessage _toast = DependencyService.Get<IMessage>();
        public static CepModels SearchCep(string cep)
        {
            string searchCep = string.Format(apiUri, cep);

            WebClient client = new WebClient();
            var result = client.DownloadString(searchCep);

            var streetCep = JsonConvert.DeserializeObject<CepModels>(result);

            if (streetCep.cep == null) return null;


            return streetCep;
        }


    }
   
}
