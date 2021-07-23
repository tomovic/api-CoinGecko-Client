using System;
using System.Net.Http;
using System.Threading.Tasks;
using Echovoice.JSON;
using System.IO;


namespace api_CoinGecko_Client
{
    internal class Geko_API
    {

        public String address_for_coin_list;
        public String result_from_coinlist_Server;

     
        public async Task<string> LoaderAsync()
        {
        //    Console.WriteLine("-geht jetzt in web->");
            Console.WriteLine(address_for_coin_list);
            using (var httpClient = new HttpClient())
            result_from_coinlist_Server = await httpClient.GetStringAsync(address_for_coin_list);          
        //    Console.WriteLine("-->"); 
         //   Console.WriteLine("<------ Die Coinliste ist zu Ende -------------->");
            return result_from_coinlist_Server;
        }

    }
}