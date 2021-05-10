﻿using appconsultacep.Models;
using Correios.NET;
using Correios.NET.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace appconsultacep.Services
{
    public class PackageServices
    {
        private const string PACKAGE_TRACKING_URL = "https://www2.correios.com.br/sistemas/rastreamento/ctrl/ctrlRastreamento.cfm";
        private readonly HttpClient _httpClient;
        private const string ZIP_ADDRESS_URL = "https://buscacepinter.correios.com.br/app/endereco/carrega-cep-endereco.php?endereco={0}&tipoCEP=ALL";

        public PackageServices()
        {
            _httpClient = new HttpClient();
        }

        //public async Task<PackageModel> GetPackageTrackingAsync(string packageCode)
        //{
        //    using (var response = await _httpClient.PostAsync(PACKAGE_TRACKING_URL, CreatePackageTrackingRequest(packageCode)))
        //    {
        //        var html = await response.Content.ReadAsStringAsync();
        //        return await Task.Run(() => Parser.ParsePackage(html));
        //    }
        //}

        private FormUrlEncodedContent CreatePackageTrackingRequest(string packageCode)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("objetos", packageCode),
                new KeyValuePair<string, string>("p_tipo", "001"),
                new KeyValuePair<string, string>("p_lingua", "001")
            });
            return content;
        }

        //public Models.PackageModel GetPackageTracking(string packageCode)
        //{
        //    return GetPackageTrackingAsync(packageCode).Result;
        //}

        public async Task<IEnumerable<Address>> GetAddressesAsync(string zipCode)
        {
            using (var response = await _httpClient.GetAsync(string.Format(ZIP_ADDRESS_URL, zipCode)))
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var correiosAddressResponse = JsonConvert.DeserializeObject<CorreiosAddresResponse>(jsonResponse);
                if (correiosAddressResponse != null && !correiosAddressResponse.Erro)
                {
                    return correiosAddressResponse.Dados.Select(a => new Address
                    {
                        Street = a.LogradouroDNEC,
                        District = a.Bairro,
                        City = a.Localidade,
                        State = a.Uf,
                        ZipCode = a.Cep
                    });
                }
            }

            return null;
        }

        public IEnumerable<Address> GetAddresses(string zipCode)
        {
            return GetAddressesAsync(zipCode).Result;
        }
    }
}
