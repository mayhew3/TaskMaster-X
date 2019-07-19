using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace TaskMaster
{
    class RestService : IRestService
    {
        HttpClient _client;

        public List<TodoItem> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<TodoItem>> RefreshDataAsync()
        {
            Items = new List<TodoItem>();

            var uri = new Uri("https://taskmaster-general.herokuapp.com/api/tasks");
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }
    }
}