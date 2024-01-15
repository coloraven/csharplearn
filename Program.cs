using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Linq;

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
        var tasks = new List<Task<Tuple<int, DateTime, DateTime, string>>>(); // 使用Tuple<int, string>来存储结果

        for (int i = 1; i <= 100; i++)
        {
            int page = i;
            tasks.Add(Task.Run(() => ProcessPageAsync(client, page)));
        }

        // 收集所有结果
        var results = await Task.WhenAll(tasks);

        // 使用 LINQ 按 ReqT 时间排序
        var sortedResults = results.OrderBy(result => result.Item2);

        foreach (var result in sortedResults)
        {
            string page = result.Item1.ToString().PadLeft(3, '0');
            Console.WriteLine($"{page} - ReqT {result.Item2}\tResT {result.Item3}\tTraceId: {result.Item4}");
        }
    }

    static async Task<Tuple<int, DateTime, DateTime, string>> ProcessPageAsync(RestClient client, int pageNumber)
    {
        DateTime request_time = DateTime.Now;
        var request = new RestRequest($"https://httpbin.org/get?page={pageNumber}");
        var resp_raw = await client.ExecuteAsync(request);
        DateTime res_time = DateTime.Now;
        if (resp_raw.Content != null)
        {
            var response = JsonConvert.DeserializeObject<JsonResponse>(resp_raw.Content);
            if (response != null && response.Headers != null && response.Headers.XAmznTraceId != null)
            {
                var TraceId = response.Headers.XAmznTraceId;
                string xAmznTraceIdLast4 = TraceId.Substring(TraceId.Length - 4);
                return Tuple.Create(pageNumber, request_time, res_time, xAmznTraceIdLast4);
            }
        }
        // 如果没有有效的结果，返回一个占位符值
        request_time = DateTime.Now;
        return Tuple.Create(pageNumber, request_time, res_time, "N/A");
    }
}
