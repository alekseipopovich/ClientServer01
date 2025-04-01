using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Async HTTP-request

namespace MultiThread01
{
    class Programm27AsyncHttp
    {
        static async Task Main27(string[] args)
        {
            using HttpClient client = new HttpClient();

            try
            {
                string content = await GetWebsiteContentAsync(client, "http://httpbin.org/get");
                Console.WriteLine(content.Substring(0, 100)); // Выводим первые 100 символов
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }

        static async Task<string> GetWebsiteContentAsync(HttpClient client, string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
