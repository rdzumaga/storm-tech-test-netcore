using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Todo.Dto;

namespace Todo.Services
{
    public class Gravatar : IGravatar
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<Gravatar> logger;
        private readonly IMemoryCache memoryCache;

        public Gravatar(IHttpClientFactory httpClientFactory, ILogger<Gravatar> logger, IMemoryCache memoryCache)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        public async Task<string> GetUserDetailsAsync(string emailAddress)
        {
            var cachedResult = await memoryCache.GetOrCreateAsync<string>(emailAddress, async (cacheEntry) => {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return await GetUserDetailsInternalAsync(emailAddress);
            });

            return cachedResult;
        }

        private async Task<string> GetUserDetailsInternalAsync(string emailAddress) {
            string result = emailAddress;
            using (var httpClient = this.httpClientFactory.CreateClient())
            {
                try
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(3);
                    var url = $"https://pl.gravatar.com/{Gravatar.GetHash(emailAddress)}.json";
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    // fake user agent for Gravatar API so that request does not get rejected
                    request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var text = await response.Content.ReadAsStringAsync();
                    if (text != "User not found")
                    {
                        var dto = JsonConvert.DeserializeObject<GravatarDto>(text);
                        return string.IsNullOrWhiteSpace(dto.Entry.First().DisplayName) ? result : dto.Entry.First().DisplayName;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
            return result;
        }

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }
    }
}