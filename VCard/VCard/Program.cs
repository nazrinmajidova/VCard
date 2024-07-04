using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VCardProject.Models;
using static VCardProject.Program;

namespace VCardProject
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // VCard filelarının save olunduğu directory
            var FilePath = "D:\\Back-End\\VCard\\VCard\\VCards";
            try
            {

                Console.Write("Please enter the number of vCards you want to create: ");
                int size = Convert.ToInt32(Console.ReadLine());

                // random user datanı aldığımız url
                string url = $"https://randomuser.me/api/?results={size}";

                // APİ requestləri yaratmaq üçün HttpClient yaradırıq 
                using HttpClient client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // JSON responseəları RandomUserResponse obyektinə deserialize edirik
                var users = JsonConvert.DeserializeObject<RandomUserResponse>(responseBody);


                foreach (var vCard in users.Results)
                {
                    string vCardString = vCard.VCardFormat();
                    Console.WriteLine(vCardString); // Vcardı string şəkildə console da göstərir

                    // VCard stringi file a save edir 
                    VCard.SaveMethod(vCardString, $"{vCard.Name.First}{vCard.Name.Last}", FilePath);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }


    }


}