using System;
//using System;
//using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using Echovoice.JSON;
using System.Threading.Tasks;

namespace api_CoinGecko_Client
{
    class Program
    {
        static string[,] config_array_type = new string[20, 20];
        static string[,] config_array_key = new string[20, 20];
        static string[,] file_inside = new string[20, 20];
        string[] coin_name_check = new string[40];


       static async Task Main(string[] args)
       {
                   
            Load_config_file();

           Symbol_to_ID symbol_to_ID = new Symbol_to_ID();

           for (int file_block = 0; file_block <= config_array_type.GetUpperBound(0); file_block++)
           {
               for (int file_line = 0; file_line <= config_array_type.GetUpperBound(1); file_line++)
               {
                   if (config_array_type[file_block, file_line] == "symbol")
                   {
                       Console.WriteLine("----> " + config_array_key[file_block, file_line]);
                       symbol_to_ID.search_ID_form_file[file_block] = config_array_key[file_block, file_line];
                   }
               }
           }
          
           // ####################################  start id search ######################################################

           symbol_to_ID.create_search();
      
           await Task.Delay(10000);          

           ID_to_enquiry new_convert = new ID_to_enquiry();

           for (int counter_for_symbol_ID = 0; counter_for_symbol_ID <= 5; counter_for_symbol_ID++)
           {
                if (!(String.IsNullOrEmpty(symbol_to_ID.suitable_ID_form_api[counter_for_symbol_ID])))
                {
                    Console.WriteLine("Check---COIN--------------------->" + symbol_to_ID.suitable_ID_form_api[counter_for_symbol_ID] + "<---------");
                    new_convert.id_search = symbol_to_ID.suitable_ID_form_api[counter_for_symbol_ID];

                    Console.WriteLine("######################### Here is the block #################################");
                    for (int file_line = 0; file_line <= config_array_key.GetUpperBound(1); file_line++)
                    {
                        if (config_array_key[counter_for_symbol_ID, file_line] == "true")
                        {
                            Console.WriteLine("is true: " + config_array_type[counter_for_symbol_ID, file_line]);
                            new_convert.search_form_file[file_line] = config_array_type[counter_for_symbol_ID, file_line];                           
                        }
                    }
                    new_convert.create_search();
                    await Task.Delay(3000);
                }
           }

            Console.WriteLine("------------now comes savefile ...........");
            string fullPath = "output.json";
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("{");
                writer.WriteLine("  \u0022coins\u0022: [");
                for (int counter_for_writefile = 0; counter_for_writefile < new_convert.saving_counter; counter_for_writefile++)
                {
                    writer.WriteLine(new_convert.saving_string[counter_for_writefile]);
                }
                writer.WriteLine("    ],");

                writer.WriteLine("}");
            }
            string readText = File.ReadAllText(fullPath);


        }
        static void Load_config_file()
        {
            int counter_for_config_file_key_and_name = 0;
            int conuter_for_config_file_block = 0;
            //   var fileName = "c:/mycoin/coingecko-client-config3.json";
            var fileName = "coingecko-client-config3.json";
            byte[] data = File.ReadAllBytes(fileName);
            Utf8JsonReader reader = new Utf8JsonReader(data);

            Console.Write(reader.Read());

            while (reader.Read())
            {
                switch (reader.TokenType)
                {

                    case JsonTokenType.StartObject:
                        break;

                    case JsonTokenType.PropertyName:
                        Console.Write($"{reader.GetString()}: ");
                        config_array_type[conuter_for_config_file_block, counter_for_config_file_key_and_name] = reader.GetString();
                        break;

                    case JsonTokenType.String:
                        Console.WriteLine(reader.GetString());
                        config_array_key[conuter_for_config_file_block, counter_for_config_file_key_and_name] = reader.GetString();
                        counter_for_config_file_key_and_name++;
                        break;

                    case JsonTokenType.EndObject:
                        counter_for_config_file_key_and_name = 0;
                        conuter_for_config_file_block++;
                        break;

                }
            }
        }
    }
}
