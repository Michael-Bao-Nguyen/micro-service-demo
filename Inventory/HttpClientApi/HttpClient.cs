using Catalog.Entities;
using Newtonsoft.Json;

namespace Inventory.HttpClientApi
{
    public class HttpClientApi
    {
        // get catalog info by id
        public HttpClient _client { get; set; }
        public string Host { get; set; }
        public string EndPoint { get; set; }

        public HttpClientApi( string host, string endPoint ) { 
            _client = new HttpClient();
            Host = host;
            EndPoint = endPoint;
        }
        public async Task<List<Category>> GetAllCategory() {
            var result = await  _client.GetAsync(Host + EndPoint);
            var a = await result.Content.ReadAsStringAsync();
            var test = JsonConvert.DeserializeObject<List<Category>>(a);
            return test;
        }
    }
}
