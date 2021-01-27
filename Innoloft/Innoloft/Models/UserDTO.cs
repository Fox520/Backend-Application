using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Innoloft.Models
{

    public class UserDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public Company company { get; set; }

        public static async Task<UserDTO> fetchUserAsync(int id)
        {
            // Fetch and deserialize user object from external resource
            using var client = new HttpClient();
            var result = await client.GetAsync("https://jsonplaceholder.typicode.com/users/" + id);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(await result.Content.ReadAsStringAsync());
        }
    }
}
