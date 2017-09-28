using ClientConvertisseurV1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV1.Service
{
    public class WSService
    {
        static HttpClient client = new HttpClient();
        private static WSService _wsService;



        // Fonction pour garder le pattern du singleton en instanciant qu'une seule fois WSService.
        public static WSService GetInstance()
        {
            if(_wsService == null)
            {
                client.BaseAddress = new Uri("http://localhost:54366/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                _wsService = new WSService();
            }
            return _wsService;
        }

        private WSService()
        {
        }
        
        
         public async Task<List<Devise>> GetAllDevisesAsync()
        {
            List<Devise> devises = new List<Devise>();
            HttpResponseMessage response = await client.GetAsync("devise");
            if (response.IsSuccessStatusCode)
            {
                devises = await response.Content.ReadAsAsync<List<Devise>>();
            }
            return devises;
        }


        public double CalculConversionDevise(double montant, Devise devise)
        {
            return montant*devise.Taux;
        }

    }
}
