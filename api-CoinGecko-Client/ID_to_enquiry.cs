using Echovoice.JSON;
using System;
using System.IO;
using System.Threading.Tasks;

namespace api_CoinGecko_Client
{
    internal class ID_to_enquiry
    {
        // public string[] id_search = new string[10];
        public string id_search;
        public string string_for_save_file;
        public string[] search_return = new string[ 25];
        public string[] search_form_file = new string[25];
        public string[,] master_result = new string[3, 4];

        public string[] saving_string = new string[100];
        public int saving_counter = 0;


        public async void create_search()
        {
        
        Geko_API coinlistx = new Geko_API();

            coinlistx.address_for_coin_list = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&ids=" + id_search + "&order=market_cap_descmarket_cap_desc%2C%20gecko_desc%2C%20gecko_asc%2C%20market_cap_asc%2C%20market_cap_desc%2C%20volume_asc%2C%20volume_desc%2C%20id_asc%2C%20id_desc&per_page=100&page=1&sparkline=true&price_change_percentage=1h";
            string result = await coinlistx.LoaderAsync();
            string[] coin_list_json = JSONDecoders.DecodeJsStringArray(result);
          //  string coin_id;
            string[,] coin_list_array = new string[9000, 8];

            saving_string[saving_counter] = "    {";
            saving_counter++;
            saving_string[saving_counter] = "      \u0022" + id_search + "\u0022,";
            saving_counter++;

            for (int counter_for_retrun_elemente = 0; counter_for_retrun_elemente <= search_return.GetUpperBound(0); counter_for_retrun_elemente++)
            {
                    search_return[ counter_for_retrun_elemente] = coin_list_json[counter_for_retrun_elemente];
            }
                          
            for (int coinline_form_web = 0; coinline_form_web <= search_return.GetUpperBound(0); coinline_form_web++)
            {
                 for (int search_line_form_file = 0; search_line_form_file <= search_form_file.GetUpperBound(0); search_line_form_file++)
                 {
                 string[] content_for_savefile = search_return[coinline_form_web].Split(new Char[] { ':' });

                       if (content_for_savefile[0].Remove(content_for_savefile[0].Length - 1, 1) == search_form_file[search_line_form_file])
                       {

                       Console.WriteLine("--x--found request -->" + content_for_savefile[0] + " : " + content_for_savefile[1]);
                       saving_string[saving_counter] = "      \u0022" + content_for_savefile[0] + ": \u0022" + content_for_savefile[1] + "\u0022,";
                       saving_counter++;
                       }
                 }
            }
            saving_string[saving_counter] = "    },";
            saving_counter++;                    
        }
    } }