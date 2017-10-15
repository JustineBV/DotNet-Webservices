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
        private static WSService _wsService = null;



        /// <summary>
        /// Fonction permettant de créer un singleton en instanciant qu'une seule fois WSService.
        /// Fonction static pour pouvoir être appelée partout
        /// </summary>
        /// <returns>WSService : la référence si il existait déjà ou une instanciation</returns>
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


        /// <summary>
        /// Constructeur private pour ne pas pouvoir être utilisé ailleurs que par  la fonction GetInstance() => singleton
        /// </summary>
        private WSService()
        {
        }


        /// <summary>
        /// Retourne de manière asynchrone toutes les devises de notre client HttpClient()
        /// </summary>
        /// <returns>Task<List<Devise>> </returns>
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


        /// <summary>
        /// Retourne le montant donné en paramètre convertit par le taux de la devise donnée en paramètre
        /// </summary>
        /// <param name="montant"></param>
        /// <param name="devise"></param>
        /// <returns>montant en double</returns>
        public double CalculConversionDevise(double montant, Devise devise)
        {
            return montant*devise.Taux;
        }

    }
}
