// using System;
// using System.Collections.Generic;
using System.Net;
// using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

public class JsonResponse
{
    [JsonProperty("headers")]
    public Headers? Headers { get; set; }
}

public class Headers
{
    [JsonProperty("X-Amzn-Trace-Id")]
    public string? XAmznTraceId { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        var options = new RestClientOptions
        {
            Proxy = new WebProxy("127.0.0.1", 1082)
        };

        var client = new RestClient(options);
        var tasks = new List<Task>();

        for (int i = 1; i <= 100; i++)
        {
            int page = i;
            tasks.Add(Task.Run(() => ProcessPageAsync(client, page)));
        }

        await Task.WhenAll(tasks);
    }

    static async Task ProcessPageAsync(RestClient client, int pageNumber)
    {
        var request = new RestRequest($"https://httpbin.org/get?page={pageNumber}");
        var resp_raw = await client.ExecuteAsync(request);
        if (resp_raw.Content != null)
        {
            var response = JsonConvert.DeserializeObject<JsonResponse>(resp_raw.Content);
            if (response != null && response.Headers != null)
            {   
                // return pageNumber,response.Headers.XAmznTraceId;
                Console.Write("\t");
                Console.Write($"Response from page {pageNumber} {DateTime.Now} - X-Amzn-Trace-Id: {response.Headers.XAmznTraceId}\n");
            }
        }
    }
}
