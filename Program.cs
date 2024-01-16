// using System;
using System.Collections.Concurrent;
// using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
// using System.Linq;


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
        var tasks = new ConcurrentBag<Task<Tuple<int, DateTime, DateTime, string>>>(); // 创建一个用于存储任务的列表
        Parallel.For(1, 101, (i) =>
        {
            int page = i;
            /*在 child_task => 这个表达式中，child_task 是一个 lambda 表达式的参数。
              在 C# 中，lambda 表达式是一种简洁的方式来定义匿名函数。这个表达式通常用于创建简短的回调函数。
              在您的代码中，child_task 代表的是由 ProcessPageAsync 方法返回的 Task 对象。
              当您调用 ContinueWith 方法时，您正在告诉程序：“当这个任务完成后，执行这个 lambda 表达式。” 
              这个 lambda 表达式接收一个 Task 参数（在这里我们命名为 child_task），
              这个 Task 对象包含了 ProcessPageAsync 方法的返回值。
            */
            // 创建并存储任务
            var task = ProcessPageAsync(client, page).ContinueWith(child_task =>
            {
                var result = child_task.Result;
                string pageStr = result.Item1.ToString().PadLeft(3, '0');
                Console.WriteLine($"{pageStr} - ReqT {result.Item2}\tResT {result.Item3}\tTraceId: {result.Item4}");
                return result;
            });
            tasks.Add(task);
        });

        // 等待所有任务完成
        await Task.WhenAll(tasks);
    }

    static async Task<Tuple<int, DateTime, DateTime, string>> ProcessPageAsync(RestClient client, int pageNumber)
    {
        // 模拟CPU密集型操作，例如计算斐波那契数列
        long Fibonacci(int n)
        {
            if (n <= 1) return n;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        // 计算一个较大的斐波那契数
        var fibResult = Fibonacci(40); // 这个值可以根据需要调整
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

