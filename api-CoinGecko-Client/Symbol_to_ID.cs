using Echovoice.JSON;
using System;

namespace api_CoinGecko_Client
{
    internal class Symbol_to_ID
    {

        public string[] search_ID_form_file = new string[10];
        public string[] suitable_ID_form_api = new string[10];

        public async void create_search()
        {
            Console.WriteLine("jetzt kommt...xxxxx.");
            Geko_API coinlist = new Geko_API();
            coinlist.address_for_coin_list = "https://api.coingecko.com/api/v3/coins/list?include_platform=true";
            string result = await coinlist.LoaderAsync();


            string[] coin_list_json = JSONDecoders.DecodeJsStringArray(result);
            string coin_id;
            string[,] coin_list_array = new string[9000, 8];

            int counter_for_coin_list_arry = 0;

            for (int counter_for_coinlist = 0; counter_for_coinlist <= coin_list_json.GetUpperBound(0); counter_for_coinlist++)
            {

                if (coin_list_json[counter_for_coinlist][0].ToString() == "{")
                {

                    if (coin_list_json[counter_for_coinlist].Substring(coin_list_json[counter_for_coinlist].Length - 1) == "}")
                    {
                        coin_id = coin_list_json[counter_for_coinlist].Remove(0, 2);
                        coin_list_array[counter_for_coin_list_arry, 0] = coin_id;   // char 2 with }
                    }
                    else
                    {
                        coin_id = coin_list_json[counter_for_coinlist].Remove(0, 2);
                        coin_id = coin_id.Substring(0, coin_id.Length - 1);
                        coin_list_array[counter_for_coin_list_arry, 0] = coin_id;   // char 2 no }
                        Console.WriteLine(1 + "---found ID --->" + coin_id);  // char with no  }

                        for (int counter_for_more_elemente = 1; counter_for_more_elemente <= coin_list_array.GetUpperBound(1); counter_for_more_elemente++)
                        {
                            if (coin_list_json[counter_for_coinlist + counter_for_more_elemente].Substring(coin_list_json[counter_for_coinlist + counter_for_more_elemente].Length - 1) == "}") // CHECK 2 char -> }
                            {
                                coin_list_array[counter_for_coin_list_arry, counter_for_more_elemente] = coin_list_json[counter_for_coinlist + counter_for_more_elemente];   // char 2 no }
                             //  Console.WriteLine(1 + counter_for_more_elemente + " " + coin_list_json[counter_for_coinlist + counter_for_more_elemente] + " end");  // char with }
                                break;
                            }
                            else
                            {
                                coin_list_array[counter_for_coin_list_arry, counter_for_more_elemente] = coin_list_json[counter_for_coinlist + counter_for_more_elemente];   // char 2 no }
                             //  Console.WriteLine(1 + counter_for_more_elemente + " " + coin_list_json[counter_for_coinlist + counter_for_more_elemente]);            // char no }
                            }
                        }
                    }
                    counter_for_coin_list_arry++;
                }
            }
       
            for (int counter_for_search_ID = 0; counter_for_search_ID <= search_ID_form_file.GetUpperBound(0); counter_for_search_ID++)
            {
              

                for (int counter_for_complet_id_list = 0; counter_for_complet_id_list < coin_list_array.GetUpperBound(0) - 4000; counter_for_complet_id_list++)
                {
                    
                    if (coin_list_array[counter_for_complet_id_list, 1].Remove(0, 9) == search_ID_form_file[counter_for_search_ID])
                    {
                        Console.WriteLine("------------found-------------");
                        Console.WriteLine(coin_list_array[counter_for_complet_id_list, 0].Remove(0, 5));
                        suitable_ID_form_api[counter_for_search_ID] = coin_list_array[counter_for_complet_id_list, 0].Remove(0, 5);
                    }
                }
            }
        }
    }
}