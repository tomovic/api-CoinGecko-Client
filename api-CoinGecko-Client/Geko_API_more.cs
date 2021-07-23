using System;
using System.Net.Http;
using System.Threading.Tasks;
using Echovoice.JSON;
using System.IO;

namespace api_CoinGecko_Client
{
    internal class Geko_API_more
    {
        public String address_for_coin_list;
        public String result_from_coinlist_Server;


        public async Task<string> LoaderAsync()
        {
            using (var httpClient = new HttpClient())
            result_from_coinlist_Server = await httpClient.GetStringAsync(address_for_coin_list);
            await Task.Delay(1100);
          //  Console.WriteLine(result_from_coinlist_Server);
            return result_from_coinlist_Server;
        }
    }
}